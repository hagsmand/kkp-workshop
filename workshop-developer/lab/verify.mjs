import { createHash } from 'node:crypto';
import { readFileSync, existsSync } from 'node:fs';
import { processSettlements } from './server.mjs';

const sha256 = (s) => createHash('sha256').update(s, 'utf8').digest('hex');

console.log('KKP Double Incident - mission verification\n');
let passed = 0;

// ---- Part 1 - Restore Bob (configuration) --------------------------------
// Each challenge's letter is derived from the salted digest of the restored
// file's bytes, so a letter can only be computed when the restore is real.
// A challenge passes when its live configuration file is byte-identical to
// the official pre-deploy snapshot (see backups/).
const configChallenges = [
  { name: 'The muted mode (custom modes)',      path: '.bob/custom_modes.yaml',             salt: '0012', digest: '6136dfc4dd6bfba89c33631113737fc5f22360e7c9eeb5450cd8202294436bda' },
  { name: 'The missing file (.bobignore)',      path: '.bobignore',                         salt: '005c', digest: '45557427bbac7473fd18fdc69b73489f865a4c720b8e27d7b2cb3d8a5f4acf94' },
  { name: 'The confident liar (Ask rules)',     path: '.bob/rules-ask/01-answer-policy.md', salt: '0048', digest: '2eec639166edc3a08bcb6651e35d73e87e3d5fef0859b378f7f4c24e4dc33a34' },
  { name: 'The wrong standard (project rules)', path: '.bob/rules/coding-style.md',         salt: '000b', digest: 'e25873258d3cc5a5d43c73cc9ae3825fd25ef61c566b2f82380da0f0c3e5453b' },
  { name: 'The invisible whisper (hooks)',      path: '.bob/settings.json',                 salt: '005a', digest: 'e3acbd15b17428020f8cedeb751ff4f1990c3fcf8e7a077152f04ed701bcf9dd' }
];

console.log('Part 1 - Restore Bob (configuration):');
configChallenges.forEach((challenge, i) => {
  const label = `Challenge ${i + 1} - ${challenge.name}`;
  const file = new URL(challenge.path, import.meta.url);
  if (!existsSync(file)) {
    console.log(`${label}: FAILED  -> ${challenge.path} is missing`);
    return;
  }
  const actual = sha256(challenge.salt + readFileSync(file, 'utf8'));
  if (actual === challenge.digest) {
    passed++;
    const letter = String.fromCharCode(65 + (parseInt(actual.slice(0, 2), 16) % 26));
    console.log(`${label}: PASSED  -> letter: ${letter}`);
  } else {
    console.log(`${label}: FAILED  -> ${challenge.path} is not the official snapshot (restore byte-exact from backups/ - deleting or rewriting it by hand does not count)`);
  }
});

// ---- Part 2 - Fix the Settlement Desk (code) -----------------------------
// Each challenge's letter is derived from that merchant's correct satang
// total, so a letter can only be computed when the repair is real.
const records = JSON.parse(
  readFileSync(new URL('./data.json', import.meta.url), 'utf8')
);
const { merchantTotals } = processSettlements(records);
const letterForTotal = (satang) => String.fromCharCode(65 + (satang % 26));
const settlementChallenges = [
  { name: 'Idempotency (SYN-A1)',  actual: merchantTotals['SYN-A1'], expected: 400051 },
  { name: 'Business day (SYN-A2)', actual: merchantTotals['SYN-A2'], expected: 300040 },
  { name: 'Exact satang (SYN-B1)', actual: merchantTotals['SYN-B1'], expected: 466672 }
];

console.log('\nPart 2 - Fix the Settlement Desk (code):');
for (const challenge of settlementChallenges) {
  if (challenge.actual === challenge.expected) {
    passed++;
    console.log(`Challenge - ${challenge.name}: PASSED  -> letter: ${letterForTotal(challenge.actual)}`);
  } else {
    console.log(`Challenge - ${challenge.name}: FAILED  -> got ${challenge.actual}, expected ${challenge.expected}`);
  }
}

// ---- Summary --------------------------------------------------------------
const total = configChallenges.length + settlementChallenges.length;
console.log('');
if (passed === total) {
  console.log('All challenges passed. Your Part 1 letters spell the first winning word;');
  console.log('your Part 2 letters spell the second. Enter both on the workshop page');
  console.log('to complete the mission.');
} else {
  console.log(`${passed} of ${total} challenges passed. Keep going.`);
  process.exit(1);
}
