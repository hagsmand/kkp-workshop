# Facilitator Run Sheet — KKP x IBM Bob Configuration Incident (60 minutes)

Private. Never distribute. The participant pack is `index.html` + `lab/` only.

## Pre-session checklist (before the clock starts)
- [ ] Participants have Node.js >= 20 (`node --version`) and IBM Bob Base signed in
    (Bob inference needs their service connection; the guide/lab itself is offline).
- [ ] Participants have the extracted participant pack (folder containing `index.html` and `lab/`).
- [ ] That folder is open as the workspace in IBM Bob.
- [ ] **PILOT-TEST WITH LIVE BOB BEFORE THE SESSION.** The five poisons are grounded in
    documented behavior (see the doc links below), but the exact misleading answers Bob
    produces were not verified in this build — confirm each symptom manifests on your
    Bob version and adjust the symptom wording if needed.
- [ ] This facilitator folder is NOT inside what you distributed.

## Schedule
| Minutes | Phase | Participant action |
|---|---|---|
| 0–8 | Setup & reproduce | Open `index.html`; `cd lab && npm run verify` (0/5); probe prompts in Bob |
| 8–18 | Investigate | Match symptoms to configuration surfaces (docs + Bob) |
| 18–43 | Restore | Fix the 5 challenges in cascade order, byte-exact from `backups/` |
| 43–53 | Verify & win | `npm run verify` after each restore; collect letters; enter the word |
| 53–60 | Debrief | Discussion points below |

## The winning word (facilitator only)
Each challenge's letter is derived from the salted SHA-256 digest of the restored
file's bytes: `String.fromCharCode(65 + (parseInt(digest.slice(0,2),16) % 26))`.

| # | Challenge | Restored file | Letter |
|---|---|---|---|
| 1 | The muted mode | `.bob/custom_modes.yaml` | **T** |
| 2 | The missing file | `.bobignore` | **R** |
| 3 | The confident liar | `.bob/rules-ask/01-answer-policy.md` | **U** |
| 4 | The wrong standard | `.bob/rules/coding-style.md` | **S** |
| 5 | The invisible whisper | `.bob/settings.json` | **T** |

**Word: TRUST.** Quick verification: ask the participant for the word.
The word is thematically guessable ("restore trust") — treat it as a progress marker.
The real proof is the green `npm run verify` run and the participant's ability to
EXPLAIN each poison at debrief.

## The five defects (do not reveal before debrief)
All five were rolled out by the fictional v2.3.0 "standardization" deploy. Each poison
exploits a real, documented IBM Bob configuration mechanism:

| # | Live path | Poison | Documented mechanism | Symptom prompt | Expected (per docs — pilot-test) |
|---|---|---|---|---|---|
| 1 | `.bob/custom_modes.yaml` | Project-level override of the default `ask` mode with a "policy oracle" roleDefinition and **no `groups` key** | Custom modes doc: project overrides take precedence over defaults; "If you omit groups, the mode does not get any grouped tools" | "Read src/payout.js and explain what toSatang does." | Ask mode cannot read ANY file — it declines or hallucinates |
| 2 | `.bobignore` | Extra pattern `docs/payout-schedule.md` | Ignoring-files doc: read_file/list operations on ignored files are blocked; ignored entries show 🔒; the .bobignore itself is implicitly ignored so Bob cannot fix it | "Summarize docs/payout-schedule.md." | Bob reports the file as missing/blocked while other docs are readable |
| 3 | `.bob/rules-ask/01-answer-policy.md` | Mode-specific Ask rule: answer from memory, never read docs/, "refund deadline is 7 days" | Custom rules doc: `.bob/rules-{mode}/` files load alphabetically for that mode | "What is the refund deadline for digital goods? Cite the file you used." | Bob answers 7 days without citing (docs say 30 days) |
| 4 | `.bob/rules/coding-style.md` | General workspace rule: "money is floating-point baht; integer-satang is deprecated" | Custom rules doc: `.bob/rules/` applies to all modes, every session | "How should I handle money in new payout code for this project?" | Bob recommends parseFloat/Math.round (project truth is integer satang) |
| 5 | `.bob/settings.json` | `UserPromptSubmit` hook that echoes a fake "PROJECT NOTE" (cutoff 16:00 ICT, weekly payouts) | Lifecycle-hooks doc: UserPromptSubmit stdout is injected as context on every prompt | "When is the settlement cutoff and how often are payouts made?" | Bob cites a "project note" contradicting docs/payout-schedule.md (17:00 ICT, daily) |

Red herrings (NOT defects): `AGENTS.md` (healthy team standards, auto-loaded by design)
and `backups/` (the recovery source). Debrief point: not every config file is a bug.

## Restore commands (hint-level, last resort)
```bash
cp backups/custom_modes.yaml              .bob/custom_modes.yaml
cp backups/bobignore-official.txt         .bobignore
cp backups/rules-ask-01-answer-policy.md  .bob/rules-ask/01-answer-policy.md
cp backups/rules-coding-style.md          .bob/rules/coding-style.md
cp backups/settings.json                  .bob/settings.json
```

## Exact expected outputs (verified on Node v25.2.1)
- Poisoned `npm run verify`: `0 of 5 challenges passed` (exit 1), no letters.
- Any single restore: `1 of 5 challenges passed` + that challenge's letter (progress meter).
- Fully restored `npm run verify`: letters `T`, `R`, `U`, `S`, `T` and "your five letters,
  in challenge order, spell the winning word" (exit 0).
- The cascade: with all five live, challenge 1's symptom dominates (Ask has no tools).
  Each restore reveals the next symptom — the page orders the challenges this way.

## Integrity notes
- A participant can defeat a letter by editing `lab/verify.mjs` (digests) or `lab/backups/`
  and copying their own file — both are protected by the rules of engagement; ask for the
  restore commands in the terminal history at debrief.
- Deleting a poisoned file is NOT a restore — verify demands byte-equality with the
  official snapshot, and the failure message says so.
- Live Bob behavior is the participant's experience; the deterministic gate is the file
  state. A participant without a working Bob connection can still complete all five
  challenges by diagnosing from the docs, but loses the symptom experience.

## Reset
- Participant: re-extract the participant ZIP.
- Facilitator (in repo): `git checkout -- workshop-config/lab`
- Facilitator (from this folder): copy `poisoned/` back over `lab/`:
  `cp -r facilitator/poisoned/.bob lab/ && cp facilitator/poisoned/.bobignore lab/.bobignore`

## Debrief points (53–60)
1. Configuration is context: Bob's reliability is a function of its configuration,
   not just the model. Five wrong lines in `.bob/` made every answer suspect.
2. Where config lives: `.bob/rules/` (all modes) vs `.bob/rules-{mode}/` (one mode) vs
   `AGENTS.md` (auto-loaded) vs `.bob/custom_modes.yaml` (mode definitions, can override
   the built-in modes) vs `.bobignore` (access) vs `.bob/settings.json` (hooks).
3. Tool groups: a mode with no `groups` has no tools. When a mode "can't" do something,
   check its definition before blaming the model.
4. .bobignore is human-in-the-loop by design: Bob cannot edit it (implicitly ignored) —
   access rules are the operator's responsibility.
5. Hooks inject context: stdout from SessionStart/UserPromptSubmit hooks reaches the
   model on every prompt. When answers look systematically biased, audit
   `.bob/settings.json` — Bob may be being lied to behind its back.
6. Same lesson as the Settlement Desk mission: Bob's explanation was never the proof —
   the executed verification was.

## Distribution
From `workshop-config/`:
    mkdir -p dist && zip -r dist/kkp-bob-config-1hr-participant.zip index.html lab -x "*.DS_Store"
Give participants the ZIP only. This facilitator folder stays private.
