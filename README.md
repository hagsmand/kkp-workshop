# 🎓 KKP x IBM Bob Workshop
### AI-Powered Productivity — Hands-on Lab Guide

> **Session Duration:** 2 hours total · Part of KKP AI Inflection Point Workshop  
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
└── workshop-developer/              ← 💻 Track สำหรับ Developers
    ├── README.md                    ← คู่มือ Workshop พร้อม Prompts
    └── data/
        └── kkp_transactions.csv     ← ข้อมูล Transaction จำลอง (35 รายการ)
```

---

## 🗺️ เลือก Track ของคุณ

| | 🏦 Business Track | 💻 Developer Track |
|---|---|---|
| **เหมาะสำหรับ** | Analyst, Risk, Compliance, HR | Developer, IT, Data Engineer |
| **ไม่ต้องใช้** | เขียนโค้ด | — |
| **เครื่องมือหลัก** | IBM Bob + CSV data | IBM Bob + Node.js |
| **ผลลัพธ์** | HTML Dashboard, Word Report, PowerPoint | REST API + Dashboard Website |
| **เวลา** | 60 นาที | 60 นาที |
| **เปิดคู่มือ** | [workshop-business/README.md](workshop-business/README.md) | [workshop-developer/README.md](workshop-developer/README.md) |

---

## ⚡ เริ่มต้นอย่างรวดเร็ว

### สำหรับ Business Users
1. เปิด IBM Bob ใน workspace นี้
2. ติดตั้ง Skills: `pptx`, `office-insights` จาก Marketplace
3. เปิด [`workshop-business/README.md`](workshop-business/README.md) และทำตาม Step 1–5

### สำหรับ Developers
1. ตรวจสอบ Node.js: `node --version` (ต้องการ v18+)
2. เปิด IBM Bob ใน workspace นี้
3. เปิด [`workshop-developer/README.md`](workshop-developer/README.md) และทำตาม Step 1–5

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

### 💻 Developer — `kkp_transactions.csv`
ข้อมูล Financial Transaction จำลอง 35 รายการ ครอบคลุม:
- **ประเภท:** Purchase, Transfer, ATM Withdrawal
- **ช่องทาง:** Mobile App, Internet Banking, ATM, Credit Card
- **สถานะ:** Success (29), Flagged (4), Failed (2)
- **Security:** Fraud Flag, Risk Score (0–100), Auth Method
- **Tech:** Device Type, Channel, Timestamp

---

## 🎯 Learning Objectives

### Business Track
- [ ] ใช้ Bob วิเคราะห์ข้อมูล CSV โดยไม่ต้องเขียนโค้ด
- [ ] สร้าง Interactive HTML Dashboard ที่มี Charts
- [ ] สร้างรายงาน Word Document (.docx) แบบมืออาชีพ
- [ ] สร้าง PowerPoint Presentation (.pptx) พร้อมนำเสนอ
- [ ] เข้าใจการใช้ Skills ใน IBM Bob Marketplace

### Developer Track
- [ ] ใช้ Bob เป็น Pair Programmer ออกแบบ API
- [ ] สร้าง Express.js REST API อ่าน CSV ได้จริง
- [ ] สร้าง Frontend Website เรียก API ด้วย fetch()
- [ ] เพิ่ม Filter, Search, และ Charts
- [ ] Debug และ Extend โค้ดด้วยความช่วยเหลือจาก Bob

---

## 🔑 IBM Bob Skills ที่ใช้ใน Workshop

| Skill | Track | วิธีติดตั้ง |
|-------|-------|------------|
| `pptx` | Business | Marketplace → ค้นหา "pptx" → Install |
| `office-insights` | Business | Marketplace → ค้นหา "office" → Install |
| `xlsx` | Business (optional) | Marketplace → ค้นหา "xlsx" → Install |
| `use-uv` | Developer (Python option) | Marketplace → ค้นหา "uv" → Install |

---

*KKP x IBM Bob Workshop · June 2024 · Prepared by KKP Digital Innovation Team*
