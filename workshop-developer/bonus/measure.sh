#!/usr/bin/env bash
# Ponytail A/B measurement: quantifies what a coding session changed.
# Compares a workdir against the pristine starter in bonus/lab — no git needed.
# Usage: bonus/measure.sh <workdir>
set -euo pipefail
dir="${1:?usage: measure.sh <workdir>}"
ref="$(cd "$(dirname "$0")" && pwd)/lab"
[ -d "$dir" ] || { echo "no such workdir: $dir" >&2; exit 1; }

# Count only code changes: skip VCS metadata, the agent's own .bob config,
# installed packages and macOS noise.
EXCL=(-path '*/.git' -o -path '*/.git/*' -o -path '*/.bob' -o -path '*/.bob/*' \
      -o -path '*/node_modules' -o -path '*/node_modules/*' -o -name '.DS_Store')

added=0; removed=0; files=0; newfiles=0
rows=()

# changed + new files (present in the workdir)
while IFS= read -r f; do
  if [ -f "$ref/$f" ]; then
    a=$(diff -u "$ref/$f" "$dir/$f" | awk '/^\+\+\+/{next} /^\+/{n++} END{print n+0}')
    r=$(diff -u "$ref/$f" "$dir/$f" | awk '/^---/{next} /^-/{n++} END{print n+0}')
    [ "$a" -gt 0 ] || [ "$r" -gt 0 ] || continue   # untouched file: skip row
    files=$((files+1))
  else
    newfiles=$((newfiles+1)); files=$((files+1))
    a=$(awk 'END{print NR}' "$dir/$f"); r=0
  fi
  added=$((added+a)); removed=$((removed+r))
  rows+=("$(printf '%-32s %5s %5s' "${f#./}" "+$a" "-$r")")
done < <(cd "$dir" && find . -type f ! \( "${EXCL[@]}" \) | sort)

# deleted files (in the starter, missing from the workdir)
while IFS= read -r f; do
  if [ ! -f "$dir/$f" ]; then
    files=$((files+1))
    r=$(awk 'END{print NR}' "$ref/$f")
    removed=$((removed+r))
    rows+=("$(printf '%-32s %5s %5s' "${f#./} (deleted)" "+0" "-$r")")
  fi
done < <(cd "$ref" && find . -type f ! \( "${EXCL[@]}" \) | sort)

echo "=== diff vs pristine starter (bonus/lab) ==="
if [ "${#rows[@]}" -gt 0 ]; then printf '%s\n' "${rows[@]}"; fi
echo "lines added:   $added"
echo "lines removed: $removed"
echo "files touched: $files (new files: $newfiles)"

echo "=== package.json changes (new dependencies would show here) ==="
diff -u "$ref/package.json" "$dir/package.json" || true

echo "=== tests ==="
(cd "$dir" && node --test 2>&1 | grep -E '^. (pass|fail)' || true)

echo "=== program output ==="
(cd "$dir" && node server.mjs)
