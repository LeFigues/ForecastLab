#!/usr/bin/env python3
# -*- coding: utf-8 -*-

import pdfplumber
import json
import os

def pdf_to_json(pdf_path):
    """
    Open the PDF and return a dict with each page's text and tables.
    Structure:
    {
      "pages": [
        {
          "page_number": 1,
          "text": "...",
          "tables": [
            [
              ["cell1", "cell2", ...],
              ["cell1", "cell2", ...],
              ...
            ],
            ...
          ]
        },
        ...
      ]
    }
    """
    data = {"pages": []}

    with pdfplumber.open(pdf_path) as pdf:
        for page_num, page in enumerate(pdf.pages, start=1):
            text = page.extract_text() or ""
            raw_tables = page.extract_tables()
            tables = []
            for table in raw_tables:
                rows = []
                for row in table:
                    clean_row = []
                    for cell in row:
                        if isinstance(cell, str):
                            clean_row.append(cell.strip())
                        else:
                            clean_row.append("")
                    rows.append(clean_row)
                tables.append(rows)
            data["pages"].append({
                "page_number": page_num,
                "text": text,
                "tables": tables
            })

    return data

if __name__ == "__main__":
    pdf_file = "RE-10-LAB-322  IOT Y ROBOTICA  v2.pdf"
    if not os.path.isfile(pdf_file):
        print(f"ERROR: File not found: {pdf_file}")
        exit(1)

    result = pdf_to_json(pdf_file)
    output_file = os.path.splitext(pdf_file)[0] + ".json"
    with open(output_file, "w", encoding="utf-8") as f:
        json.dump(result, f, ensure_ascii=False, indent=2)

    print(f"JSON file created: {output_file}")
