# 💻 KKP Workshop: IBM Bob for Developers
### สร้าง Transaction Dashboard API + Website ด้วย AI

**ระยะเวลา:** 60 นาที  
**ระดับ:** Developer (JavaScript / Python พื้นฐาน)  
**เป้าหมาย:** ใช้ IBM Bob ช่วยสร้าง REST API + Frontend Website อ่านข้อมูล Transaction จาก CSV

---

## 📦 โครงสร้าง Workshop นี้

```
workshop-developer/
├── data/
│   └── kkp_transactions.csv     ← ข้อมูล Transaction จำลอง (35 รายการ)
└── README.md                    ← คู่มือนี้
```

**ผลลัพธ์ที่จะสร้างระหว่าง Workshop:**
```
workshop-developer/
├── api/
│   ├── server.js                ← Express.js REST API server
│   ├── package.json
│   └── data/                   ← symlink หรือ copy ของ CSV
└── frontend/
    ├── index.html               ← Dashboard website
    ├── style.css
    └── app.js                   ← Fetch API calls
```

---

## 📋 เนื้อเรื่อง Workshop

คุณเป็น Developer ที่ทีม **Digital Banking** ของ KKP  
ทีม Business ส่ง CSV มาให้ไฟล์หนึ่ง: ข้อมูล Transaction 35 รายการ  
งานของคุณคือสร้าง:

1. **REST API** — อ่าน CSV และ serve ข้อมูลผ่าน endpoints
2. **Frontend Website** — แสดง Dashboard เรียกข้อมูลจาก API

คุณจะใช้ IBM Bob เป็น pair-programmer ตลอดกระบวนการ

---

## 🚀 Workshop Prompts — ทำตามลำดับ

---

### STEP 1 — ทำความเข้าใจข้อมูลและออกแบบ API (10 นาที)

**เปิดไฟล์:** `workshop-developer/data/kkp_transactions.csv`

**Prompt 1.1 — สำรวจข้อมูล:**
```
อ่านไฟล์ workshop-developer/data/kkp_transactions.csv 
แล้วบอกฉัน:
1. โครงสร้างข้อมูล: columns และ data types ของแต่ละ column
2. ค่าที่เป็น unique ของ transaction_type, channel, status
3. มี fraud transactions (is_fraud_flag = 1) กี่รายการ?
4. ช่วยออกแบบ REST API endpoints ที่เหมาะสมสำหรับข้อมูลนี้
```

**สิ่งที่ควรได้รับ:** Bob จะแนะนำ endpoints เช่น:
- `GET /api/transactions` — รายการทั้งหมด (พร้อม filter query params)
- `GET /api/transactions/:id` — รายการเดียว
- `GET /api/summary` — สถิติภาพรวม
- `GET /api/fraud` — รายการที่ถูก flag

---

**Prompt 1.2 — ยืนยัน API Design:**
```
จากข้อมูล transaction ที่วิเคราะห์แล้ว ช่วยออกแบบ API spec ให้สมบูรณ์:
1. แสดง endpoint ทั้งหมดที่จะสร้าง พร้อม HTTP method และ description
2. ตัวอย่าง response JSON ของแต่ละ endpoint
3. Query parameters ที่รองรับ เช่น ?status=Flagged&channel=Mobile+App
4. Error response format
```

---

### STEP 2 — สร้าง REST API Server (15 นาที)

**Prompt 2.1 — สร้าง Express.js API:**
```
สร้าง REST API server ด้วย Node.js + Express.js ที่อ่านข้อมูลจาก 
workshop-developer/data/kkp_transactions.csv

สร้างไฟล์ต่อไปนี้:

1. workshop-developer/api/package.json
   - dependencies: express, csv-parser, cors
   - script: "start": "node server.js"

2. workshop-developer/api/server.js ที่มี endpoints:
   - GET /api/transactions — รายการทั้งหมด, รองรับ query params:
     ?status=Success|Failed|Flagged
     ?channel=Mobile+App|ATM|Internet+Banking|Credit+Card
     ?transaction_type=Purchase|Transfer|ATM+Withdrawal
   - GET /api/transactions/:transaction_id — รายการเดียว
   - GET /api/summary — สถิติ: total_count, total_amount, fraud_count, 
     avg_risk_score, breakdown_by_channel, breakdown_by_type
   - GET /api/fraud — เฉพาะรายการที่ is_fraud_flag=1 พร้อม risk_score

3. ใส่ CORS headers เพื่อให้ frontend เรียกได้
4. Server รันที่ port 3001
5. ใส่ console.log แสดงจำนวน transactions ที่โหลดเมื่อ start
```

---

**Prompt 2.2 — ทดสอบ API:**
```
ช่วยสร้างคำสั่ง curl สำหรับทดสอบ API ทุก endpoint 
และบอกวิธี install dependencies และ start server
```

> **ทำจริง:** เปิด Terminal แล้วรัน:
> ```bash
> cd workshop-developer/api
> npm install
> node server.js
> ```
> จากนั้นทดสอบด้วย curl หรือ browser

---

### STEP 3 — สร้าง Frontend Dashboard Website (20 นาที)

**Prompt 3.1 — สร้าง HTML Dashboard:**
```
สร้าง Frontend dashboard website ที่เรียกข้อมูลจาก API ที่รันที่ http://localhost:3001

สร้างไฟล์ต่อไปนี้:

1. workshop-developer/frontend/index.html — หน้าหลัก Dashboard
   - Title: "KKP Transaction Monitor"
   - Navigation: Overview | Transactions | Fraud Alerts
   - Section "Overview": 4 stat cards (Total Txn, Total Amount, Fraud Count, Avg Risk Score)
   - Section "Transactions": ตารางแสดงข้อมูล transaction พร้อม:
     * Filter dropdowns: Status, Channel, Transaction Type  
     * Search box สำหรับ customer name
     * Pagination (10 รายการต่อหน้า)
     * แถวที่มี is_fraud_flag=1 ให้ highlight สีแดงอ่อน
   - Section "Fraud Alerts": cards แสดง flagged transactions

2. workshop-developer/frontend/style.css
   - สไตล์ธนาคาร: สีน้ำเงิน (#003087) header, card layout, responsive
   - ตาราง: hover effect, zebra striping
   - Badge: Success=green, Failed=grey, Flagged=red

3. workshop-developer/frontend/app.js
   - ใช้ fetch() เรียก API (ไม่ใช้ library ภายนอก)
   - โหลด summary stats เมื่อเปิดหน้า
   - filter และ search ทำงานแบบ real-time
   - จัดการ error กรณี API ไม่ตอบสนอง (แสดง message)
```

---

**Prompt 3.2 — เพิ่ม Chart:**
```
เพิ่ม Chart.js (CDN) ใน index.html แล้วสร้าง charts ในส่วน Overview:
1. Donut chart: สัดส่วน Transaction ตาม Channel (Mobile App, ATM, Internet Banking, Credit Card)
2. Bar chart: จำนวน Transaction ตาม Status (Success, Failed, Flagged)
3. ข้อมูลของ charts ให้ดึงมาจาก GET /api/summary endpoint

อัปเดต app.js เพื่อ render charts หลังจากโหลด summary data สำเร็จ
```

---

### STEP 4 — Debug และปรับปรุง (10 นาที)

**Prompt 4.1 — แก้บัค (ใช้เมื่อเจอปัญหา):**
```
ฉันรัน server แล้วเจอ error นี้: [วาง error message ที่นี่]
ช่วย debug และแก้ไขให้หน่อย
```

**Prompt 4.2 — เพิ่ม Feature:**
```
เพิ่ม endpoint ใหม่ใน API:
GET /api/transactions/export/csv 
— ให้ดาวน์โหลด CSV เฉพาะ transactions ที่ถูก filter 
  (รองรับ query params เดิม: status, channel, transaction_type)

และเพิ่มปุ่ม "Export CSV" ในหน้า Transactions ของ frontend
เมื่อกดให้ส่ง request ด้วย filter ปัจจุบันที่เลือกอยู่
```

---

### STEP 5 (Bonus) — API Documentation (5 นาที)

**Prompt 5.1 — สร้าง API Docs:**
```
สร้างไฟล์ workshop-developer/api/API_DOCS.md 
อธิบาย API ทุก endpoint แบบ developer-friendly:
- Method + URL
- Description
- Query Parameters (ถ้ามี)
- Example Request (curl)
- Example Response (JSON)

และสร้าง workshop-developer/frontend/api-test.html 
เป็นหน้า API Tester อย่างง่าย: มี dropdown เลือก endpoint, กด Send แล้วแสดง JSON response
```

---

## 🔧 Environment Setup

ก่อนเริ่ม ตรวจสอบว่ามี Node.js ติดตั้งแล้ว:
```bash
node --version   # ต้องการ v18+
npm --version
```

หากยังไม่มี สามารถถาม Bob:
```
ฉันต้องการติดตั้ง Node.js บน macOS ช่วยบอกวิธีทำ
```

---

## 💡 Tips สำหรับ Developer

| สถานการณ์ | Prompt ที่แนะนำ |
|-----------|----------------|
| อยากเข้าใจ code ที่ Bob สร้าง | "อธิบาย server.js ให้ฉันเข้าใจทีละ function" |
| ต้องการเพิ่ม authentication | "เพิ่ม API key authentication ใน middleware ของ Express" |
| อยากใช้ TypeScript | "แปลง server.js เป็น TypeScript พร้อม type definitions" |
| ต้องการ Docker | "สร้าง Dockerfile สำหรับ run API server นี้" |
| อยาก deploy | "สร้าง docker-compose.yml ที่รัน API และ serve frontend พร้อมกัน" |
| มี CORS error | "แก้ CORS error ใน Express ให้รองรับ request จาก http://localhost:8080" |

---

## 🏃 วิธีรัน Workshop นี้หลัง Build เสร็จ

```bash
# Terminal 1 — รัน API Server
cd workshop-developer/api
npm install
node server.js
# API พร้อมที่ http://localhost:3001

# Terminal 2 — เปิด Frontend
# เปิดไฟล์ workshop-developer/frontend/index.html ใน browser
# หรือใช้ live-server:
npx live-server workshop-developer/frontend --port=8080
```

**ทดสอบ API:**
```bash
# ดู transactions ทั้งหมด
curl http://localhost:3001/api/transactions

# Filter เฉพาะ Flagged
curl "http://localhost:3001/api/transactions?status=Flagged"

# ดู summary
curl http://localhost:3001/api/summary

# ดู fraud alerts
curl http://localhost:3001/api/fraud
```

---

## 🎯 สิ่งที่ควรได้หลัง Workshop นี้

- [ ] `api/server.js` — Express API server อ่าน CSV ได้จริง
- [ ] `api/package.json` — dependencies ครบ
- [ ] `frontend/index.html` — Dashboard แสดงข้อมูลจาก API
- [ ] `frontend/style.css` — สไตล์ธนาคาร พร้อมใช้งาน
- [ ] `frontend/app.js` — Fetch API, filter, charts ทำงานได้
- [ ] เข้าใจ workflow: CSV → API → Frontend ที่ Bob ช่วยสร้างทั้งหมด

---

## 📚 Technology Stack ที่ใช้

| ส่วน | Technology | เหตุผล |
|------|-----------|--------|
| API Server | Node.js + Express | เบา, ติดตั้งง่าย, npm ใช้งานง่าย |
| CSV Parsing | csv-parser | stream-based, รองรับ CSV ใหญ่ |
| CORS | cors middleware | ให้ browser เรียก API ได้ |
| Frontend | Vanilla JS + fetch() | ไม่มี dependency, ง่ายต่อการเข้าใจ |
| Charts | Chart.js (CDN) | ไม่ต้อง install, ใช้งานง่าย |

---

*KKP x IBM Bob Workshop · Developer Track · June 2024*
