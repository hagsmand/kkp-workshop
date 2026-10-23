import { createHash } from 'node:crypto';
import { readFileSync, existsSync } from 'node:fs';

const sha256 = (s) => createHash('sha256').update(s, 'utf8').digest('hex');

// Each challenge's letter is derived from the salted digest of the restored
// file's bytes, so a letter can only be computed when the restore is real.
// A challenge passes when its live configuration file is byte-identical to
// the official pre-deploy snapshot (see backups/).
const challenges = [
  { name: 'The muted mode (custom modes)',      path: '.bob/custom_modes.yaml',             salt: '0012', digest: '6136dfc4dd6bfba89c33631113737fc5f22360e7c9eeb5450cd8202294436bda' },
  { name: 'The missing file (.bobignore)',      path: '.bobignore',                         salt: '005c', digest: '45557427bbac7473fd18fdc69b73489f865a4c720b8e27d7b2cb3d8a5f4acf94' },
  { name: 'The confident liar (Ask rules)',     path: '.bob/rules-ask/01-answer-policy.md', salt: '0048', digest: '2eec639166edc3a08bcb6651e35d73e87e3d5fef0859b378f7f4c24e4dc33a34' },
  { name: 'The wrong standard (project rules)', path: '.bob/rules/coding-style.md',         salt: '000b', digest: 'e25873258d3cc5a5d43c73cc9ae3825fd25ef61c566b2f82380da0f0c3e5453b' },
  { name: 'The invisible whisper (hooks)',      path: '.bob/settings.json',                 salt: '005a', digest: 'e3acbd15b17428020f8cedeb751ff4f1990c3fcf8e7a077152f04ed701bcf9dd' }
];

console.log('KKP Configuration Incident - mission verification\n');
let passed = 0;
challenges.forEach((challenge, i) => {
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
console.log('');
if (passed === challenges.length) {
  console.log('All challenges passed. Your five letters, in challenge order, spell');
  console.log('the winning word. Enter it on the workshop page to complete the mission.');
} else {
  console.log(`${passed} of ${challenges.length} challenges passed. Keep restoring the configuration.`);
  process.exit(1);
}
