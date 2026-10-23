import { readFileSync } from 'node:fs';

// KKP Settlement Desk - daily merchant payout job.
// v2.3.0 (deployed yesterday) rewrote the payout loop to "simplify" it.
// This morning three merchants called support about wrong payouts.

export function processSettlements(records) {
  const payouts = {};
  let processedCount = 0;

  for (const record of records) {
    const amountBaht = parseFloat(record.amount_baht);
    if (!(amountBaht > 0)) continue;

    payouts[record.merchant] = (payouts[record.merchant] || 0) + amountBaht;
    processedCount++;
  }

  const merchantTotals = {};
  for (const [merchant, baht] of Object.entries(payouts)) {
    merchantTotals[merchant] = Math.floor(baht * 100);
  }

  return { merchantTotals, processedCount };
}

// CLI: print the settlement summary for today's file.
if (process.argv[1] && process.argv[1].endsWith('server.mjs')) {
  const records = JSON.parse(
    readFileSync(new URL('./data.json', import.meta.url), 'utf8')
  );
  console.log(JSON.stringify(processSettlements(records), null, 2));
}
