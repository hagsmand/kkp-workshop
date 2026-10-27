#!/usr/bin/env python3
"""Check that pqdata.csv is an exact, valid export of pqtracker.sqlite.

Run from workshop-challenge/data-migration/ after your converter has produced
pqdata.csv:

    python3 check_migration.py

Passes when every database row appears in the CSV with the same values, in the
exact legacy format. Exits 1 and lists the problems otherwise.
Python 3.8+, standard library only.
"""
import argparse
import csv
import datetime
import os
import sqlite3
import sys

DB_DEFAULT = "pqtracker.sqlite"
CSV_DEFAULT = "pqdata.csv"
HEADER = ["Reference", "MPName", "QuestionText", "PQType", "DateReceived",
          "Deadline", "AssignedTeam", "Status", "ResponseText", "DateAnswered"]
COLUMNS = ("reference", "mp_name", "question_text", "pq_type", "date_received",
           "deadline", "assigned_team", "status", "response_text", "date_answered")
DATE_FIELDS = ("DateReceived", "Deadline", "DateAnswered")


def load_db(path):
    if not os.path.exists(path):
        sys.exit("FAIL: database not found: %s (run this script from data-migration/, "
                 "or build it first with: python3 build_db.py)" % path)
    con = sqlite3.connect(path)
    try:
        rows = con.execute(
            "SELECT %s FROM parliamentary_questions ORDER BY reference" % ",".join(COLUMNS)
        ).fetchall()
    except sqlite3.Error as e:
        sys.exit("FAIL: cannot read %s: %s" % (path, e))
    finally:
        con.close()
    return [dict(zip(HEADER, r)) for r in rows]


def valid_date(s):
    try:
        datetime.datetime.strptime(s, "%Y-%m-%d")
        return True
    except ValueError:
        return False


def main():
    ap = argparse.ArgumentParser(description=__doc__)
    ap.add_argument("--db", default=DB_DEFAULT)
    ap.add_argument("--csv", default=CSV_DEFAULT)
    args = ap.parse_args()

    db = load_db(args.db)
    if not os.path.exists(args.csv):
        sys.exit("FAIL: %s not found - write a converter that produces it in this folder"
                 % args.csv)

    with open(args.csv, newline="", encoding="utf-8") as f:
        rows = list(csv.reader(f))

    problems = []
    if not rows:
        sys.exit("FAIL: %s is empty" % args.csv)
    if rows[0] != HEADER:
        problems.append("header mismatch: expected '%s' got '%s'"
                        % (",".join(HEADER), ",".join(rows[0])))

    data = rows[1:]
    seen = set()
    for i, r in enumerate(data, start=2):
        if len(r) != 10:
            problems.append("line %d: expected 10 fields, found %d" % (i, len(r)))
            continue
        if any("," in field for field in r):
            problems.append("line %d: comma inside a field - the legacy loader splits on every comma" % i)
        ref = r[0]
        if ref in seen:
            problems.append("line %d: duplicate reference %s" % (i, ref))
        seen.add(ref)

    db_by_ref = {d["Reference"]: d for d in db}
    for r in data:
        if len(r) != 10:
            continue
        ref = r[0]
        d = db_by_ref.get(ref)
        if d is None:
            problems.append("%s: not in the database - remove it or fix the reference" % ref)
            continue
        for j, name in enumerate(HEADER):
            got = r[j]
            want = "" if d[name] is None else str(d[name])
            if name in DATE_FIELDS:
                if got and not valid_date(got):
                    problems.append("%s: %s '%s' is not a valid yyyy-mm-dd date" % (ref, name, got))
                elif got != want:
                    problems.append("%s: %s expected '%s' got '%s'"
                                    % (ref, name, want or "(empty)", got or "(empty)"))
            elif got != want:
                problems.append("%s: %s expected '%s' got '%s'" % (ref, name, want, got))

    for d in db:
        if d["Reference"] not in seen:
            problems.append("%s: in the database but missing from %s" % (d["Reference"], args.csv))

    if problems:
        print("FAIL: %d problem(s):" % len(problems))
        for p in problems[:10]:
            print(" -", p)
        if len(problems) > 10:
            print("  ... and %d more" % (len(problems) - 10))
        sys.exit(1)

    print("Migration check passed: %d of %d rows match. %s is valid legacy-format data."
          % (len(db), len(db), args.csv))


if __name__ == "__main__":
    main()
