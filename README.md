# 🏢 ApiBTG - API de Gestión Empresarial

[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![C#](https://img.shields.io/badge/C%23-239120?logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-6.0-green.svg)](https://docs.microsoft.com/en-us/ef/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2019-red.svg)](https://www.microsoft.com/en-us/sql-server/sql-server-2019)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## 📋 Descripción

**ApiBTG** es una API REST moderna desarrollada en **.NET 8** que proporciona servicios de gestión empresarial integral. La aplicación implementa arquitectura limpia (Clean Architecture) con patrones CQRS y MediatR, ofreciendo una solución robusta y escalable para la gestión de clientes, productos, sucursales, disponibilidades, inscripciones, visitas y usuarios.

## ✨ Características Principales

### 🏗️ **Arquitectura y Patrones**
- **Clean Architecture** con separación clara de responsabilidades
- **CQRS (Command Query Responsibility Segregation)** para operaciones de lectura y escritura
- **MediatR** para implementación de patrones mediator
- **Repository Pattern** para acceso a datos
- **Unit of Work** para transacciones
- **FluentValidation** para validaciones robustas

### 🔐 **Seguridad y Autenticación**
- **JWT (JSON Web Tokens)** para autenticación
- **Autorización basada en roles** (admin, usuario)
- **Validación de datos** con FluentValidation
- **Logging estructurado** para auditoría
- **Manejo seguro de contraseñas**

### 📊 **Funcionalidades del Negocio**
- **Gestión de Clientes**: CRUD completo con validaciones
- **Gestión de Productos**: Administración de catálogo de productos
- **Gestión de Sucursales**: Control de ubicaciones empresariales
- **Gestión de Disponibilidades**: Control de stock y disponibilidad
- **Gestión de Inscripciones**: Registro de clientes en actividades
- **Gestión de Visitas**: Seguimiento de visitas a sucursales
- **Gestión de Usuarios**: Administración de usuarios del sistema

### 🔔 **Sistema de Notificaciones**
- **Notificaciones por Email** (Mailtrap para desarrollo)
- **Notificaciones por SMS** (Twilio)
- **Preferencias de notificación** por usuario
- **Sistema de templates** para mensajes

### 🧪 **Testing**
- **Tests unitarios** con xUnit
- **FluentAssertions** para aserciones legibles
- **Moq** para mocking de dependencias
- **Cobertura de tests** para todas las entidades principales

## 🏛️ Arquitectura del Proyecto

```
ApiBTG/
├── 📁 ApiBTG/                    # Capa de Presentación (API Controllers)
├── 📁 ApiBTG.Application/        # Capa de Aplicación (CQRS, Services)
├── 📁 ApiBTG.Domain/            # Capa de Dominio (Entities, DTOs, Validators)
├── 📁 ApiBTG.Infrastructure/    # Capa de Infraestructura (DbContext, Repositories)
├── 📁 ApiBTG.Test/              # Tests Unitarios
└── 📁 cloudformation/           # Configuración AWS CloudFormation
```

### **Estructura de Capas**

#### **🎯 ApiBTG (Presentación)**
- **Controllers**: Endpoints REST de la API
- **Program.cs**: Configuración de la aplicación
- **appsettings.json**: Configuración de la aplicación

#### **⚙️ ApiBTG.Application (Aplicación)**
- **Commands**: Operaciones de escritura (Create, Update, Delete)
- **Queries**: Operaciones de lectura (Get, GetAll)
- **Services**: Servicios de aplicación
- **Behaviors**: Comportamientos transversales (validación, logging)

#### **🏛️ ApiBTG.Domain (Dominio)**
- **Entities**: Entidades del dominio de negocio
- **DTOs**: Objetos de transferencia de datos
- **Validators**: Validaciones de dominio con FluentValidation
- **Enums**: Enumeraciones del sistema

#### **🗄️ ApiBTG.Infrastructure (Infraestructura)**
- **DbContexts**: Contexto de Entity Framework
- **Repositories**: Implementación de repositorios
- **Migrations**: Migraciones de base de datos

## 🚀 Tecnologías Utilizadas

### **Backend**
- **.NET 8** - Framework de desarrollo
- **ASP.NET Core** - Framework web
- **Entity Framework Core 8** - ORM para acceso a datos
- **SQL Server** - Base de datos relacional
- **MediatR** - Implementación de patrones mediator
- **FluentValidation** - Validación de datos
- **AutoMapper** - Mapeo de objetos

### **Testing**
- **xUnit** - Framework de testing
- **FluentAssertions** - Aserciones legibles
- **Moq** - Framework de mocking

### **DevOps & Cloud**
- **AWS CloudFormation** - Infrastructure as Code
- **AWS ECS Fargate** - Contenedores serverless
- **AWS RDS** - Base de datos gestionada
- **AWS Secrets Manager** - Gestión de secretos
- **Docker** - Containerización

### **Notificaciones**
- **Mailtrap** - Servidor SMTP para desarrollo
- **Twilio** - Servicios de SMS

## 📦 Instalación y Configuración

### **Prerrequisitos**
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) o [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [VS Code](https://code.visualstudio.com/)

### **1. Clonar el Repositorio**
```bash
git clone https://github.com/DidierAvila/ApiBTG.git
cd ApiBTG
```

### **2. Configurar la Base de Datos**
```bash
# Actualizar connection string en appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=BTG;User ID=admin;Password=admin;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### **3. Ejecutar Migraciones**
```bash
cd ApiBTG
dotnet ef database update
```

### **4. Configurar Variables de Entorno**
Crear `appsettings.Development.json`:
```json
{
  "JwtSettings": {
    "key": "TU_JWT_SECRET_KEY"
  },
  "EmailSettings": {
    "Username": "TU_MAILTRAP_USERNAME",
    "Password": "TU_MAILTRAP_PASSWORD"
  },
  "Twilio": {
    "AccountSid": "TU_TWILIO_ACCOUNT_SID",
    "AuthToken": "TU_TWILIO_AUTH_TOKEN"
  }
}
```

### **5. Ejecutar la Aplicación**
```bash
dotnet run
```

La API estará disponible en: `https://localhost:7001` o `http://localhost:5001`

## 📚 Documentación de la API

### **Endpoints Principales**

#### **🔐 Autenticación**
- `POST /api/Security/login` - Iniciar sesión
- `POST /api/Security/register` - Registrar usuario

#### **👥 Clientes**
- `GET /api/Cliente` - Obtener todos los clientes
- `GET /api/Cliente/{id}` - Obtener cliente por ID
- `POST /api/Cliente` - Crear cliente
- `PUT /api/Cliente/{id}` - Actualizar cliente
- `DELETE /api/Cliente/{id}` - Eliminar cliente

#### **🏪 Sucursales**
- `GET /api/Sucursal` - Obtener todas las sucursales
- `GET /api/Sucursal/{id}` - Obtener sucursal por ID
- `POST /api/Sucursal` - Crear sucursal
- `PUT /api/Sucursal/{id}` - Actualizar sucursal
- `DELETE /api/Sucursal/{id}` - Eliminar sucursal

#### **📦 Productos**
- `GET /api/Producto` - Obtener todos los productos
- `GET /api/Producto/{id}` - Obtener producto por ID
- `POST /api/Producto` - Crear producto
- `PUT /api/Producto/{id}` - Actualizar producto
- `DELETE /api/Producto/{id}` - Eliminar producto

#### **📋 Disponibilidades**
- `GET /api/Disponibilidad` - Obtener todas las disponibilidades
- `GET /api/Disponibilidad/{id}` - Obtener disponibilidad por ID
- `POST /api/Disponibilidad` - Crear disponibilidad
- `PUT /api/Disponibilidad/{id}` - Actualizar disponibilidad
- `DELETE /api/Disponibilidad/{id}` - Eliminar disponibilidad

#### **📝 Inscripciones**
- `GET /api/Inscripcion` - Obtener todas las inscripciones
- `GET /api/Inscripcion/{id}` - Obtener inscripción por ID
- `POST /api/Inscripcion` - Crear inscripción
- `PUT /api/Inscripcion/{id}` - Actualizar inscripción
- `DELETE /api/Inscripcion/{id}` - Eliminar inscripción

#### **🏃 Visitas**
- `GET /api/Visita` - Obtener todas las visitas
- `GET /api/Visita/{id}` - Obtener visita por ID
- `POST /api/Visita` - Crear visita
- `PUT /api/Visita/{id}` - Actualizar visita
- `DELETE /api/Visita/{id}` - Eliminar visita

#### **👤 Usuarios**
- `GET /api/User` - Obtener todos los usuarios (admin)
- `GET /api/User/{id}` - Obtener usuario por ID (admin)
- `POST /api/User` - Crear usuario (admin)
- `PUT /api/User/{id}` - Actualizar usuario (admin)
- `DELETE /api/User/{id}` - Eliminar usuario (admin)
- `GET /api/User/profile` - Obtener perfil actual
- `PUT /api/User/profile` - Actualizar perfil actual

#### **🏥 Health Checks**
- `GET /health` - Estado de salud de la API
- `GET /ready` - Estado de preparación de la API

### **Ejemplo de Uso**

#### **Autenticación**
```bash
curl -X POST "https://localhost:7001/api/Security/login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "admin@example.com",
    "password": "admin123"
  }'
```

#### **Crear Cliente**
```bash
curl -X POST "https://localhost:7001/api/Cliente" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "nombre": "Juan Pérez",
    "apellido": "García",
    "email": "juan.perez@example.com",
    "telefono": "+1234567890",
    "monto": 1000.50
  }'
```

## 🧪 Testing

### **Ejecutar Tests**
```bash
# Ejecutar todos los tests
dotnet test

# Ejecutar tests con cobertura
dotnet test --collect:"XPlat Code Coverage"

# Ejecutar tests específicos
dotnet test --filter "FullyQualifiedName~ClienteTests"
```

### **Estructura de Tests**
- **Tests unitarios** para todas las entidades
- **Tests de validación** con FluentValidation
- **Tests de controladores** con mocking
- **Tests de servicios** con inyección de dependencias

## 🚀 Despliegue en AWS

### **Prerrequisitos**
- [AWS CLI](https://aws.amazon.com/cli/) configurado
- [Docker](https://www.docker.com/) instalado
- Cuenta AWS con permisos necesarios

### **1. Construir Imagen Docker**
```bash
docker build -t apibtg .
```

### **2. Subir a ECR**
```bash
aws ecr get-login-password --region us-east-1 | docker login --username AWS --password-stdin YOUR_ACCOUNT_ID.dkr.ecr.us-east-1.amazonaws.com
docker tag apibtg:latest YOUR_ACCOUNT_ID.dkr.ecr.us-east-1.amazonaws.com/apibtg:latest
docker push YOUR_ACCOUNT_ID.dkr.ecr.us-east-1.amazonaws.com/apibtg:latest
```

### **3. Desplegar con CloudFormation**
```bash
# Actualizar parámetros en cloudformation/parameters.json
# Ejecutar script de despliegue
.\deploy-aws.ps1 production us-east-1
```

### **Arquitectura AWS**
- **VPC** con subnets públicas y privadas
- **ECS Fargate** para contenedores serverless
- **RDS SQL Server** para base de datos
- **Application Load Balancer** para distribución de tráfico
- **Secrets Manager** para gestión de secretos
- **CloudWatch** para monitoreo y logs

## 📊 Monitoreo y Logs

### **Health Checks**
- **/health**: Estado general de la aplicación
- **/ready**: Estado de preparación (base de datos, servicios externos)

### **Logging**
- **Structured Logging** con Serilog
- **Niveles**: Information, Warning, Error
- **Contexto**: User ID, Request ID, Timestamp
- **Integración**: CloudWatch en AWS

### **Métricas**
- **Tiempo de respuesta** de endpoints
- **Tasa de errores** por endpoint
- **Uso de recursos** (CPU, memoria)
- **Conexiones de base de datos**

## 🔧 Configuración

### **Variables de Entorno**
```bash
# Base de datos
ConnectionStrings__DefaultConnection=your_connection_string

# JWT
JwtSettings__key=your_jwt_secret_key
JwtSettings__Issuer=your_issuer
JwtSettings__Audience=your_audience

# Email (Mailtrap)
EmailSettings__SmtpServer=sandbox.smtp.mailtrap.io
EmailSettings__Port=2525
EmailSettings__Username=your_mailtrap_username
EmailSettings__Password=your_mailtrap_password

# SMS (Twilio)
Twilio__AccountSid=your_twilio_account_sid
Twilio__AuthToken=your_twilio_auth_token
Twilio__FromNumber=your_twilio_phone_number
```

### **Configuración de Logging**
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "ApiBTG.Application": "Information"
    }
  }
}
```

## 🤝 Contribución

### **Proceso de Contribución**
1. **Fork** el repositorio
2. **Crear** una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. **Commit** tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. **Push** a la rama (`git push origin feature/AmazingFeature`)
5. **Abrir** un Pull Request

### **Estándares de Código**
- **C# Coding Conventions** de Microsoft
- **Clean Code** principles
- **SOLID** principles
- **Tests unitarios** para nuevas funcionalidades
- **Documentación** actualizada

### **Estructura de Commits**
```
feat: add new user management feature
fix: resolve authentication issue
docs: update API documentation
test: add unit tests for user service
refactor: improve error handling
```

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo [LICENSE](LICENSE) para más detalles.

## 👥 Autores

- **Didier Avila** - *Desarrollo inicial* - [DidierAvila](https://github.com/DidierAvila)

## 🙏 Agradecimientos

- **Microsoft** por .NET 8 y ASP.NET Core
- **Entity Framework** team por el ORM
- **MediatR** contributors por el patrón mediator
- **FluentValidation** team por las validaciones
- **AWS** por la infraestructura cloud

## 📞 Soporte

Para soporte técnico o preguntas:
- 📧 Email: support@apibtg.com
- 🐛 Issues: [GitHub Issues](https://github.com/DidierAvila/ApiBTG/issues)
- 📖 Wiki: [GitHub Wiki](https://github.com/DidierAvila/ApiBTG/wiki)

---

<div align="center">
  <p>Hecho con ❤️ usando .NET 8</p>
  <p>⭐ Si te gusta este proyecto, dale una estrella en GitHub</p>
</div>