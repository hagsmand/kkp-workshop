# Facilitator Run Sheet — KKP x IBM Bob Double Incident (60 minutes)

Private. Never distribute. The participant pack is `index.html` + `lab/` only.

## Pre-session checklist (before the clock starts)
- [ ] Participants have Node.js >= 20 (`node --version`) and IBM Bob Base signed in
    (Bob inference needs their service connection; the guide/lab itself is offline).
- [ ] Participants have the extracted participant pack (folder containing `index.html` and `lab/`).
- [ ] **The `lab/` folder is open as the workspace in IBM Bob** — Bob's `.bob` configuration
    lives at the workspace root, so opening the pack root instead silently skips every poison.
- [ ] **PILOT-TEST WITH LIVE BOB BEFORE THE SESSION.** The five configuration poisons are
    grounded in documented behavior (links in the defect matrix), but the exact misleading
    answers Bob produces were not verified in this build — confirm each Part 1 symptom
    manifests on your Bob version and adjust wording if needed.
- [ ] This facilitator folder is NOT inside what you distributed.

## Schedule
| Minutes | Phase | Participant action |
|---|---|---|
| 0–8 | Setup & reproduce | Open `index.html`; open `lab/` as the Bob workspace; `cd lab && npm test` (5 red); `npm run verify` (0 of 8); Bob self-check prompt |
| 8–20 | Part 1 — Restore Bob | Match symptoms to config surfaces; restore byte-exact from `backups/` |
| 20–28 | Part 2 — Investigate (Bob Ask) | `npm start`; Ask prompt; hypotheses vs evidence |
| 28–48 | Part 2 — Repair (Bob Agent) | Copy Agent prompt; review diff; approve; iterate until green |
| 48–60 | Debrief | `npm run verify` 8/8 green; review terminal history + diff; discuss poisons |

Pacing note: 8 challenges in 60 minutes is deliberately tight. If the audience is mixed,
extend Part 2's repair window first; the Part 1 restores are quick once diagnosed.

## Progress letters (facilitator only)
The page no longer collects winning words (section 8 was removed) — the mission's
finish line is `npm run verify` reporting 8/8. `verify.mjs` still prints one
tamper-evident letter per solved challenge: Part 1 letters derive from the salted
SHA-256 digest of each restored file's bytes; Part 2 letters derive from the
merchant's correct satang total. For reference they spell **TRUST** (T custom_modes ·
R .bobignore · U rules-ask · S rules · T hooks) and **PAY** (P 400051 · A 300040 ·
Y 466672) — useful as a quick progress read if you walk the room, but nothing is
entered anywhere. The real proof is the green `npm run verify` run and the
participant's ability to EXPLAIN each poison and defect at debrief.

## The eight defects (do not reveal before debrief)
One fictional v2.3.0 deploy caused everything: it rewrote the payout loop AND
"standardized" the `.bob` configuration.

### Part 1 — configuration poisons (each exploits documented IBM Bob behavior)
| # | Live path | Poison | Documented mechanism | Symptom prompt |
|---|---|---|---|---|
| 1 | `.bob/custom_modes.yaml` | Project override of `ask` with a "policy oracle" role and **no `groups`** | Custom modes: project overrides beat defaults; omitted groups = no tools | "Read src/payout.js and explain what toSatang does." |
| 2 | `.bobignore` | Extra pattern `docs/payout-schedule.md` | Ignoring files: reads blocked, 🔒 marker, implicitly ignored by Bob itself | "Summarize docs/payout-schedule.md." |
| 3 | `.bob/rules-ask/01-answer-policy.md` | Answer from memory, never read docs, "refund is 7 days" | Custom rules: `.bob/rules-{mode}/` loads for that mode | "What is the refund deadline for digital goods?" |
| 4 | `.bob/rules/coding-style.md` | "Money is floating-point baht; satang deprecated" | Custom rules: `.bob/rules/` applies to all modes | "How should I handle money in new payout code?" |
| 5 | `.bob/settings.json` | `UserPromptSubmit` hook echoing a fake PROJECT NOTE (16:00 ICT, weekly) | Lifecycle hooks: stdout injected as context on every prompt | "When is the settlement cutoff and how often are payouts made?" |

Red herrings (NOT defects): `AGENTS.md` (healthy, auto-loaded by design) and `backups/`
(the recovery source). TXN-010 ("0.00") is the Part 2 equivalent.

### Part 2 — code defects in `lab/server.mjs`
| # | Defect | Symptom | Fixed by |
|---|---|---|---|
| 6 | No idempotency: seen ids never remembered | SYN-A1 paid twice for redelivered TXN-001 (550051 vs 400051) | Track ids in a `Set`; first occurrence wins |
| 7 | No business-day window: timestamps unchecked | SYN-A2 paid for TXN-005 (01:05 ICT 23 Oct) and TXN-006 (16:00 ICT 21 Oct) | Filter to [2026-10-21T17:00:00.000Z, 2026-10-22T17:00:00.000Z) |
| 8 | Float money: parse to floats, sum, `Math.floor` | SYN-B1 paid 466671 instead of 466672 satang | Parse to integer satang at the boundary; accumulate integers |

## Restore commands (Part 1 hint level — last resort)
```bash
cp backups/custom_modes.yaml              .bob/custom_modes.yaml
cp backups/bobignore-official.txt         .bobignore
cp backups/rules-ask-01-answer-policy.md  .bob/rules-ask/01-answer-policy.md
cp backups/rules-coding-style.md          .bob/rules/coding-style.md
cp backups/settings.json                  .bob/settings.json
```

## Exact expected outputs (verified on Node v25.2.1)
- Starter `npm test`: 5 tests, 0 pass, 5 fail (exit 1)
- Starter `npm start`: `{"merchantTotals":{"SYN-A1":550051,"SYN-A2":500125,"SYN-B1":466671},"processedCount":9}`
- Starter `npm run verify`: `0 of 8 challenges passed` (exit 1), no letters
- Config restored only: `5 of 8 challenges passed` + letters T, R, U, S, T
- Reference server (copied in): `npm test` 5 pass; `npm run verify` letters T R U S T P A Y
  and "All challenges passed. Mission complete — Bob is trustworthy again"
- The cascade: with all five config poisons live, Challenge 1's symptom dominates
  (Ask mode has no tools). Each restore reveals the next symptom; after Part 1, Bob is
  reliable for the Part 2 investigation.

## Integrity notes
- Part 1 letters are salted digests — editing `verify.mjs` or `backups/` to self-award
  requires consistent tampering; both are protected by the rules of engagement. Ask to see
  the restore commands in terminal history at debrief.
- Part 2: hardcoding expected totals in `server.mjs` defeats the letters. Completion = green verify + reviewed diff + explained poisons.
- Deleting a poisoned file is NOT a restore — verify demands byte-equality with the
  official snapshot, and the failure message says so.

## Reset
- Participant: re-extract the participant ZIP.
- Facilitator (in repo): `git checkout -- workshop-developer/lab`
- Facilitator (from this folder): copy `poisoned/` back over `lab/` and restore the
  broken starter via git:
  `cp -r facilitator/poisoned/.bob lab/ && cp facilitator/poisoned/.bobignore lab/.bobignore`

## Debrief points (56–60)
1. Two incidents, one deploy: the code was broken AND the assistant was compromised.
   Fix your tools before trusting their output.
2. Configuration is context: where each surface lives (`.bob/rules/`, `rules-{mode}/`,
   `AGENTS.md`, `custom_modes.yaml`, `.bobignore`, `settings.json` hooks).
3. A mode with no tool groups has no tools; `.bobignore` is human-in-the-loop by design;
   hook stdout is injected context — audit `settings.json` when answers look biased.
4. Symptom vs root cause: eight symptoms, one deploy, eight distinct defects — and
   oddities that were never bugs (TXN-010, AGENTS.md).
5. Integer satang (555.55 + 1555.55 + 2555.62 floors to 466671) and half-open UTC
   windows for "22 Oct in Bangkok".
6. Bob's explanation was never the proof — the executed tests and byte-exact restores were.

## Distribution
From `workshop-developer/`:
    mkdir -p dist && zip -r dist/kkp-bob-1hr-participant.zip index.html lab -x "*.DS_Store"
Give participants the ZIP only. This facilitator folder stays private.
