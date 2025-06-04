
# forecast.py
import json
import os
import sys
from datetime import datetime
import pandas as pd
from data_utils import fetch_movimientos, aggregate_monthly_by_insumo
from model_utils import train_sarima, forecast_sarima, forecast_with_gpt
from pymongo import MongoClient

def main(args):
    """
    Uso (CLI):
      python forecast.py <insumo_nombre> <meses_a_predecir>
    Ejemplo:
      python forecast.py "LED Rojo 0.5mm" 3
    """
    if len(args) != 2:
        print("Uso: python forecast.py <insumo_nombre> <meses_a_predecir>")
        sys.exit(1)

    insumo = args[0]
    meses = int(args[1])

    # 1. Cargar configuración
    config = json.load(open("config.json", "r"))
    api_url = config["MOVIMIENTOS_API"]
    mongo_uri = config["MONGO_URI"]
    coll_name = config["FORECAST_COLLECTION"]

    # 2. Obtener datos y agrupar
    df_mov = fetch_movimientos(api_url)
    ts = aggregate_monthly_by_insumo(df_mov, insumo)

    if ts.empty:
        print(f"No hay movimientos para '{insumo}'.")
        sys.exit(1)

    # 3. Entrenar SARIMA y hacer forecast clásico
    modelo = train_sarima(ts)
    forecast_classic = None
    if modelo:
        forecast_classic = forecast_sarima(modelo, periods=meses)

    # 4. (Opcional) Forecast con GPT
    openai_key = os.getenv("OPENAI_API_KEY")
    forecast_gpt = None
    try:
        forecast_gpt = forecast_with_gpt(ts, periods=meses, openai_key=openai_key)
    except Exception as e:
        print(f"Error usando GPT: {e}")

    # 5. Preparar documento para persistir en MongoDB
    resultado = {
        "insumo": insumo,
        "timestamp": datetime.utcnow(),
        "historico": ts.to_dict(),  
        "forecast_classic": (forecast_classic.to_dict() if forecast_classic is not None else {}),
        "forecast_gpt": (forecast_gpt.to_dict() if forecast_gpt is not None else {}),
        "periodos_futuro": meses
    }

    # 6. Guardar en MongoDB
    client = MongoClient(mongo_uri)
    db = client.get_default_database()
    colec = db[coll_name]
    colec.insert_one(resultado)
    print(f"Forecast guardado en MongoDB para '{insumo}'.")

if __name__ == "__main__":
    main(sys.argv[1:])
