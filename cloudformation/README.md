# 🚀 ApiBTG - AWS CloudFormation Deployment

Este directorio contiene todos los templates de AWS CloudFormation necesarios para desplegar la API ApiBTG en AWS de manera automatizada y escalable.

## 📋 Arquitectura

La solución incluye los siguientes componentes:

### 🏗️ Infraestructura Base
- **VPC** con subnets públicas y privadas
- **NAT Gateway** para acceso a internet desde subnets privadas
- **Internet Gateway** para acceso público

### 🗄️ Base de Datos
- **RDS SQL Server** en subnets privadas
- **Multi-AZ** para alta disponibilidad
- **Backup automático** con retención de 7 días
- **Encriptación** habilitada

### 🐳 Aplicación
- **ECS Fargate** para ejecutar contenedores sin servidores
- **Application Load Balancer** para distribución de carga
- **Auto Scaling** basado en métricas de CloudWatch
- **Health Checks** automáticos

### 🔐 Seguridad
- **Secrets Manager** para gestión segura de credenciales
- **Security Groups** con reglas mínimas necesarias
- **IAM Roles** con permisos específicos
- **VPC** aislada con acceso controlado

### 📊 Monitoreo
- **CloudWatch Logs** para logs de aplicación
- **Métricas** de ECS y ALB
- **Alarmas** configurables

## 📁 Estructura de Archivos

```
cloudformation/
├── main.yaml              # Template principal (orquesta todo)
├── vpc.yaml               # VPC y networking
├── rds.yaml               # Base de datos SQL Server
├── ecs.yaml               # ECS Fargate y ALB
├── secrets.yaml           # Secrets Manager
├── parameters.json        # Parámetros de ejemplo
├── Dockerfile             # Containerización de la app
├── deploy.sh              # Script de despliegue
└── README.md              # Este archivo
```

## 🛠️ Prerrequisitos

### 1. AWS CLI
```bash
# Instalar AWS CLI
curl "https://awscli.amazonaws.com/awscli-exe-linux-x86_64.zip" -o "awscliv2.zip"
unzip awscliv2.zip
sudo ./aws/install

# Configurar credenciales
aws configure
```

### 2. Docker (para construir la imagen)
```bash
# Instalar Docker
sudo apt-get update
sudo apt-get install docker.io
sudo usermod -aG docker $USER
```

### 3. ECR Repository
```bash
# Crear repositorio ECR
aws ecr create-repository --repository-name apibtg --region us-east-1
```

## 🚀 Pasos de Despliegue

### 1. Preparar la Aplicación

#### Construir y subir la imagen Docker:
```bash
# Login a ECR
aws ecr get-login-password --region us-east-1 | docker login --username AWS --password-stdin 123456789012.dkr.ecr.us-east-1.amazonaws.com

# Construir imagen
docker build -t apibtg .

# Tag de la imagen
docker tag apibtg:latest 123456789012.dkr.ecr.us-east-1.amazonaws.com/apibtg:latest

# Subir a ECR
docker push 123456789012.dkr.ecr.us-east-1.amazonaws.com/apibtg:latest
```

### 2. Configurar Parámetros

Editar `parameters.json` con tus valores:
```json
{
  "ParameterKey": "ImageUri",
  "ParameterValue": "123456789012.dkr.ecr.us-east-1.amazonaws.com/apibtg:latest"
}
```

### 3. Desplegar Infraestructura

#### Opción A: Script Automatizado
```bash
# Dar permisos de ejecución
chmod +x deploy.sh

# Desplegar
./deploy.sh production us-east-1
```

#### Opción B: Manual
```bash
# Crear bucket S3 para templates
aws s3 mb s3://apibtg-cloudformation-production-123456789012

# Subir templates
aws s3 cp . s3://apibtg-cloudformation-production-123456789012/cloudformation/ --recursive

# Desplegar stack
aws cloudformation create-stack \
  --stack-name apibtg-production \
  --template-url https://apibtg-cloudformation-production-123456789012.s3.us-east-1.amazonaws.com/cloudformation/main.yaml \
  --parameters file://parameters.json \
  --capabilities CAPABILITY_IAM CAPABILITY_NAMED_IAM \
  --region us-east-1
```

## 🔧 Configuración de la Aplicación

### Variables de Entorno
La aplicación espera las siguientes variables de entorno:

```bash
# Base de datos
ConnectionStrings__DefaultConnection=Server=db-endpoint;Database=BTG;User Id=admin;Password=password;

# JWT
JwtSettings__key=your-jwt-secret-key
JwtSettings__Issuer=https://api.apibtg.com
JwtSettings__Audience=https://api.apibtg.com

# Email (Mailtrap)
EmailSettings__SmtpServer=sandbox.smtp.mailtrap.io
EmailSettings__Port=2525
EmailSettings__Username=your-mailtrap-username
EmailSettings__Password=your-mailtrap-password

# Twilio
Twilio__AccountSid=your-twilio-account-sid
Twilio__AuthToken=your-twilio-auth-token
Twilio__FromNumber=+12513365758
```

### Health Check Endpoint
La aplicación debe exponer un endpoint de health check en `/health`:

```csharp
[HttpGet("health")]
public IActionResult Health()
{
    return Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
}
```

## 📊 Monitoreo y Logs

### CloudWatch Logs
Los logs de la aplicación se envían automáticamente a CloudWatch:
- **Log Group**: `/ecs/production-ApiBTG`
- **Retención**: 30 días

### Métricas Disponibles
- **ECS**: CPU, memoria, número de tareas
- **ALB**: requests, latencia, errores
- **RDS**: conexiones, CPU, espacio en disco

## 🔄 Actualizaciones

### Actualizar la Aplicación
```bash
# Construir nueva imagen
docker build -t apibtg .

# Subir a ECR
docker push 123456789012.dkr.ecr.us-east-1.amazonaws.com/apibtg:latest

# Forzar nuevo despliegue
aws ecs update-service --cluster production-ApiBTG-Cluster --service production-ApiBTG-Service --force-new-deployment
```

### Actualizar Infraestructura
```bash
# Actualizar stack
./deploy.sh production us-east-1
```

## 🗑️ Limpieza

Para eliminar toda la infraestructura:
```bash
aws cloudformation delete-stack --stack-name apibtg-production --region us-east-1
```

## 🔒 Seguridad

### Credenciales
- Todas las credenciales se almacenan en AWS Secrets Manager
- Las contraseñas nunca se almacenan en texto plano
- Rotación automática de credenciales

### Red
- La aplicación corre en subnets privadas
- Solo el ALB está en subnets públicas
- Security Groups con reglas mínimas

### IAM
- Roles con permisos específicos
- Principio de menor privilegio
- Sin credenciales hardcodeadas

## 📞 Soporte

Para problemas o preguntas:
1. Revisar logs en CloudWatch
2. Verificar estado de los servicios en AWS Console
3. Consultar métricas de CloudWatch

## 💰 Costos Estimados

**Desarrollo/Staging** (t3.micro, 1 instancia):
- ~$50-80/mes

**Producción** (t3.small, 2 instancias, Multi-AZ):
- ~$150-250/mes

*Los costos varían según el uso y la región.* 