#!/usr/bin/env python3
"""Build pqtracker.sqlite — the mockup database for the data-migration exercise.

Run from workshop-challenge/data-migration/:

    python3 build_db.py

Why a generator instead of a committed file: the Block Secrets pre-commit
scanner blocks binary SQLite files from the repository, and a generated
fixture keeps the data reviewable in git. The build is deterministic —
the same 12 synthetic rows every time (fictional MPs and questions; the
first 8 are the legacy app's own sample data from Form1.vb).
"""
import os
import sqlite3

DB = "pqtracker.sqlite"

SCHEMA = """
CREATE TABLE parliamentary_questions (
  reference      TEXT PRIMARY KEY,
  mp_name        TEXT NOT NULL,
  question_text  TEXT NOT NULL,
  pq_type        TEXT NOT NULL CHECK (pq_type IN ('Written','Oral','Priority Written')),
  date_received  TEXT NOT NULL,
  deadline       TEXT NOT NULL,
  assigned_team  TEXT NOT NULL,
  status         TEXT NOT NULL,
  response_text  TEXT NOT NULL DEFAULT '',
  date_answered  TEXT
);
CREATE TABLE meta (key TEXT PRIMARY KEY, value TEXT NOT NULL);
"""

ROWS = [
    ("PQ-20240315-001", "John Smith MP", "What is the department budget for 2024?", "Written", "2024-03-15", "2024-03-26", "Policy Team A", "Received", "", None),
    ("PQ-20240316-001", "Sarah Jones MP", "How many staff work in the department?", "Oral", "2024-03-16", "2024-03-21", "Policy Team B", "Assigned", "", None),
    ("PQ-20240317-001", "Michael Brown MP", "What are the key priorities for this year?", "Priority Written", "2024-03-17", "2024-03-22", "Private Office", "Drafted", "Draft response prepared", None),
    ("PQ-20240318-001", "Emma Wilson MP", "How much was spent on IT last year?", "Written", "2024-03-18", "2024-03-29", "Policy Team A", "Approved", "Response approved by Minister", "2024-03-28"),
    ("PQ-20240319-001", "David Taylor MP", "What is the department doing about climate change?", "Written", "2024-03-19", "2024-04-02", "Policy Team B", "Answered", "The department is committed to reducing emissions by 50% by 2030", "2024-04-01"),
    ("PQ-20240320-001", "Lisa Anderson MP", "How many FOI requests were received last year?", "Oral", "2024-03-20", "2024-03-25", "Private Office", "Received", "", None),
    ("PQ-20240321-001", "Robert Clark MP", "What training is provided to new staff?", "Priority Written", "2024-03-21", "2024-03-26", "Policy Team A", "Overdue", "", None),
    ("PQ-20240322-001", "Helen Davis MP", "What is the department policy on remote working?", "Written", "2024-03-22", "2024-04-04", "Policy Team B", "Assigned", "", None),
    ("PQ-20240325-001", "Priya Nair MP", "How many apprenticeships were created last year?", "Written", "2024-03-25", "2024-04-03", "Policy Team A", "Answered", "The department funded 1250 apprenticeships in the last financial year", "2024-04-02"),
    ("PQ-20240325-002", "Tom Walker MP", "What is the budget for road maintenance this year?", "Oral", "2024-03-25", "2024-03-28", "Private Office", "Drafted", "", None),
    ("PQ-20240326-001", "Aisha Khan MP", "How many staff work remotely each week?", "Written", "2024-03-26", "2024-04-04", "Policy Team B", "Received", "", None),
    ("PQ-20240326-002", "George Finch MP", "What is the plan for digital services?", "Priority Written", "2024-03-26", "2024-03-29", "Private Office", "Answered", "The plan sets three priorities for digital service delivery by 2027", "2024-03-28"),
]

META = [("app", "PQTracker modernised store (mockup)"), ("schema_version", "1")]


def build(path=DB):
    if os.path.exists(path):
        os.remove(path)
    con = sqlite3.connect(path)
    try:
        con.executescript(SCHEMA)
        con.executemany("INSERT INTO parliamentary_questions VALUES (?,?,?,?,?,?,?,?,?,?)", ROWS)
        con.executemany("INSERT INTO meta VALUES (?,?)", META)
        con.commit()
        return con.execute("SELECT COUNT(*) FROM parliamentary_questions").fetchone()[0]
    finally:
        con.close()


if __name__ == "__main__":
    count = build()
    print("Built %s: %d records (deterministic fixture — no real data)" % (DB, count))
