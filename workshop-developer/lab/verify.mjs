import { readFileSync } from 'node:fs';
import { processSettlements } from './server.mjs';

const records = JSON.parse(
  readFileSync(new URL('./data.json', import.meta.url), 'utf8')
);
const { merchantTotals } = processSettlements(records);

// Each challenge's letter is derived from that merchant's correct satang
// total, so a letter can only be computed when the repair is real.
const letterFor = (satang) => String.fromCharCode(65 + (satang % 26));

const challenges = [
  { name: 'Idempotency (SYN-A1)',  actual: merchantTotals['SYN-A1'], expected: 400051 },
  { name: 'Business day (SYN-A2)', actual: merchantTotals['SYN-A2'], expected: 300040 },
  { name: 'Exact satang (SYN-B1)', actual: merchantTotals['SYN-B1'], expected: 466672 }
];

console.log('KKP Settlement Desk - mission verification\n');
let passed = 0;
for (const challenge of challenges) {
  if (challenge.actual === challenge.expected) {
    passed++;
    console.log(`Challenge - ${challenge.name}: PASSED  -> letter: ${letterFor(challenge.actual)}`);
  } else {
    console.log(`Challenge - ${challenge.name}: FAILED  -> got ${challenge.actual}, expected ${challenge.expected}`);
  }
}
console.log('');
if (passed === challenges.length) {
  console.log('All challenges passed. Your three letters, in challenge order, spell');
  console.log('the winning word. Enter it on the workshop page to complete the mission.');
} else {
  console.log(`${passed} of ${challenges.length} challenges passed. Keep repairing lab/server.mjs.`);
  process.exit(1);
}
