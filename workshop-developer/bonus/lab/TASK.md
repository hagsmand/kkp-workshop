# Task: Settlement reconciliation tool

From: Finance team (ticket FIN-1201)

Every morning the bank sends us a statement of the payouts it actually
executed. This morning the totals do not match what the Settlement Desk
computed, and finance needs to see exactly where before calling the bank.

Build a reconciliation tool:

- read the bank statement from `bank-statement.csv` (columns:
  `merchant,amount_baht`)
- compare it against what the Settlement Desk computed for the same day —
  the existing `processSettlements` in `server.mjs`, run over `data.json`
- print one line per discrepancy: which merchant, what the desk computed,
  what the bank paid, and the difference in baht
- print a summary line: how many merchants reconciled cleanly
- exit with code 1 when there is any discrepancy, 0 otherwise

The finance team runs it with: `node reconcile.mjs`

The existing tests must keep passing. Money must be compared exactly, to
the satang.
