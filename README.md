# API CRUD Básica con .NET 8 (Arquitectura en Tres Capas + ADO.NET)

Este proyecto es un ejemplo de una API RESTful básica para gestionar empleados, desarrollada con .NET 8. Utiliza una arquitectura en tres capas: **Modelo**, **Data**, y **Web API**, e implementa acceso a datos usando **ADO.NET** y SQL Server.

## Estructura del Proyecto

├── Data/ # Capa de acceso a datos (ADO.NET)
│ ├── EmpleadoData.cs # Métodos CRUD usando ADO.NET
│ └── Conexion.cs # Clase para la conexión a SQL Server
│
├── Modelo/ # Capa de modelos (DTOs/Entidades)
│ └── Empleado.cs # Modelo de datos del empleado
│
├── WebAPI/ # Capa de presentación (API)
│ ├── Controllers/
│ │ └── EmpleadoController.cs # Controlador con los endpoints
│ ├── Program.cs # Configuración y DI
│ └── appsettings.json # Configuración de conexión a BD

## Tecnologías Usadas

- .NET 8
- ADO.NET
- SQL Server
- Arquitectura en tres capas
- Inyección de dependencias (DI)
- RESTful API

## Endpoints Disponibles

-  GET /api/empleado - Obtener todos los empleados
-  GET /api/empleado/{id} - Obtener un empleado por ID
-  POST /api/empleado - Crear un nuevo empleado
-  PUT /api/empleado - Actualizar un empleado existente
-  DELETE /api/empleado/{id}  - Eliminar un empleado
## Debes crear una base de datos y los procedimientos almacenados 
 `` ejecuta el script de la base de datos, este tiene los procedimientos almacenados necesarios para la aplicacion
 
## Configuración

1. Clona el repositorio:

   ```bash
   git clone https://github.com/tuusuario/nombre-proyecto.git
   
## Configura la cadena de conexion en tu proyecto
   "ConnectionStrings": {
  "MiConexion": "Server=TU_SERVIDOR;Database=TU_BD;Trusted_Connection=True;"
}
  
## Restaura los paquetes y ejecuta el proyecto
dotnet restore
dotnet run --project WebAPI

## NOTA a tomar en cuenta
Este proyecto no utiliza Entity Framework, sino ADO.NET directamente para mantener el control total sobre las operaciones SQL.
La separación por capas permite un mantenimiento y escalabilidad más limpios.
