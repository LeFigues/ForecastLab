
# data_utils.py
import requests
import pandas as pd

def fetch_movimientos(api_url: str) -> pd.DataFrame:
    """
    Consulta la API de movimientos-inventario y retorna un DataFrame con:
    columnas: ['fecha_entregado', 'fecha_devuelto', 'cantidad', 'insumo_nombre', ...].
    """
    resp = requests.get(api_url)
    resp.raise_for_status()
    json_data = resp.json().get("data", [])
    # Convertir a DataFrame
    df = pd.DataFrame(json_data)
    # Asegurarse de parsear fechas
    df["fecha_entregado"] = pd.to_datetime(df["fecha_entregado"])
    df["cantidad"] = df["cantidad"].astype(int)
    return df

# data_utils.py (continuación)
def aggregate_monthly(df: pd.DataFrame) -> pd.Series:
    """
    Recibe df de movimientos, asume que cada fila es un movimiento de 'PRESTAMO'
    y devuelve una serie con índice mensuales (YYYY-MM) y valores = sum(cantidad).
    """
    # Filtrar solo movimientos de tipo PRESTAMO u otro filtro si aplica
    df_filtrado = df[df["tipo_movimiento"] == "PRESTAMO"].copy()
    # Usar fecha_entregado para agrupar
    df_filtrado["mes"] = df_filtrado["fecha_entregado"].dt.to_period("M")
    series = df_filtrado.groupby("mes")["cantidad"].sum().sort_index()
    # Convertir índice a datetime (por convención, usaremos el primer día del mes)
    series.index = series.index.to_timestamp()
    return series

# data_utils.py (continuación)
def aggregate_monthly_by_insumo(df: pd.DataFrame, insumo: str) -> pd.Series:
    df_i = df[df["insumo_nombre"] == insumo]
    if df_i.empty:
        return pd.Series(dtype=float)
    df_i["mes"] = df_i["fecha_entregado"].dt.to_period("M")
    s = df_i.groupby("mes")["cantidad"].sum().sort_index()
    s.index = s.index.to_timestamp()
    return s
