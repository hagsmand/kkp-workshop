# 🎓 KKP x IBM Bob Workshop
### AI-Powered Productivity — Hands-on Lab Guide

> **Session Duration:** Business track 60 minutes; developer track 60 minutes (separate sessions).
> **Facilitator:** KKP Digital Innovation Team

---

## 📂 Workshop Structure

```
kkp-workshop/
├── README.md                        ← คู่มือนี้ (ภาพรวม)
│
├── workshop-business/               ← 🏦 Track สำหรับ Business Users
│   ├── README.md                    ← คู่มือ Workshop พร้อม Prompts
│   └── data/
│       ├── kkp_loan_data.csv        ← ข้อมูลสินเชื่อจำลอง (30 รายการ)
│       └── kkp_employee_data.csv    ← ข้อมูลพนักงานจำลอง (20 คน)
│
└── workshop-developer/              ← 💻 Track สำหรับ Developers (60 นาที)
    ├── index.html                   ← คู่มือ Workshop หน้าเดียว (เปิดในเบราว์เซอร์)
    ├── lab/                         ← Settlement Desk lab + .bob config ที่ถูกวางยา
    ├── facilitator/                 ← เฉพาะ facilitator (ห้ามแจกจ่าย)
    └── dist/                        ← participant ZIP ที่สร้างตอนแจกจ่าย
```

---

## 🗺️ เลือก Track ของคุณ

| | 🏦 Business Track | 💻 Developer Track |
|---|---|---|
| **เหมาะสำหรับ** | Analyst, Risk, Compliance, HR | Developer, IT, Data Engineer |
| **ไม่ต้องใช้** | เขียนโค้ด | — |
| **เครื่องมือหลัก** | IBM Bob + CSV data | IBM Bob + Node.js |
| **ผลลัพธ์** | HTML Dashboard, Word Report, PowerPoint | Restored Bob configuration + repaired settlement job, 8/8 challenges green, two winning words |
| **เวลา** | 60 นาที | 60 นาที |
| **เปิดคู่มือ** | [workshop-business/README.md](workshop-business/README.md) | [workshop-developer/index.html](workshop-developer/index.html) |

---

## ⚡ เริ่มต้นอย่างรวดเร็ว

### สำหรับ Business Users
1. เปิด IBM Bob ใน workspace นี้
2. ติดตั้ง Skills: `pptx`, `office-insights` จาก Marketplace
3. เปิด [`workshop-business/README.md`](workshop-business/README.md) และทำตาม Step 1–5

### สำหรับ Developers
1. ตรวจสอบ Node.js: `node --version` (ต้องการ v20+) และเตรียม IBM Bob Base โดยไม่ใช้ Premium packages
2. เปิด [`workshop-developer/index.html`](workshop-developer/index.html) ในเบราว์เซอร์ — คู่มือทั้งหมดอยู่ในหน้าเดียว (60 นาที)
3. เปิดโฟลเดอร์ `workshop-developer/lab/` ใน Bob แล้วทำตาม Part 1 (คืนค่า .bob config จาก `backups/`) และ Part 2 (แก้ได้เฉพาะ `server.mjs`) ในหน้าคู่มือ
4. ไม่มี npm dependencies, credentials, production access หรือ external application services; Bob inference ยังต้องใช้ service connection ที่เตรียมไว้

---

## 📊 ข้อมูลจำลองในแต่ละ Track

### 🏦 Business — `kkp_loan_data.csv`
ข้อมูลสินเชื่อลูกค้า KKP จำลอง 30 รายการ ครอบคลุม:
- **ประเภทสินเชื่อ:** SME Loan, Personal Loan, Home Loan, Agricultural Loan
- **ข้อมูลลูกค้า:** อายุ, อาชีพ, รายได้, Credit Score
- **สถานะ:** Approved (25), Rejected (5)
- **ความเสี่ยง:** days_overdue, default_flag, interest_rate
- **ภูมิศาสตร์:** กระจาย 15 จังหวัดทั่วประเทศ

### 🏦 Business — `kkp_employee_data.csv`  
ข้อมูลพนักงาน KKP จำลอง 20 คน ครอบคลุม:
- **แผนก:** Risk, Retail Banking, Technology, Compliance, Finance, HR, Investment
- **ตัวชี้วัด:** Performance Score, Training Hours, AI Tool Usage Score
- **HR Data:** Salary, Satisfaction Score, Promotion Eligibility

### 💻 Developer — Double incident (code + Bob configuration)
Workspace เดียวที่ deploy v2.3.0 ทำลายสองอย่างพร้อมกัน: settlement job จ่ายเงินผิด 3 อาการ (จ่ายซ้ำ, นอกวันทำการ, ต่าง 1 satang) และ .bob configuration ถูกวางยา 5 จุด (โหมด Ask ไม่มี tools, ไฟล์ถูกซ่อน, rules หลอก, hook แทรก context ปลอม)
ผู้เรียนคืนค่า config แบบ byte-exact จาก backups ตาม official configuration docs ก่อน แล้วใช้ Bob ที่กลับมาเชื่อถือได้สืบหาและซ่อมโค้ดผ่าน acceptance tests 5 ข้อ เก็บตัวอักษร 8 ตัวประกอบเป็น winning words สองคำ
เหตุการณ์เป็นเรื่องสมมติ อาการ config อ้างอิงจากพฤติกรรมที่ document ไว้ใน IBM Bob docs; starter ตั้งใจให้ tests และ verify ไม่ผ่าน

---

## 🎯 Learning Objectives

### Business Track
- [ ] ใช้ Bob วิเคราะห์ข้อมูล CSV โดยไม่ต้องเขียนโค้ด
- [ ] สร้าง Interactive HTML Dashboard ที่มี Charts
- [ ] สร้างรายงาน Word Document (.docx) แบบมืออาชีพ
- [ ] สร้าง PowerPoint Presentation (.pptx) พร้อมนำเสนอ
- [ ] เข้าใจการใช้ Skills ใน IBM Bob Marketplace

### Developer Track
- [ ] วินิจฉัยอาการของ Bob แล้วโยงกับ configuration surface ที่ document ไว้ใน official docs
- [ ] เข้าใจ .bob/rules/, rules-{mode}/, AGENTS.md, custom_modes.yaml, .bobignore และ settings.json hooks
- [ ] ใช้ Bob Ask เปรียบเทียบสมมติฐานกับหลักฐาน และอนุมัติ diff ของ Bob Agent ก่อนทำจริง
- [ ] คืนค่า configuration แบบ byte-exact และซ่อม settlement job ให้ถูกต้องครบทั้ง 8 challenge
- [ ] พิสูจน์ผลด้วย acceptance tests และ verify script ที่รันจริง และ winning words สองคำจากตัวอักษรทั้ง 8 ตัว

---

## 🔑 IBM Bob Skills ที่ใช้ใน Workshop

| Skill | Track | วิธีติดตั้ง |
|-------|-------|------------|
| `pptx` | Business | Marketplace → ค้นหา "pptx" → Install |
| `office-insights` | Business | Marketplace → ค้นหา "office" → Install |
| `xlsx` | Business (optional) | Marketplace → ค้นหา "xlsx" → Install |
| ไม่ต้องติดตั้ง Skills เพิ่ม | Developer | ใช้ core Ask / Plan / Agent; ไม่ใช้ Premium, MCP หรือ trial |

---

*KKP x IBM Bob Workshop · October 2026 · Prepared by KKP Digital Innovation Team*
