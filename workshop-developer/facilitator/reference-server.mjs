import { readFileSync } from 'node:fs';

// FACILITATOR REFERENCE SOLUTION - never distribute to participants.

function toSatang(amountBaht) {
  const [baht, satang = '0'] = amountBaht.split('.');
  if (!/^\d+$/.test(baht) || !/^\d*$/.test(satang) || satang.length > 2) {
    throw new Error(`Invalid amount_baht: ${amountBaht}`);
  }
  return Number(baht) * 100 + Number(satang.padEnd(2, '0'));
}

export function processSettlements(records) {
  // Business day 22 Oct 2026, Asia/Bangkok (ICT = UTC+7):
  // 00:00-24:00 ICT == [2026-10-21T17:00:00.000Z, 2026-10-22T17:00:00.000Z)
  const windowStart = Date.parse('2026-10-21T17:00:00.000Z');
  const windowEnd = Date.parse('2026-10-22T17:00:00.000Z');

  const seenIds = new Set();
  const merchantTotals = {};
  let processedCount = 0;

  for (const record of records) {
    const satang = toSatang(record.amount_baht);
    if (!(satang > 0)) continue;                      // rule 4: non-positive never settles
    if (seenIds.has(record.id)) continue;             // rule 1: idempotency, first wins
    const timestamp = Date.parse(record.timestamp);
    if (timestamp < windowStart || timestamp >= windowEnd) continue; // rule 2: half-open window

    seenIds.add(record.id);
    merchantTotals[record.merchant] = (merchantTotals[record.merchant] || 0) + satang; // rule 3: integer satang
    processedCount++;
  }

  return { merchantTotals, processedCount };
}

if (process.argv[1] && process.argv[1].endsWith('server.mjs')) {
  // ../lab/data.json resolves correctly both in place (facilitator/) and when
  // this file is copied over lab/server.mjs (lab/../lab/data.json == lab/data.json).
  const records = JSON.parse(
    readFileSync(new URL('../lab/data.json', import.meta.url), 'utf8')
  );
  console.log(JSON.stringify(processSettlements(records), null, 2));
}
