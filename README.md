# 🎓 KKP x IBM Bob Workshop
### AI-Powered Productivity — Hands-on Lab Guide

> **Session Duration:** Business track 60 minutes; developer track 60 minutes; configuration track 60 minutes (separate sessions).
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
├── workshop-developer/              ← 💻 Track สำหรับ Developers (60 นาที)
│   ├── index.html                   ← คู่มือ Workshop หน้าเดียว (เปิดในเบราว์เซอร์)
│   ├── lab/                         ← Settlement Desk lab ที่มีบั๊ก (แก้ได้เฉพาะ server.mjs)
│   ├── facilitator/                 ← เฉพาะ facilitator (ห้ามแจกจ่าย)
│   └── dist/                        ← participant ZIP ที่สร้างตอนแจกจ่าย
│
└── workshop-config/                 ← ⚙️ Track สำหรับ Bob Configuration (60 นาที)
    ├── index.html                   ← คู่มือ Workshop หน้าเดียว (เปิดในเบราว์เซอร์)
    ├── lab/                         ← Workspace ที่ .bob config ถูกวางยา (คืนค่าจาก backups/)
    ├── facilitator/                 ← เฉพาะ facilitator (ห้ามแจกจ่าย)
    └── dist/                        ← participant ZIP ที่สร้างตอนแจกจ่าย
```

---

## 🗺️ เลือก Track ของคุณ

| | 🏦 Business Track | 💻 Developer Track | ⚙️ Config Track |
|---|---|---|---|
| **เหมาะสำหรับ** | Analyst, Risk, Compliance, HR | Developer, IT, Data Engineer | Developer, IT, Platform/DevOps |
| **ไม่ต้องใช้** | เขียนโค้ด | — | — |
| **เครื่องมือหลัก** | IBM Bob + CSV data | IBM Bob + Node.js | IBM Bob + Node.js (verify) |
| **ผลลัพธ์** | HTML Dashboard, Word Report, PowerPoint | Repaired settlement job, 5/5 acceptance tests green, winning word | Restored .bob configuration, 5/5 challenges green, winning word |
| **เวลา** | 60 นาที | 60 นาที | 60 นาที |
| **เปิดคู่มือ** | [workshop-business/README.md](workshop-business/README.md) | [workshop-developer/index.html](workshop-developer/index.html) | [workshop-config/index.html](workshop-config/index.html) |

---

## ⚡ เริ่มต้นอย่างรวดเร็ว

### สำหรับ Business Users
1. เปิด IBM Bob ใน workspace นี้
2. ติดตั้ง Skills: `pptx`, `office-insights` จาก Marketplace
3. เปิด [`workshop-business/README.md`](workshop-business/README.md) และทำตาม Step 1–5

### สำหรับ Developers
1. ตรวจสอบ Node.js: `node --version` (ต้องการ v20+) และเตรียม IBM Bob Base โดยไม่ใช้ Premium packages
2. เปิด [`workshop-developer/index.html`](workshop-developer/index.html) ในเบราว์เซอร์ — คู่มือทั้งหมดอยู่ในหน้าเดียว (60 นาที)
3. เปิดโฟลเดอร์ `workshop-developer/` ใน Bob แล้วทำตาม Step 1–5 ในหน้าคู่มือ (แก้ได้เฉพาะ `lab/server.mjs`)
4. ไม่มี npm dependencies, credentials, production access หรือ external application services; Bob inference ยังต้องใช้ service connection ที่เตรียมไว้

### สำหรับ Bob Configuration
1. ตรวจสอบ Node.js: `node --version` (ต้องการ v20+) และเตรียม IBM Bob Base โดยไม่ใช้ Premium packages
2. เปิด [`workshop-config/index.html`](workshop-config/index.html) ในเบราว์เซอร์ — คู่มือทั้งหมดอยู่ในหน้าเดียว (60 นาที)
3. เปิดโฟลเดอร์ `workshop-config/` ใน Bob แล้วทำตาม Challenge 1–5 ในหน้าคู่มือ (คืนค่า config จาก `lab/backups/` เท่านั้น)
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

### 💻 Developer — Settlement Desk incident
แอป settlement จำลอง (10 รายการ, 3 ร้านค้า) ที่ deploy v2.3.0 ทำให้จ่ายเงินผิด 3 อาการ: จ่ายซ้ำ, จ่ายรายการนอกวันทำการ และยอดต่าง 1 satang
ผู้เรียนใช้ Bob Ask สืบหาสาเหตุจากหลักฐาน อนุมัติ diff ผ่าน Bob Agent ผ่าน acceptance tests 5 ข้อ และเก็บตัวอักษรจากทั้ง 3 challenge ประกอบเป็น winning word
เหตุการณ์เป็นเรื่องสมมติ ไม่ใช่ incident จริงของ KKP; starter ตั้งใจให้ tests ไม่ผ่าน

### ⚙️ Config — Bob configuration incident
Workspace ช่วยเหลือร้านค้าที่ deploy v2.3.0 วางยา .bob configuration 5 จุด: โหมด Ask ไม่มี tools, ไฟล์ถูกซ่อนด้วย .bobignore, rules หลอกทั้งโหมดเฉพาะและทั่วไป และ hook แทรก context ปลอม
ผู้เรียนวินิจฉัยอาการผ่าน Bob ตาม official configuration docs คืนค่า byte-exact จาก backups และเก็บตัวอักษรจากทั้ง 5 challenge ประกอบเป็น winning word
เหตุการณ์เป็นเรื่องสมมติ อาการอ้างอิงจากพฤติกรรมที่ document ไว้ใน IBM Bob docs; starter ตั้งใจให้ verify ไม่ผ่าน

---

## 🎯 Learning Objectives

### Business Track
- [ ] ใช้ Bob วิเคราะห์ข้อมูล CSV โดยไม่ต้องเขียนโค้ด
- [ ] สร้าง Interactive HTML Dashboard ที่มี Charts
- [ ] สร้างรายงาน Word Document (.docx) แบบมืออาชีพ
- [ ] สร้าง PowerPoint Presentation (.pptx) พร้อมนำเสนอ
- [ ] เข้าใจการใช้ Skills ใน IBM Bob Marketplace

### Developer Track
- [ ] ใช้ Bob Ask เปรียบเทียบสมมติฐานกับหลักฐาน ไม่ใช่เดาสาเหตุ
- [ ] ตรวจและอนุมัติ diff ของ Bob Agent ก่อนทำจริง
- [ ] ซ่อม settlement job ให้จ่ายเงินถูกต้องครบทั้ง 3 challenge
- [ ] พิสูจน์ผลด้วย acceptance tests 5 ข้อที่รันจริง และ winning word ที่ประกอบจากตัวอักษรของทั้ง 3 challenge

### Config Track
- [ ] วินิจฉัยอาการของ Bob แล้วโยงกับ configuration surface ที่ document ไว้ใน official docs
- [ ] เข้าใจ .bob/rules/, rules-{mode}/, AGENTS.md, custom_modes.yaml, .bobignore และ settings.json hooks
- [ ] คืนค่า configuration จาก backup แบบ byte-exact และพิสูจน์ผลด้วย verify script ที่รันจริง
- [ ] เก็บตัวอักษรจากทั้ง 5 challenge ประกอบเป็น winning word

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
