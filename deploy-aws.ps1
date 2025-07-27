# Script de despliegue para ApiBTG en AWS CloudFormation (PowerShell)
# Uso: .\deploy-aws.ps1 [environment] [region]

param(
    [string]$Environment = "production",
    [string]$Region = "us-east-1"
)

$ErrorActionPreference = "Stop"

$StackName = "apibtg-$Environment"

Write-Host "🚀 Iniciando despliegue de ApiBTG en AWS CloudFormation" -ForegroundColor Green
Write-Host "Environment: $Environment" -ForegroundColor Yellow
Write-Host "Region: $Region" -ForegroundColor Yellow
Write-Host "Stack Name: $StackName" -ForegroundColor Yellow

# Verificar que AWS CLI esté configurado
try {
    $caller = aws sts get-caller-identity 2>$null
    if ($LASTEXITCODE -ne 0) {
        throw "AWS CLI no está configurado"
    }
    Write-Host "✅ AWS CLI configurado correctamente" -ForegroundColor Green
} catch {
    Write-Host "❌ Error: AWS CLI no está configurado. Ejecuta 'aws configure' primero." -ForegroundColor Red
    exit 1
}

# Obtener Account ID
$accountId = (aws sts get-caller-identity --query Account --output text 2>$null).Trim()
if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Error: No se pudo obtener el Account ID" -ForegroundColor Red
    exit 1
}

# Crear bucket S3 para los templates
$BucketName = "apibtg-cloudformation-$Environment-$accountId"
Write-Host "📦 Usando bucket S3: $BucketName" -ForegroundColor Cyan

# Verificar si el bucket existe
$bucketExists = aws s3 ls "s3://$BucketName" 2>$null
if ($LASTEXITCODE -ne 0) {
    Write-Host "📦 Creando bucket S3..." -ForegroundColor Yellow
    aws s3 mb "s3://$BucketName" --region $Region
    if ($LASTEXITCODE -ne 0) {
        Write-Host "❌ Error: No se pudo crear el bucket S3" -ForegroundColor Red
        exit 1
    }
    aws s3api put-bucket-versioning --bucket "$BucketName" --versioning-configuration Status=Enabled
    Write-Host "✅ Bucket S3 creado exitosamente" -ForegroundColor Green
} else {
    Write-Host "✅ Bucket S3 ya existe" -ForegroundColor Green
}

# Subir templates a S3
Write-Host "📤 Subiendo templates a S3..." -ForegroundColor Yellow
aws s3 cp cloudformation/ s3://$BucketName/cloudformation/ --recursive --exclude "*.json" --exclude "*.sh" --exclude "*.md" --exclude "*.ps1"
if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Error: No se pudieron subir los templates a S3" -ForegroundColor Red
    exit 1
}

# URL del template principal
$TemplateUrl = "https://$BucketName.s3.$Region.amazonaws.com/cloudformation/main.yaml"

# Verificar si el stack existe
$stackExists = aws cloudformation describe-stacks --stack-name $StackName --region $Region 2>$null
if ($LASTEXITCODE -eq 0) {
    Write-Host "🔄 Stack existente encontrado. Actualizando..." -ForegroundColor Yellow
    $Operation = "update-stack"
} else {
    Write-Host "🆕 Creando nuevo stack..." -ForegroundColor Yellow
    $Operation = "create-stack"
}

# Desplegar stack
Write-Host "🚀 Desplegando stack..." -ForegroundColor Green
aws cloudformation $Operation `
    --stack-name $StackName `
    --template-url $TemplateUrl `
    --parameters file://cloudformation/parameters.json `
    --capabilities CAPABILITY_IAM CAPABILITY_NAMED_IAM `
    --region $Region `
    --tags Key=Environment,Value=$Environment Key=Project,Value=ApiBTG

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Error: No se pudo desplegar el stack" -ForegroundColor Red
    exit 1
}

# Esperar a que el stack se complete
Write-Host "⏳ Esperando a que el stack se complete..." -ForegroundColor Yellow
if ($Operation -eq "create-stack") {
    aws cloudformation wait stack-create-complete --stack-name $StackName --region $Region
} else {
    aws cloudformation wait stack-update-complete --stack-name $StackName --region $Region
}

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Error: El stack no se completó correctamente" -ForegroundColor Red
    exit 1
}

# Obtener outputs
Write-Host "📋 Obteniendo outputs del stack..." -ForegroundColor Cyan
aws cloudformation describe-stacks `
    --stack-name $StackName `
    --region $Region `
    --query 'Stacks[0].Outputs' `
    --output table

# Obtener API Endpoint
$apiEndpoint = aws cloudformation describe-stacks `
    --stack-name $StackName `
    --region $Region `
    --query 'Stacks[0].Outputs[?OutputKey==`APIEndpoint`].OutputValue' `
    --output text

Write-Host "✅ Despliegue completado exitosamente!" -ForegroundColor Green
Write-Host "🌐 API Endpoint: $apiEndpoint" -ForegroundColor Cyan 