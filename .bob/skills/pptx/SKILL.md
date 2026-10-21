---
name: pptx
description: "Use this skill any time a .pptx file is involved in any way — as input, output, or both. This includes: creating slide decks, pitch decks, or presentations; reading, parsing, or extracting text from any .pptx file (even if the extracted content will be used elsewhere, like in an email or summary); editing, modifying, or updating existing presentations; combining or splitting slide files; working with templates, layouts, speaker notes, or comments. Trigger whenever the user mentions \"deck,\" \"slides,\" \"presentation,\" or references a .pptx filename, regardless of what they plan to do with the content afterward. If a .pptx file needs to be opened, created, or touched, use this skill."
---

# PPTX Skill

Use `pptxgenjs` (npm) to create .pptx files from scratch.

## Quick start

Install dependency:

```bash
npm install -g pptxgenjs
```

Generate slides with JavaScript using the pptxgenjs API, then run with `node` to produce the final .pptx file.

## Basic example

```js
const pptx = new PptxGenJS();

const slide = pptx.addSlide();
slide.addText("Hello World", { x: 1, y: 1, w: 8, h: 1.5, fontSize: 36, bold: true, color: "003087" });

pptx.writeFile({ fileName: "output.pptx" });
```

## Key rules

- Always set slide dimensions: `pptx.layout = "LAYOUT_WIDE"` (13.33" x 7.5")
- All positions and sizes are in inches: `x`, `y`, `w`, `h`
- Colors are hex strings without `#`: e.g. `"003087"` not `"#003087"`
- Run the script with `node slides.js` to produce the final file
- Verify the output opens correctly in PowerPoint or LibreOffice

---

## KKP Bank Theme

When creating presentations for **Kiatnakin Phatra Bank (KKP)**, always apply the official KKP brand palette and design conventions below. These colors are extracted directly from the KKP corporate website (kkpfg.com).

### Official KKP Color Palette

```js
// ─── KKP Brand Colors ────────────────────────────────────────────────────────
const KKP_PURPLE      = "594F74";  // Primary brand purple — navbar, header bars, footer
const KKP_PURPLE_MID  = "635F98";  // Secondary purple — accent shapes, badges
const KKP_PURPLE_LIGHT= "756897";  // Lighter purple — secondary text on dark bg
const KKP_LAVENDER    = "9385B8";  // Lavender — subtle highlights, icons
const KKP_BG_LIGHT    = "F6F7FE";  // Near-white lavender — card/KPI backgrounds
const KKP_BORDER      = "D4CBED";  // Soft purple border — table lines, card outlines
const KKP_TEXT_DARK   = "3D3C3E";  // Near-black — body text
const KKP_TEXT_MID    = "666666";  // Medium gray — secondary body text
const WHITE           = "FFFFFF";
```

### KKP Slide Design Conventions

1. **Title/Cover slide**: Full `KKP_PURPLE` background; main title in white; subtitle in `KKP_LAVENDER`
2. **Content slides**: White background; header bar in `KKP_PURPLE` (full width, ~1.1" tall); thin accent line below header in `KKP_PURPLE_MID`
3. **KPI / stat cards**: `KKP_BG_LIGHT` fill with `KKP_BORDER` outline; value in `KKP_PURPLE`; label in `KKP_TEXT_DARK`
4. **Tables**: Header row fill `KKP_PURPLE` with white text; data rows alternate white / `KKP_BG_LIGHT`; borders `KKP_BORDER`
5. **Number badges / icons**: `KKP_PURPLE` fill, white text
6. **Recommendation / highlight cards**: `KKP_BG_LIGHT` fill; left accent strip in `KKP_PURPLE`
7. **Thank-you / closing slide**: Full `KKP_PURPLE` background, matching the cover
8. **Font**: `"Calibri"` throughout; Thai and English text both supported

### KKP Helper — addHeaderSlide()

```js
function addHeaderSlide(pptx, title, subtitle) {
  const slide = pptx.addSlide();
  slide.background = { color: WHITE };

  // Header bar
  slide.addShape(pptx.ShapeType.rect, {
    x: 0, y: 0, w: 13.33, h: 1.1,
    fill: { color: KKP_PURPLE }, line: { color: KKP_PURPLE },
  });
  slide.addText(title, {
    x: 0.4, y: 0.1, w: 12.5, h: 0.7,
    fontSize: 24, bold: true, color: WHITE, fontFace: "Calibri", valign: "middle",
  });
  if (subtitle) {
    slide.addText(subtitle, {
      x: 0.4, y: 0.75, w: 12.5, h: 0.3,
      fontSize: 12, color: KKP_LAVENDER, fontFace: "Calibri", italic: true,
    });
  }
  // Accent line under header
  slide.addShape(pptx.ShapeType.rect, {
    x: 0, y: 1.1, w: 13.33, h: 0.04,
    fill: { color: KKP_PURPLE_MID }, line: { color: KKP_PURPLE_MID },
  });
  return slide;
}
```

### Quick reference — do NOT use these generic colors for KKP decks

| Wrong (generic) | Correct (KKP)    |
|-----------------|------------------|
| `"003087"` (dark blue) | `"594F74"` (KKP purple) |
| `"0066CC"` (blue accent) | `"635F98"` (KKP mid-purple) |
| `"E8EFF8"` (blue tint bg) | `"F6F7FE"` (KKP lavender bg) |
| `"C0CEDE"` (blue border) | `"D4CBED"` (KKP purple border) |
| `"B0C4DE"` (light blue text) | `"9385B8"` (KKP lavender text) |
