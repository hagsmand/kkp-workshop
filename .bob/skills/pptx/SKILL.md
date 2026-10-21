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
