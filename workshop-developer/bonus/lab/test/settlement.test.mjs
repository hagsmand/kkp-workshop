import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readFileSync } from 'node:fs';
import { processSettlements } from '../server.mjs';

// Business day under test: 22 Oct 2026, Asia/Bangkok (ICT = UTC+7).
const records = JSON.parse(
  readFileSync(new URL('../data.json', import.meta.url), 'utf8')
);

test('SYN-A1 settles each transaction exactly once', () => {
  const { merchantTotals } = processSettlements(records);
  assert.equal(merchantTotals['SYN-A1'], 400051, 'SYN-A1 payout must be 400051 satang');
});

test('SYN-A2 settles only transactions from the 22 Oct 2026 Bangkok business day', () => {
  const { merchantTotals } = processSettlements(records);
  assert.equal(merchantTotals['SYN-A2'], 300040, 'SYN-A2 payout must be 300040 satang');
});

test('SYN-B1 payout is exact to the satang', () => {
  const { merchantTotals } = processSettlements(records);
  assert.equal(merchantTotals['SYN-B1'], 466672, 'SYN-B1 payout must be 466672 satang');
});

test('exactly 6 transactions settle in this run', () => {
  const { processedCount } = processSettlements(records);
  assert.equal(processedCount, 6, 'processedCount must be 6');
});
