import pdfplumber
import re
import json

def parse_pdf(path):
    # 1) LEER TODO EL TEXTO DEL PDF
    full_text = ""
    with pdfplumber.open(path) as pdf:
        for page in pdf.pages:
            txt = page.extract_text()
            if txt:
                full_text += txt + "\n"

    # 2) SEPARAR EN LINEAS LIMPIAS
    lines = [l.strip() for l in full_text.split("\n") if l.strip()]

    # 3) EXTRAER FACULTAD Y CARRERA (lineas que empiecen con FACULTAD o CARRERA)
    faculty = ""
    career = ""
    for l in lines:
        m_fac = re.match(r"^FACULTAD\s+(.+)$", l, re.IGNORECASE)
        if m_fac:
            faculty = m_fac.group(1).strip()
        m_car = re.match(r"^CARRERA\s+(.+)$", l, re.IGNORECASE)
        if m_car:
            career = m_car.group(1).strip()
        if faculty and career:
            break

    # 4) EXTRAER CODIGO DE REGISTRO (RE-<num>-LAB-<num>) Y VERSION (Version X o Version X.Y)
    code = ""
    version = ""
    m_code = re.search(r"\bRE-\d+-LAB-\d+\b", full_text)
    if m_code:
        code = m_code.group(0).strip()
        start = full_text.find(code)
        snippet = full_text[start:start + 200]
        m_ver = re.search(r"Version[:\s]*([\d\.]+)", snippet, re.IGNORECASE)
        if m_ver:
            version = m_ver.group(1).strip()

    # 5) EXTRAER TITULO DEL DOCUMENTO (primera linea en MAYUSCULAS que no empiece con FACULTAD/CARRERA)
    title = ""
    for l in lines:
        if len(l) > 5 and l.upper() == l and not re.match(r"^(FACULTAD|CARRERA)\b", l, re.IGNORECASE):
            title = l
            break

    # 6) DIVIDIR EN BLOQUES DE “PRACTICA <num>”
    practs = []
    bloques = re.split(r"(?=PRACTICA\s+\d+\s*[:\.]?)", full_text, flags=re.IGNORECASE)

    for bloque in bloques:
        primera_line = bloque.strip().split("\n")[0].strip()
        if not re.match(r"PRACTICA\s+\d+", primera_line, re.IGNORECASE):
            continue

        # 6.1) EXTRAER NOMBRE DE LA PRACTICA
        lines_bloc = [l.strip() for l in bloque.split("\n") if l.strip()]
        idx = 0
        for i, l2 in enumerate(lines_bloc):
            if re.match(r"PRACTICA\s+\d+", l2, re.IGNORECASE):
                idx = i
                break

        name_parts = []
        for j in range(idx + 1, len(lines_bloc)):
            l2 = lines_bloc[j]
            if re.match(r"^\d+\.", l2) or re.match(r"^I\.", l2) or re.match(r"^OBJETIVOS", l2, re.IGNORECASE) or re.match(r"^MATERIALES", l2, re.IGNORECASE):
                break
            name_parts.append(l2)
        pract_name = " ".join(name_parts).strip()
        if not pract_name:
            continue

        # 6.2) EXTRAER CANTIDAD DE ESTUDIANTES POR GRUPO
        est_count = ""
        m_ce = re.search(r"Cantidad\s+de\s+Estudiantes\s+por\s+Grupo\s*[:\-]?\s*(\d+)", bloque, re.IGNORECASE)
        if m_ce:
            est_count = m_ce.group(1).strip()
        else:
            m_alt = re.search(r"de\s+(\d+)\s+estudiantes", bloque, re.IGNORECASE)
            if m_alt:
                est_count = m_alt.group(1).strip()

        practs.append({
            "nombre_practica": pract_name,
            "materiales_reactivos_equipos": [],
            "cantidad_estudiantes_por_grupo": est_count
        })

    # 7) EXTRAER “Materiales, Reactivos y Equipos” usando tablas página por página
    with pdfplumber.open(path) as pdf:
        for page in pdf.pages:
            texto = page.extract_text() or ""
            m_pr = re.search(r"PRACTICA\s+(\d+)", texto, re.IGNORECASE)
            if not m_pr:
                continue
            num = int(m_pr.group(1))
            if 1 <= num <= len(practs):
                tablas = page.extract_tables()
                if tablas and tablas[0]:
                    primera_tabla = tablas[0]
                    lista_desc = []
                    for fila in primera_tabla:
                        if len(fila) >= 3 and fila[2]:
                            desc = fila[2].replace("\n", " ").strip()
                            lista_desc.append(desc)
                    practs[num - 1]["materiales_reactivos_equipos"] = lista_desc

    # 8) ARMAR DICCIONARIO FINAL
    result = {
        "facultad": faculty,
        "carrera": career,
        "titulo_documento": title,
        "codigo_registro": code,
        "version": version,
        "practicas": practs
    }
    return result

if __name__ == "__main__":
    pdf_path = "RE-10-LAB-322  IOT Y ROBOTICA  v2.pdf"  # Ajusta al nombre exacto de tu PDF
    data = parse_pdf(pdf_path)
    print(json.dumps(data, ensure_ascii=False, indent=2))
