
# model_utils.py (continuaci�n)
import openai
import json

def forecast_with_gpt(historical: pd.Series, periods: int = 3, openai_key: str = None):
    """
    Usa la API de OpenAI para solicitar un forecast en base a la serie hist�rica.
    - historical: pd.Series (�ndice datetime, valores num�ricos)
    - periods: meses a predecir
    - openai_key: tu API key
    Retorna un pd.Series con fechas futuras y valores estimados (aprox).
    """
    if openai_key:
        openai.api_key = openai_key
    else:
        raise ValueError("Debes proveer OPENAI_API_KEY")

    # Construir el prompt: lista de fechas y cantidades
    # Ejemplo: "Datos hist�ricos (YYYY-MM: valor): 2024-01: 23, 2024-02: 17, ...; predice los siguientes 3 meses"
    datos = []
    for fecha, val in historical.items():
        datos.append(f"{fecha.strftime('%Y-%m')}: {int(val)}")
    datos_str = ", ".join(datos)

    prompt = (
        "Tienes la siguiente serie hist�rica de demanda mensual de un insumo de laboratorio:\n"
        f"{datos_str}\n"
        f"�Cu�l ser�a la demanda estimada para los pr�ximos {periods} meses? "
        "Devu�lvelo en el formato: 'YYYY-MM: valor, YYYY-MM: valor, ...' con valores redondeados.\n"
    )

    response = openai.ChatCompletion.create(
        model="gpt-4o-mini",  # o el que uses
        messages=[
            {"role": "system", "content": "Eres un analista de series temporales."},
            {"role": "user", "content": prompt}
        ],
        temperature=0.0,
        max_tokens=200
    )
    text = response.choices[0].message.content.strip()

    # Parsear la respuesta (suponiendo que venga como "2025-07: 42, 2025-08: 37, 2025-09: 40")
    forecast_dict = {}
    for parte in text.split(","):
        par = parte.strip()
        if not par:
            continue
        fecha_str, val_str = par.split(":")
        fecha_str = fecha_str.strip()
        val = float(val_str.strip())
        # Convertir �2025-07� a pandas Timestamp (primer d�a del mes)
        ts = pd.to_datetime(fecha_str + "-01")
        forecast_dict[ts] = val

    forecast_series = pd.Series(forecast_dict)
    return forecast_series
