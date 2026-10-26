import { readFileSync } from 'node:fs';
import { toSatang } from './src/payout.js';

// KKP Settlement Desk — daily merchant payout job.
// Repaired version (end of the workshop main session): dedupes by transaction
// id, settles only the Bangkok business day (ICT = UTC+7), keeps money exact
// in integer satang.

const DAY_START_UTC = Date.parse('2026-10-21T17:00:00Z');
const DAY_END_UTC = Date.parse('2026-10-22T17:00:00Z');

export function processSettlements(records) {
  const payouts = {};
  const seen = new Set();
  let processedCount = 0;

  for (const record of records) {
    if (seen.has(record.id)) continue;
    const ts = Date.parse(record.timestamp);
    if (!(ts >= DAY_START_UTC && ts < DAY_END_UTC)) continue;
    const satang = toSatang(record.amount_baht);
    if (!(satang > 0)) continue;

    seen.add(record.id);
    payouts[record.merchant] = (payouts[record.merchant] || 0) + satang;
    processedCount++;
  }

  return { merchantTotals: payouts, processedCount };
}

// CLI: print the settlement summary for today's file.
if (process.argv[1] && process.argv[1].endsWith('server.mjs')) {
  const records = JSON.parse(
    readFileSync(new URL('./data.json', import.meta.url), 'utf8')
  );
  console.log(JSON.stringify(processSettlements(records), null, 2));
}
