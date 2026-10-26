# Ponytail A/B — Reference Results (facilitator only)

Reference run of the exact bonus exercise, executed 26 Oct 2026 with two
identical subagents (same model, same tools) on the `bonus/lab/` starter.
Both received the identical `TASK.md` ticket (FIN-1201, settlement
reconciliation tool). Run A had no skill. Run B had the ponytail skill
active for the whole session.

## Numbers

| Metric | Run A (no skill) | Run B (ponytail) |
|---|---|---|
| Lines added | 229 | **73 (−68%)** |
| — tool itself (`reconcile.mjs`) | 105 lines | 44 lines (2.4× smaller) |
| — tests (`test/reconcile.test.mjs`) | 124 lines, 7 tests | 29 lines, 2 tests |
| Files touched | 2 (both new) | 2 (both new) |
| New dependencies | 0 | 0 |
| Tests after the change | 11 pass, 0 fail | 6 pass, 0 fail |
| API calls | 12 | 10 |
| Agent activity volume (transcript minus kickoff) | ~11.0 KB | **~7.5 KB (−31%)** |
| Wall time | 221.8 s | 210.9 s |

Both tools are functionally correct: both flag SYN-A2 (bank 10 baht short)
and SYN-C1 (paid by the bank, never settled by the desk), compare money
exactly in integer satang, print the clean-merchant count, and exit 1.

Token note: subagent token totals are not directly exposed, so transcript
bytes minus the fixed kickoff prompt are used as the activity proxy. Run B's
kickoff includes the full 3.4 KB skill text — the −31% figure already
accounts for paying that cost. Participants should record Bob's real
per-session token usage during the live exercise.

## What each agent actually did

**Run A** wrote a 105-line tool plus a 124-line test suite (7 tests),
including its own fixture-building helpers and case-by-case coverage. Good
work, correct, well tested — and 3.1× the code the ticket needed.

**Run B** wrote a 44-line tool plus a 29-line check (2 tests: comparison
logic + CLI contract). It reused `processSettlements` and `toSatang`,
formatted baht with integer string math, mirrored the existing argv-guard
CLI pattern from `server.mjs`, and verified the exit-0 path with a temporary
fixture it deleted afterwards. It ended with the skill's disclosure:
skipped a real CSV parser (`split(',')` only) and named the risk (quoted
fields would misparse).

## Interpretation for the debrief

1. The skill did not change correctness — both runs are right. It changed
   **volume**: 3.1× less code to review, ~30% less context churn, fewer API
   calls, at equal wall time.
2. The cost structure matters: the skill's own text is a **fixed** ~3.4 KB
   context cost. It only pays off when the task has enough surface for
   over-engineering to cost more than that. (See the history note below.)
3. The plain run's extra 124 lines of tests are not "better quality" here —
   both shipped tested, working tools; the extra tests cover the extra
   structure the plain run invented.
4. The closing disclosure ("skipped X, risk Y") is itself a deliverable:
   the reviewer learns the shortcut's limit without reading the code.

## History note — why this task is the size it is

An earlier round used a small ticket (print a formatted summary, ~20-line
solution). Result: 25 vs 21 lines and near-equal tokens — the skill's fixed
context cost roughly cancelled its savings, and with some models the
with-skill session spent *more* tokens. Lesson for participants: **skills
are overhead until the task is big enough to give them leverage.** That is
why the bonus ticket is a small-but-real tool (CSV, matching, reporting,
exit codes) rather than a formatting change.

## Caveats

- Single run per configuration, one model. Present these numbers as "what
  we measured once", not a benchmark.
- Both outputs are functionally correct; the skill changed the *shape* of
  the change, not its correctness.
- If your live run shows a smaller gap, ask participants what the plain
  agent spent its extra lines on — structure, tests of structure, or
  defensive parsing nobody asked for.

## Reproduction

```bash
# two copies of the starter, one with the skill pre-installed
# (run from workshop-developer/)
cd workshop-developer
cp -r bonus/lab /tmp/run-a && cp -r bonus/lab /tmp/run-b
mkdir -p /tmp/run-b/.bob/skills
cp -r bonus/skills/ponytail /tmp/run-b/.bob/skills/ponytail
# give each copy the same TASK.md ticket in separate agent sessions,
# then measure (measure.sh diffs against the pristine bonus/lab — no git):
bonus/measure.sh /tmp/run-a
bonus/measure.sh /tmp/run-b
```
