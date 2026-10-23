# Facilitator Run Sheet — KKP x IBM Bob Settlement Desk (60 minutes)

Private. Never distribute. The participant pack is `index.html` + `lab/` only.

## Pre-session checklist (before the clock starts)
- [ ] Participants have Node.js >= 20 (`node --version`) and IBM Bob Base signed in
    (Bob inference needs their service connection; the guide/lab itself is offline).
- [ ] Participants have the extracted participant pack (folder containing `index.html` and `lab/`).
- [ ] That folder is open as the workspace in IBM Bob.
- [ ] This facilitator folder is NOT inside what you distributed.

## Schedule
| Minutes | Phase | Participant action |
|---|---|---|
| 0–8 | Setup & reproduce | Open `index.html`; `cd lab && npm test` (5 red); `npm start` (wrong payouts) |
| 8–23 | Investigate (Bob Ask) | Copy Ask prompt; hypotheses vs evidence; open questions |
| 23–43 | Repair (Bob Agent) | Copy Agent prompt; review diff; approve; iterate until green |
| 43–53 | Verify & win | `npm test` 5/5 green; `npm run verify` -> one letter per challenge; enter the word |
| 53–60 | Debrief | Discussion points below |

## The winning word (facilitator only)
Each challenge's letter is derived from the merchant's correct satang total:
`String.fromCharCode(65 + (total % 26))`.

- Challenge 1 — Idempotency (SYN-A1 = 400051): **P**
- Challenge 2 — Business day (SYN-A2 = 300040): **A**
- Challenge 3 — Exact satang (SYN-B1 = 466672): **Y**

**Word: PAY.** Quick verification: ask the participant for the word.
The word is thematically guessable ("the desk pays again") — treat it as a progress
marker. The real proof of completion is the green test run and the reviewed diff.

## The three defects (do not reveal before debrief)
All three were introduced by the fictional v2.3.0 "simplification" deploy:

| # | Defect in `lab/server.mjs` | Symptom | Fixed by |
|---|---|---|---|
| 1 | No idempotency: the loop never remembers seen transaction ids | SYN-A1 paid twice for redelivered TXN-001 (550051 vs 400051 satang) | Track ids in a `Set`; skip repeats (first occurrence wins) |
| 2 | No business-day window: timestamps are never checked | SYN-A2 paid for TXN-005 (01:05 ICT 23 Oct) and TXN-006 (16:00 ICT 21 Oct) | Filter to [2026-10-21T17:00:00.000Z, 2026-10-22T17:00:00.000Z) |
| 3 | Float money: baht parsed as decimal floats, summed, then `Math.floor(sum * 100)` | SYN-B1 paid 466671 instead of 466672 satang | Parse each amount to integer satang at the boundary; accumulate integers |

Unrelated observation (NOT a defect): TXN-010 has amount "0.00" and is correctly skipped
by both broken and fixed code — good debrief point about symptoms vs causes.

## Exact expected outputs (verified on Node v25.2.1; re-record yours if fixtures change)
- Starter `npm start`: `{"merchantTotals":{"SYN-A1":550051,"SYN-A2":500125,"SYN-B1":466671},"processedCount":9}`
- Starter `npm test`: 5 tests, 0 pass, 5 fail (exit 1)
- Starter `npm run verify`: 0 of 3 challenges passed, no letters (exit 1)
- Reference `npm test`: 5 tests, 5 pass, 0 fail (exit 0)
- Reference `npm start`: `{"merchantTotals":{"SYN-A1":400051,"SYN-A2":300040,"SYN-B1":466672},"processedCount":6}`
- Reference `npm run verify`: letters P, A, Y — "your three letters, in challenge order, spell the winning word"

## Partial-repair demo (shows no single fix resolves the incident)
| Repair applied | SYN-A1 | SYN-A2 | SYN-B1 | count | Tests green | Letter |
|---|---|---|---|---|---|---|
| none (starter) | 550051 | 500125 | 466671 | 9 | 0/5 | — |
| dedup only | 400051 | 500125 | 466671 | 8 | 1/5 | P |
| window only | 550051 | 300040 | 466671 | 7 | 1/5 | A |
| satang-int only | 550051 | 500125 | 466672 | 9 | 1/5 | Y |
| all three (reference) | 400051 | 300040 | 466672 | 6 | 5/5 | P A Y |

## Integrity notes
- A participant can defeat the gate by hardcoding the expected totals in `server.mjs`,
  or by brute-forcing the 3-letter word against the page's digest. Completion = green
  tests + a reviewed diff (the Agent prompt forbids hardcoding; ask to see the diff
  during debrief). The word is a learning gate, not proctoring.
- `verify.mjs` checks the three totals independently of the test files, so weakening
  or deleting tests does not award letters.

## Reset
- Participant: re-extract the participant ZIP.
- Facilitator (in repo): `git checkout -- workshop-developer/lab/server.mjs`

## Debrief points (53–60)
1. Symptom vs root cause: three symptoms, one deploy, three distinct defects —
   and one odd record (TXN-010) that was never a bug.
2. Why payments code uses integer minor units: 555.55 + 1555.55 + 2555.62 in IEEE-754
   floats floors to 466671, not 466672.
3. Timezones: "22 Oct in Bangkok" is a UTC window [17:00Z, 17:00Z) — half-open bounds matter.
4. How Bob was used: Ask built hypotheses against evidence; Agent proposed a diff that a
   human approved. Bob's explanation was never the proof — the executed tests were.

## Distribution
From `workshop-developer/`:
    mkdir -p dist && zip -r dist/kkp-bob-1hr-participant.zip index.html lab -x "*.DS_Store"
Give participants the ZIP only. This facilitator folder stays private.
