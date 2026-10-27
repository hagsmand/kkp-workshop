#!/usr/bin/env python3
"""Show the schema and sample rows of pqtracker.sqlite.

Run from workshop-challenge/data-migration/:

    python3 inspect_db.py
"""
import os
import sqlite3

if not os.path.exists("pqtracker.sqlite"):
    raise SystemExit("pqtracker.sqlite not found - build it first with: python3 build_db.py")
con = sqlite3.connect("pqtracker.sqlite")
print("== tables ==")
for name, sql in con.execute("SELECT name, sql FROM sqlite_master WHERE type='table'"):
    print(sql, ";\n", sep="")
print("== row count ==")
print(con.execute("SELECT COUNT(*) FROM parliamentary_questions").fetchone()[0], "records")
print("== first 3 records ==")
for row in con.execute("SELECT reference, mp_name, pq_type, date_received, deadline,"
                       " assigned_team, status, date_answered"
                       " FROM parliamentary_questions ORDER BY reference LIMIT 3"):
    print(" ", row)
con.close()
