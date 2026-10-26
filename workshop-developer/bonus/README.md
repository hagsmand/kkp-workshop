# Bonus — Ponytail A/B

Optional 10-minute extension after the main mission. Full instructions are in
the "★ Bonus" section at the bottom of `../index.html`.

- `lab/` — the repaired Settlement Desk starter (green tests) + the Finance
  ticket in `lab/TASK.md`
- `skills/ponytail/` — the ponytail skill (MIT, from
  github.com/DietrichGebert/ponytail)
- `measure.sh` — diff/deps/test metrics for each run

Quick start (cd into `workshop-developer/` first):

```bash
cd kkp-workshop/workshop-developer   # from the folder containing the repo
cp -r bonus/lab run-a        # run A: no skill
cp -r bonus/lab run-b        # run B: with ponytail
mkdir -p run-b/.bob/skills
cp -r bonus/skills/ponytail run-b/.bob/skills/ponytail
```

`measure.sh` compares each run against the pristine `bonus/lab` starter with
plain `diff` — no git needed, no baseline commit.

Reference numbers from our live A/B: `../facilitator/ponytail-expected-results.md`
(facilitator only).
