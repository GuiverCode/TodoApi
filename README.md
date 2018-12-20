# TodoApi
Rest api para una aplicación de tareas pendientes

## Requerimientos

* .NET Core 2.* SDK (https://www.microsoft.com/net/download/linux)

## Pasos para configurar el entorno de desarrollo:

1. Copiar el archivo `appSettings.DevGuilleNote.json` y renombrarlo `appSettings.NOMBRE_AMBIENTE.json`
2. Abrir el arhivo `appSettings.NOMBRE_AMBIENTE.json` y modificar el atributo `"ConnectionStrings"` con los valores de conexión de la base de datos local
3. Establecer la variable de entorno, ejecutando el siguiente comando: `ASPNETCORE_ENVIROMENT=NOMBRE_AMBIENTE`

## Pasos para generar una migración de base de datos:

1. Comando para generar el archivo de migración: `dotnet ef migrations add NOMBRE_MIGRACION`
2. Ejecutar la migración: `dotnet ef database update`

## Ejecutar el proyecto: 
* `dotnet run`
