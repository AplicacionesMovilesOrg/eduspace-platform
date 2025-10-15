# EduSpace Platform

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![MySQL](https://img.shields.io/badge/MySQL-8.0-4479A1?logo=mysql&logoColor=white)](https://www.mysql.com/)
[![Docker](https://img.shields.io/badge/Docker-Ready-2496ED?logo=docker&logoColor=white)](https://www.docker.com/)

## 📋 Descripción

EduSpace Platform es una plataforma educativa completa desarrollada con .NET 8 y MySQL, diseñada para gestionar de forma integral los procesos de una institución educativa. La plataforma incluye gestión de usuarios (IAM con JWT), perfiles de administradores y profesores, reservas de espacios, programación de reuniones, gestión de aulas y recursos, y reportes de mantenimiento.

## 🏗️ Arquitectura

El proyecto implementa **Clean Architecture** con **Domain-Driven Design (DDD)**, organizando el código en contextos delimitados (bounded contexts). Cada módulo sigue una estructura de 4 capas:

### Capas de la Arquitectura

- **Domain**: Entidades, agregados, value objects, comandos, queries, interfaces de repositorios y servicios de dominio
- **Application**: Servicios de comandos y consultas, interfaces de servicios externos (ACL)
- **Infrastructure**: Implementaciones de repositorios, servicios de hashing, tokens, y persistencia con Entity Framework Core
- **Interfaces**: Controladores REST, facades ACL, resources (DTOs) y transformadores

### Contextos Delimitados

- **IAM**: Autenticación JWT, autorización, gestión de cuentas
- **Profiles**: Perfiles de administradores y profesores
- **Reservations**: Reservas de espacios compartidos
- **ReservationScheduling**: Programación de reuniones con participantes
- **SpacesAndResourceManagement**: Gestión de aulas, áreas compartidas y recursos
- **BreakdownManagement**: Reportes de mantenimiento y averías

### Patrones Implementados

- **Anti-Corruption Layer (ACL)**: Comunicación entre contextos mediante facades
- **Command/Query Separation**: Servicios separados para operaciones de lectura y escritura
- **Repository Pattern**: Abstracción de acceso a datos con Unit of Work
- **Resource Transformation**: Assemblers para convertir entre modelos de dominio y DTOs

## 📦 Requisitos Previos

- **.NET 8.0 SDK** o superior
- **MySQL 8.0**
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
   MYSQL_ROOT_PASSWORD=rootpassword
   MYSQL_DATABASE=eduspacedb
   MYSQL_USER=eduspace
   MYSQL_PASSWORD=eduspace1234
   MYSQL_PORT=3307

   ConnectionStrings__DefaultConnection=server=localhost;port=3307;user=eduspace;password=eduspace1234;database=eduspacedb;AllowPublicKeyRetrieval=true;SslMode=none
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

2. **Instala MySQL 8.0** y crea la base de datos:
   ```sql
   CREATE DATABASE eduspacedb;
   CREATE USER 'eduspace'@'localhost' IDENTIFIED BY 'eduspace1234';
   GRANT ALL PRIVILEGES ON eduspacedb.* TO 'eduspace'@'localhost';
   FLUSH PRIVILEGES;
   ```

3. **Configura la cadena de conexión** en `FULLSTACKFURY.EduSpace.API/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "server=localhost;port=3306;user=eduspace;password=eduspace1234;database=eduspacedb"
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

5. **Aplica las migraciones** (opcional, se crean automáticamente al iniciar):
   ```bash
   dotnet ef database update --project FULLSTACKFURY.EduSpace.API
   ```

6. **Ejecuta la aplicación**:
   ```bash
   dotnet run --project FULLSTACKFURY.EduSpace.API/FULLSTACKFURY.EduSpace.API.csproj
   ```

7. **La API estará disponible en**: `http://localhost:8080`

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
- **Entity Framework Core 8** - ORM y gestión de base de datos
- **MySQL.EntityFrameworkCore** - Provider de MySQL para EF Core

### Seguridad
- **BCrypt.Net-Next** - Hashing de contraseñas
- **System.IdentityModel.Tokens.Jwt** - Generación y validación de tokens JWT
- **Microsoft.AspNetCore.Authentication.JwtBearer** - Middleware de autenticación JWT

### Utilidades
- **Swashbuckle.AspNetCore** - Documentación OpenAPI/Swagger
- **Humanizer** - Conversión de convenciones de nombres (snake_case)
- **EntityFrameworkCore.CreatedUpdatedDate** - Auditoría automática de entidades
- **DotNetEnv** - Gestión de variables de entorno

## 🔧 Comandos de Desarrollo

### Migraciones de Base de Datos

```bash
# Crear una nueva migración
dotnet ef migrations add NombreDeLaMigracion --project FULLSTACKFURY.EduSpace.API

# Aplicar migraciones pendientes
dotnet ef database update --project FULLSTACKFURY.EduSpace.API

# Revertir la última migración
dotnet ef migrations remove --project FULLSTACKFURY.EduSpace.API

# Ver el SQL de una migración
dotnet ef migrations script --project FULLSTACKFURY.EduSpace.API
```

### Build y Testing

```bash
# Compilar el proyecto
dotnet build

# Compilar en modo Release
dotnet build --configuration Release

# Limpiar artefactos de compilación
dotnet clean
```

## 🌐 Configuración de CORS

El proyecto incluye dos políticas CORS configuradas:

- **Development**: Permite cualquier origen, header y método (útil para desarrollo local)
- **Production**: Restringido al dominio `https://eduspacewebapp.netlify.app`

La política se aplica automáticamente según el entorno de ejecución.

## 📁 Estructura del Proyecto

```
FULLSTACKFURY.EduSpace.API/
├── IAM/                           # Gestión de identidad y acceso
│   ├── Domain/
│   ├── Application/
│   ├── Infrastructure/
│   └── Interfaces/
├── Profiles/                      # Perfiles de administradores y profesores
├── Reservations/                  # Reservas de espacios
├── ReservationScheduling/         # Programación de reuniones
├── SpacesAndResourceManagement/   # Gestión de aulas y recursos
├── BreakdownManagement/           # Reportes de mantenimiento
├── Shared/                        # Infraestructura compartida
│   ├── Domain/
│   └── Infrastructure/
│       └── Persistence/EFC/
│           └── Configuration/
│               └── AppDbContext.cs
├── Migrations/                    # Migraciones de EF Core
├── appsettings.json
├── Program.cs
└── Dockerfile
```

Cada bounded context sigue la estructura de Clean Architecture:
- **Domain**: Lógica de negocio pura
- **Application**: Casos de uso
- **Infrastructure**: Implementaciones técnicas
- **Interfaces**: APIs REST y ACL

## 🤝 Contribución

¡Las contribuciones son bienvenidas! Si deseas contribuir:

1. **Fork** el proyecto
2. Crea una **rama** para tu feature:
   ```bash
   git checkout -b feature/nueva-funcionalidad
   ```
3. **Commit** tus cambios siguiendo los estándares:
   ```bash
   git commit -m 'feat: agrega nueva funcionalidad'
   ```
4. **Push** a la rama:
   ```bash
   git push origin feature/nueva-funcionalidad
   ```
5. Abre un **Pull Request**

### Convenciones de Commits

- `feat:` - Nueva funcionalidad
- `fix:` - Corrección de bugs
- `docs:` - Cambios en documentación
- `refactor:` - Refactorización de código
- `test:` - Añadir o modificar tests
- `chore:` - Tareas de mantenimiento

## 📝 Licencia

Este proyecto está bajo la Licencia MIT. Consulta el archivo `LICENSE` para más detalles.

## 👥 Equipo

Desarrollado por **FullStackFury**

## 📧 Contacto y Soporte

Para preguntas, sugerencias o reportar problemas:

- Abre un [issue](https://github.com/ExperimentDesign/eduspace-platform/issues) en GitHub
- Contacta al equipo de desarrollo

---

⭐ Si este proyecto te resulta útil, considera darle una estrella en GitHub
