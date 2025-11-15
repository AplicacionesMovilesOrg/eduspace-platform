# EduSpace Platform

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![MongoDB](https://img.shields.io/badge/MongoDB-8.0-47A248?logo=mongodb&logoColor=white)](https://www.mongodb.com/)
[![Docker](https://img.shields.io/badge/Docker-Ready-2496ED?logo=docker&logoColor=white)](https://www.docker.com/)

## 📋 Descripción

EduSpace Platform es una plataforma educativa completa desarrollada con .NET 8 y MongoDB, diseñada para gestionar de forma integral los procesos de una institución educativa. La plataforma incluye gestión de usuarios (IAM con JWT), perfiles de administradores y profesores, reservas de espacios, programación de reuniones, gestión de aulas y recursos, y reportes de mantenimiento.

## 🏗️ Arquitectura

El proyecto implementa **Clean Architecture** con **Domain-Driven Design (DDD)**, organizando el código en contextos delimitados (bounded contexts). Cada módulo sigue una estructura de 4 capas:

### Capas de la Arquitectura

- **Domain**: Entidades, agregados, value objects, comandos, queries, interfaces de repositorios y servicios de dominio
- **Application**: Servicios de comandos y consultas, interfaces de servicios externos (ACL)
- **Infrastructure**: Implementaciones de repositorios con MongoDB, servicios de hashing, tokens, y persistencia
- **Interfaces**: Controladores REST, facades ACL, resources (DTOs) y transformadores

### Contextos Delimitados

- **IAM**: Autenticación JWT, autorización, gestión de cuentas
- **Profiles**: Perfiles de administradores y profesores
- **ReservationsManagement**: Gestión de reservas de espacios
- **MeetingsManagement**: Programación de reuniones con participantes profesores
- **ClassroomAndSpacesManagement**: Gestión de aulas, áreas compartidas y recursos
- **ReportsManagement**: Reportes de mantenimiento y averías

### Patrones Implementados

- **Anti-Corruption Layer (ACL)**: Comunicación entre contextos mediante facades
- **Command/Query Separation**: Servicios separados para operaciones de lectura y escritura
- **Repository Pattern**: Abstracción de acceso a datos con Unit of Work
- **Resource Transformation**: Assemblers para convertir entre modelos de dominio y DTOs

## 📦 Requisitos Previos

- **.NET 8.0 SDK** o superior
- **MongoDB 8.0**
- **Docker y Docker Compose** (opcional, recomendado)

## 🚀 Instalación y Configuración

### Opción 1: Con Docker (Recomendado)

1. **Clona el repositorio**:
   ```bash
   git clone https://github.com/ExperimentDesign/eduspace-platform.git
   cd eduspace-platform
   ```

2. **Configura las variables de entorno**:

   Crea un archivo `.env` en la raíz del proyecto (ya existe un ejemplo):
   ```env
   MONGODB_VERSION=8.0
   MONGODB_PORT=27017
   MONGODB_DATABASE_NAME=eduspacedb
   MONGODB_ROOT_USERNAME=eduspace
   MONGODB_ROOT_PASSWORD=eduspace123

   MONGODB_CONNECTION_STRING=mongodb://eduspace:eduspace123@localhost:27017
   ```

3. **Configura el Token JWT**:

   Edita `FULLSTACKFURY.EduSpace.API/appsettings.json` y agrega tu secreto JWT:
   ```json
   {
     "TokenSettings": {
       "Secret": "tu-secret-key-muy-segura-de-al-menos-32-caracteres"
     }
   }
   ```

4. **Levanta los servicios**:
   ```bash
   docker-compose up --build
   ```

5. **La API estará disponible en**: `http://localhost:8080`

### Opción 2: Instalación Local

1. **Clona el repositorio**:
   ```bash
   git clone https://github.com/ExperimentDesign/eduspace-platform.git
   cd eduspace-platform
   ```

2. **Instala MongoDB 8.0**:
   - Descarga desde [mongodb.com/try/download/community](https://www.mongodb.com/try/download/community)
   - Inicia el servicio MongoDB en el puerto por defecto (27017)

3. **Configura las variables de entorno** en `FULLSTACKFURY.EduSpace.API/appsettings.json`:
   ```json
   {
     "MongoDbSettings": {
       "ConnectionString": "mongodb://localhost:27017",
       "DatabaseName": "eduspacedb"
     },
     "TokenSettings": {
       "Secret": "tu-secret-key-muy-segura-de-al-menos-32-caracteres"
     }
   }
   ```

4. **Restaura las dependencias**:
   ```bash
   dotnet restore
   ```

5. **Ejecuta la aplicación**:
   ```bash
   dotnet run --project FULLSTACKFURY.EduSpace.API/FULLSTACKFURY.EduSpace.API.csproj
   ```

6. **La API estará disponible en**: `http://localhost:8080`

## 📚 Documentación de la API

### Swagger UI

Una vez que la aplicación esté ejecutándose, puedes acceder a la documentación interactiva de Swagger en:

- **Desarrollo**: `http://localhost:8080/swagger`
- **Producción**: La documentación Swagger también está habilitada en producción

### Autenticación

La API utiliza **JWT (JSON Web Tokens)** para la autenticación. Para acceder a endpoints protegidos:

1. **Regístrate** usando `POST /api/v1/authentication/sign-up`
2. **Inicia sesión** usando `POST /api/v1/authentication/sign-in` para obtener tu token
3. Incluye el token en el header `Authorization: Bearer {tu-token}` en todas las peticiones protegidas

### Endpoints Principales

#### Autenticación
- `POST /api/v1/authentication/sign-up` - Registro de usuario
- `POST /api/v1/authentication/sign-in` - Inicio de sesión (retorna JWT)

#### Perfiles
- `GET /api/v1/administratorprofiles` - Listar perfiles de administradores
- `POST /api/v1/administratorprofiles` - Crear perfil de administrador
- `GET /api/v1/teachersprofiles` - Listar perfiles de profesores
- `POST /api/v1/teachersprofiles` - Crear perfil de profesor

#### Aulas y Recursos
- `GET /api/v1/classrooms` - Listar aulas
- `POST /api/v1/classrooms` - Crear aula
- `GET /api/v1/resource` - Listar recursos
- `POST /api/v1/resource` - Crear recurso
- `GET /api/v1/sharedarea` - Listar áreas compartidas

#### Reservas y Reuniones
- `GET /api/v1/reservations` - Listar reservas
- `POST /api/v1/reservations` - Crear reserva
- `GET /api/v1/meeting` - Listar reuniones
- `POST /api/v1/meeting` - Crear reunión

#### Reportes de Mantenimiento
- `GET /api/v1/report` - Listar reportes
- `POST /api/v1/report` - Crear reporte de avería

Consulta el archivo `FULLSTACKFURY.EduSpace.API.http` para ver ejemplos de peticiones.

## 🛠️ Tecnologías y Librerías

### Backend
- **.NET 8.0** - Framework principal
- **ASP.NET Core** - APIs REST
- **MongoDB.Driver** - Driver oficial de MongoDB para .NET
- **MongoDB.Bson** - Serialización y manipulación de documentos BSON

### Seguridad
- **BCrypt.Net-Next** - Hashing de contraseñas
- **System.IdentityModel.Tokens.Jwt** - Generación y validación de tokens JWT
- **Microsoft.AspNetCore.Authentication.JwtBearer** - Middleware de autenticación JWT

### Utilidades
- **Swashbuckle.AspNetCore** - Documentación OpenAPI/Swagger con anotaciones
- **DotNetEnv** - Gestión de variables de entorno desde archivos .env

## 📁 Estructura del Proyecto

```
FULLSTACKFURY.EduSpace.API/
├── IAM/                                # Gestión de identidad y acceso
│   ├── Domain/
│   ├── Application/
│   ├── Infrastructure/
│   └── Interfaces/
├── Profiles/                           # Perfiles de administradores y profesores
├── ReservationsManagement/             # Gestión de reservas de espacios
├── MeetingsManagement/                 # Programación de reuniones
├── ClassroomAndSpacesManagement/       # Gestión de aulas y recursos
├── ReportsManagement/                  # Reportes de mantenimiento
├── Shared/                             # Infraestructura compartida
│   ├── Domain/
│   └── Infrastructure/
│       └── Persistence/MongoDB/
│           └── Configuration/
│               └── MongoDbContext.cs
├── appsettings.json
├── Program.cs
└── Dockerfile
```

Cada bounded context sigue la estructura de Clean Architecture:
- **Domain**: Entidades, agregados, value objects, comandos, queries, interfaces de repositorios y servicios
- **Application**: Implementación de servicios de comandos y consultas, servicios externos (ACL)
- **Infrastructure**: Repositorios MongoDB, servicios de infraestructura
- **Interfaces**: Controladores REST, facades ACL, resources (DTOs) y assemblers

### Convenciones de Commits

- `feat:` - Nueva funcionalidad
- `fix:` - Corrección de bugs
- `docs:` - Cambios en documentación
- `refactor:` - Refactorización de código
- `test:` - Añadir o modificar tests
- `chore:` - Tareas de mantenimiento