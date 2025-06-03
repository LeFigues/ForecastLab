# ForecastLab
Sistema de Prediccion de Insumos para Laboratorios

Distribucion de Branches:

main:
  Codigo Backend y FrontEnd, para ejecturase desde el IDE VisualStudio 2022
backend:
  Codigo Backend, ASP.NET Core Web API
frontend:
  Codigo Frontend Blazor WebAssembly en VisualStudio 2022
release:
  Publicacion del codigo completo (Backend, Frontend y PythonModules), listo para instalar en el Servidor
release-fl_api:
  Publicacion de la API (backend)
release-frontend:
  Publicacion del frontend
release-python
  Modulos Python
utils:
  PDFs

Se recomienda tener las siguientes tecnologias e IDEs
Desarrollo:
VisualStudio2022 
.net9
Python
MongoDB

Servidor
dotnet 9
python (enviorement in folder of release-python recommended)
  pdfplumber
MongoDB
Nginx
SSL required

API key de OpenAI y configurar un Asistente que tenga permitido el acceso a documentos y archivos json.
