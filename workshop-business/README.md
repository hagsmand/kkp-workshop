# 🏦 KKP Workshop: IBM Bob for Business Users
### วิเคราะห์ข้อมูลสินเชื่อด้วย AI — ผลลัพธ์ 3 รูปแบบ

**ระยะเวลา:** 60 นาที  
**ระดับ:** Business User (ไม่ต้องเขียนโค้ด)  
**เป้าหมาย:** ใช้ IBM Bob วิเคราะห์ข้อมูลสินเชื่อและผลิตรายงานใน 3 รูปแบบ

---

## 📦 โครงสร้าง Workshop นี้

```
workshop-business/
├── data/
│   ├── kkp_loan_data.csv       ← ข้อมูลสินเชื่อจำลอง (30 รายการ)
│   └── kkp_employee_data.csv   ← ข้อมูลพนักงาน KKP จำลอง (20 คน)
└── README.md                   ← คู่มือนี้
```

---

## 🛠️ การเตรียม Skills ก่อนเริ่ม

Skills คือชุดคำแนะนำพิเศษที่สอน Bob วิธีสร้างไฟล์รูปแบบต่างๆ ได้อย่างถูกต้อง

### Skills ที่ต้องใช้ใน Workshop นี้

| Skill | ใช้ทำอะไร |
|-------|-----------|
| **pptx** | สร้างและแก้ไขไฟล์ PowerPoint (.pptx) |
| **xlsx** | อ่านและสร้างไฟล์ Excel (.xlsx) |
| **office-insights** | อ่าน/แก้ไข .docx, .xlsx, .pptx ผ่าน OpenXML |

---

### Skills ที่รวมอยู่ใน Workshop นี้แล้ว ✅

Skills ถูกติดตั้งไว้ใน project นี้แล้วที่ `.bob/skills/` ไม่ต้องติดตั้งเพิ่ม

```
.bob/
└── skills/
    ├── pptx/
    │   └── SKILL.md        ← สร้างไฟล์ PowerPoint ด้วย pptxgenjs
    └── office-insights/
        └── SKILL.md        ← อ่านและแก้ไข .docx / .xlsx / .pptx
```

> **หมายเหตุ:** Skills เหล่านี้ถูกโหลดอัตโนมัติเมื่อ Bob ตรวจพบว่าคุณต้องการไฟล์นั้น เช่น พอคุณพูดว่า "สร้าง PowerPoint" Bob จะใช้ pptx skill โดยอัตโนมัติ

---

## 📋 เนื้อเรื่อง Workshop

คุณเป็นทีม **Credit Risk Analytics** ของ KKP  
คุณได้รับ CSV ข้อมูลสินเชื่อลูกค้า 30 ราย และต้องการ:

1. เข้าใจภาพรวมพอร์ตสินเชื่อ (สัดส่วนประเภทสินเชื่อ, การกระจายตามจังหวัด)
2. ระบุความเสี่ยง (ลูกค้าที่มีหนี้ค้างชำระ, Credit Score ต่ำ)
3. นำเสนอผลให้ผู้บริหาร ใน 3 รูปแบบ

---

## 🚀 Workshop Prompts — ทำตามลำดับ

---

### STEP 1 — อ่านและทำความเข้าใจข้อมูล (5 นาที)

**เปิดไฟล์:** `workshop-business/data/kkp_loan_data.csv`

**Prompt 1.1 — สำรวจข้อมูลเบื้องต้น:**
```
อ่านไฟล์ workshop-business/data/kkp_loan_data.csv 
แล้วสรุปให้ฉันเข้าใจว่าข้อมูลนี้มีอะไรบ้าง มีกี่แถว กี่คอลัมน์ 
และบอกสิ่งที่น่าสนใจเบื้องต้นที่เห็นในข้อมูล
```

**สิ่งที่ควรได้รับ:** Bob จะอ่านไฟล์และสรุปโครงสร้างข้อมูล พร้อมสิ่งที่สังเกตเห็น

---

**Prompt 1.2 — วิเคราะห์ภาพรวมพอร์ต:**
```
จากข้อมูลสินเชื่อ kkp_loan_data.csv วิเคราะห์ให้ฉัน:
1. สัดส่วนของประเภทสินเชื่อ (loan_type) แต่ละประเภท
2. จำนวนและมูลค่าสินเชื่อรวมแยกตามจังหวัด (province) Top 5
3. อัตราการปฏิเสธสินเชื่อ (rejection rate) 
4. มูลค่าสินเชื่อเฉลี่ยแยกตามอาชีพ (occupation)
```

---

### STEP 2 — วิเคราะห์ความเสี่ยง (10 นาที)

**Prompt 2.1 — ระบุลูกค้าความเสี่ยงสูง:**
```
จากข้อมูลสินเชื่อ kkp_loan_data.csv ช่วยระบุลูกค้าที่มีความเสี่ยงสูง โดย:
1. แสดงรายชื่อลูกค้าที่มี days_overdue > 0 พร้อมรายละเอียด
2. วิเคราะห์ pattern ของลูกค้าที่มี default_flag = 1
3. บอกว่า credit_score เฉลี่ยของลูกค้าที่ผิดนัดชำระ เทียบกับที่ไม่ผิดนัดชำระเป็นอย่างไร
4. สรุปปัจจัยเสี่ยงที่พบ
```

**Prompt 2.2 — เปรียบเทียบกลุ่ม:**
```
วิเคราะห์ความสัมพันธ์ระหว่าง credit_score กับ loan_amount_thb 
และ interest_rate_pct กับ credit_score 
แล้วสรุปข้อค้นพบที่สำคัญ 3 ข้อสำหรับทีม Risk Management
```

---

### STEP 3 — สร้าง Interactive HTML Dashboard (15 นาที)

**Prompt 3.1 — สร้าง HTML Report:**
```
จากการวิเคราะห์ข้อมูลสินเชื่อ kkp_loan_data.csv ทั้งหมด 
สร้างไฟล์ HTML แบบ interactive dashboard ที่มี:
1. หัวเรื่อง "KKP Credit Portfolio Dashboard — May 2024"
2. Summary cards: จำนวนสินเชื่อทั้งหมด, มูลค่ารวม, อัตรา Default, Credit Score เฉลี่ย
3. Pie chart แสดงสัดส่วนประเภทสินเชื่อ (ใช้ Chart.js จาก CDN)
4. Bar chart แสดง Top 5 จังหวัดตามมูลค่าสินเชื่อ
5. ตารางแสดงลูกค้าความเสี่ยงสูง (days_overdue > 0) พร้อม highlight สีแดง
6. ดีไซน์สไตล์ธนาคาร สีน้ำเงิน-ขาว ดูเป็นมืออาชีพ

บันทึกเป็นไฟล์ workshop-business/output/kkp_credit_dashboard.html
```

> **Tip:** Bob จะสร้างไฟล์ HTML พร้อม Chart.js ให้อัตโนมัติ หลังจากนั้นเปิดไฟล์ใน browser เพื่อดูผลลัพธ์

---

### 💬 เริ่ม Chat Session ใหม่ก่อน STEP 4

> Skills จะโหลดได้เต็มประสิทธิภาพเมื่อเริ่ม session ใหม่เท่านั้น

1. กดปุ่ม **New Chat** (ไอคอน ✏️ หรือ `+` มุมบนซ้าย) เพื่อเปิด session ใหม่
2. เรียก skill `office-insights` ด้วย slash command:
   ```
   /office-insights
   ```
3. Bob จะยืนยันว่าโหลด skill สำเร็จ — จากนั้นทำ STEP 4 ได้เลย

---

### STEP 4 — สร้าง Word Document Report (15 นาที)

**Prompt 4.1 — สร้าง DOCX:**
```
สร้างรายงาน Word (.docx) สรุปผลการวิเคราะห์สินเชื่อ โดยบันทึกเป็น 
workshop-business/output/kkp_credit_report.docx

รายงานควรมีหัวข้อดังนี้:
1. บทสรุปผู้บริหาร (Executive Summary) — ย่อหน้า 3-4 บรรทัด
2. ภาพรวมพอร์ตสินเชื่อ — ตารางแสดงสถิติสำคัญ
3. การวิเคราะห์ความเสี่ยง — รายชื่อลูกค้าความเสี่ยงสูงในตาราง
4. ข้อเสนอแนะ — bullet points อย่างน้อย 3 ข้อ
5. ข้อมูลทั่วไป: จัดทำโดย KKP Credit Risk Team, วันที่ 1 มิถุนายน 2567

ใช้ font Calibri, หัวข้อสีน้ำเงินเข้ม, มีเส้นแบ่งหัวข้อ
```

> **หมายเหตุ:** Bob จะใช้ office-insights skill เพื่อสร้างไฟล์ .docx ที่ถูกต้องตามมาตรฐาน OpenXML

---

### 💬 เริ่ม Chat Session ใหม่ก่อน STEP 5

> Skills จะโหลดได้เต็มประสิทธิภาพเมื่อเริ่ม session ใหม่เท่านั้น

1. กดปุ่ม **New Chat** เพื่อเปิด session ใหม่
2. เรียก skill `pptx` ด้วย slash command:
   ```
   /pptx
   ```
3. Bob จะยืนยันว่าโหลด skill สำเร็จ — จากนั้นทำ STEP 5 ได้เลย

---

### STEP 5 — สร้าง PowerPoint Presentation (15 นาที)

**Prompt 5.1 — สร้าง PPTX:**
```
สร้าง PowerPoint presentation สำหรับนำเสนอผลวิเคราะห์สินเชื่อต่อผู้บริหาร KKP
บันทึกเป็น workshop-business/output/kkp_credit_presentation.pptx

โครงสร้าง Slides:
- Slide 1: Title — "KKP Credit Portfolio Analysis Q2 2024" + subtitle "Credit Risk Management Team"
- Slide 2: Agenda (4 หัวข้อ)
- Slide 3: Portfolio Overview — ตารางสถิติสำคัญ
- Slide 4: Loan Type Distribution — ข้อมูล Pie chart (ใส่เป็นตารางข้อมูล)
- Slide 5: Risk Analysis — ตารางลูกค้าความเสี่ยงสูง
- Slide 6: Key Findings — bullet points 3-4 ข้อ
- Slide 7: Recommendations — bullet points 3 ข้อ
- Slide 8: Thank You + ข้อมูลติดต่อ

สีธีม: น้ำเงินเข้ม (#003087) และขาว, font: Calibri
```

> **Tip:** Bob จะใช้ pptx skill เพื่อสร้างไฟล์ที่เปิดได้ใน PowerPoint ได้ทันที

---

### STEP 6 (Bonus) — วิเคราะห์ข้อมูลพนักงาน (10 นาที)

**Prompt 6.1 — HR Analytics:**
```
อ่านไฟล์ workshop-business/data/kkp_employee_data.csv 
แล้ววิเคราะห์ให้ฉัน:
1. แผนกไหนมี ai_tool_usage_score สูงที่สุด?
2. มีความสัมพันธ์ระหว่าง training_hours_2023 กับ performance_score_2023 หรือไม่?
3. พนักงานที่ eligible สำหรับ promotion มีคุณสมบัติร่วมกันอะไรบ้าง?
4. สรุปข้อเสนอแนะด้าน HR 2 ข้อ
```

---

## 💡 Tips & Best Practices

| สถานการณ์ | วิธีแก้ |
|-----------|---------|
| Bob ไม่อ่านไฟล์ CSV | บอก Bob ว่า "อ่านไฟล์ที่ path `workshop-business/data/kkp_loan_data.csv`" ให้ชัดเจน |
| ไฟล์ PPTX เปิดไม่ได้ | ลอง Prompt ว่า "ตรวจสอบและแก้ไขไฟล์ .pptx ให้ถูกต้องตาม OpenXML standard" |
| ต้องการแก้ไขสีใน HTML | บอก Bob ว่า "เปลี่ยนสี header เป็น #003087 และ background เป็น #f5f5f5" |
| Chart ไม่แสดงข้อมูล | บอก Bob ว่า "ใส่ข้อมูลตัวเลขจริงจาก CSV ลงใน Chart.js dataset" |

---

## 🎯 สิ่งที่ควรได้หลัง Workshop นี้

- [ ] ไฟล์ `kkp_credit_dashboard.html` — เปิดใน browser ได้ มี interactive charts
- [ ] ไฟล์ `kkp_credit_report.docx` — เปิดใน Word ได้ มีตารางและ heading
- [ ] ไฟล์ `kkp_credit_presentation.pptx` — เปิดใน PowerPoint ได้ มี 8 slides
- [ ] เข้าใจวิธีใช้ Bob วิเคราะห์ข้อมูลจาก CSV โดยไม่ต้องเขียนโค้ด

---

## 📚 Skills ที่ใช้ใน Workshop นี้

```
pptx skill       → ช่วย Bob สร้าง .pptx ที่ถูกต้อง
office-insights  → ช่วย Bob อ่านและเขียน .docx / .xlsx
(built-in)       → HTML/CSS/JavaScript สร้างได้ทันทีโดยไม่ต้อง skill พิเศษ
```

---

*KKP x IBM Bob Workshop · Business Track · June 2024*
