---
name: office-insights
description: "Use when the user wants to read, edit, create, or inspect a .docx, .xlsx, or .pptx file using the office_read and office_edit tools."
---

# Office Insights Skill

Use the built-in `office_read` and `office_edit` tools to work with Office files (.docx, .xlsx, .pptx).

## Reading

| mode | ใช้เมื่อ |
|------|---------|
| `text` | ดึงเนื้อหาทั้งหมดเป็น plain text |
| `outline` | ดูโครงสร้าง JSON ของไฟล์ |
| `get` + `query` | ดึง element เฉพาะจุด เช่น `/body/p[1]` |
| `validate` | ตรวจสอบ OpenXML schema |

## Editing

- ใช้ `operation: batch` เสมอเมื่อเพิ่มหลาย element พร้อมกัน
- `.docx` — path format: `/body/p[N]`, `/body/tbl[N]`
- `.pptx` — path format: `/slide[N]/shape[N]`
- `.xlsx` — path format: `/SheetName/A1`

## กฎสำคัญ

- **Serial calls only** — ห้ามเรียก `office_read` หรือ `office_edit` พร้อมกันหลายครั้ง ต้องรอให้แต่ละ call เสร็จก่อน
- สำหรับ .docx ให้ใช้ `office_edit` แทนการเขียน XML โดยตรง
