# TodoApi
Rest api para una aplicación de tareas pendientes.

## Requerimientos

* .NET Core 2.* SDK (https://www.microsoft.com/net/download/linux)

Para instalar las dependencias, ejecutar `dotnet restore` dentro de la carpeta TodoApi

#### Pasos para configurar el entorno de desarrollo:

1. Copiar el archivo `appSettings.Development.json`
2. Abrir el arhivo `appSettings.Development.json` y modificar el atributo `"ConnectionStrings"` con los valores de conexión de la base de datos local
3. Establecer la variable de entorno, ejecutando el siguiente comando: `ASPNETCORE_ENVIRONMENT=NOMBRE_AMBIENTE`

#### Pasos para generar una migración de base de datos:

1. Comando para generar el archivo de migración: `dotnet ef migrations add NOMBRE_MIGRACION`
2. Ejecutar la migración: `dotnet ef database update`


**Ejecutar el proyecto con** `dotnet run`

