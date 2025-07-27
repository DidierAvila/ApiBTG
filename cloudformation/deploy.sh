#!/bin/bash

# Script de despliegue para ApiBTG en AWS CloudFormation
# Uso: ./deploy.sh [environment] [region]

set -e

# Configuración por defecto
ENVIRONMENT=${1:-production}
REGION=${2:-us-east-1}
STACK_NAME="apibtg-${ENVIRONMENT}"

echo "🚀 Iniciando despliegue de ApiBTG en AWS CloudFormation"
echo "Environment: $ENVIRONMENT"
echo "Region: $REGION"
echo "Stack Name: $STACK_NAME"

# Verificar que AWS CLI esté configurado
if ! aws sts get-caller-identity &> /dev/null; then
    echo "❌ Error: AWS CLI no está configurado. Ejecuta 'aws configure' primero."
    exit 1
fi

# Verificar que los templates existan
if [ ! -f "main.yaml" ]; then
    echo "❌ Error: No se encontró el archivo main.yaml"
    exit 1
fi

# Crear bucket S3 para los templates (si no existe)
BUCKET_NAME="apibtg-cloudformation-${ENVIRONMENT}-$(aws sts get-caller-identity --query Account --output text)"
echo "📦 Usando bucket S3: $BUCKET_NAME"

# Crear bucket si no existe
if ! aws s3 ls "s3://$BUCKET_NAME" &> /dev/null; then
    echo "📦 Creando bucket S3..."
    aws s3 mb "s3://$BUCKET_NAME" --region $REGION
    aws s3api put-bucket-versioning --bucket "$BUCKET_NAME" --versioning-configuration Status=Enabled
fi

# Subir templates a S3
echo "📤 Subiendo templates a S3..."
aws s3 cp . s3://$BUCKET_NAME/cloudformation/ --recursive --exclude "*.json" --exclude "*.sh" --exclude "*.md"

# URL del template principal
TEMPLATE_URL="https://$BUCKET_NAME.s3.$REGION.amazonaws.com/cloudformation/main.yaml"

# Verificar si el stack existe
if aws cloudformation describe-stacks --stack-name $STACK_NAME --region $REGION &> /dev/null; then
    echo "🔄 Stack existente encontrado. Actualizando..."
    OPERATION="update-stack"
else
    echo "🆕 Creando nuevo stack..."
    OPERATION="create-stack"
fi

# Desplegar stack
echo "🚀 Desplegando stack..."
aws cloudformation $OPERATION \
    --stack-name $STACK_NAME \
    --template-url $TEMPLATE_URL \
    --parameters file://parameters.json \
    --capabilities CAPABILITY_IAM CAPABILITY_NAMED_IAM \
    --region $REGION \
    --tags Key=Environment,Value=$ENVIRONMENT Key=Project,Value=ApiBTG

# Esperar a que el stack se complete
echo "⏳ Esperando a que el stack se complete..."
aws cloudformation wait stack-$([ "$OPERATION" = "create-stack" ] && echo "create" || echo "update")-complete \
    --stack-name $STACK_NAME \
    --region $REGION

# Obtener outputs
echo "📋 Obteniendo outputs del stack..."
aws cloudformation describe-stacks \
    --stack-name $STACK_NAME \
    --region $REGION \
    --query 'Stacks[0].Outputs' \
    --output table

echo "✅ Despliegue completado exitosamente!"
echo "🌐 API Endpoint: $(aws cloudformation describe-stacks --stack-name $STACK_NAME --region $REGION --query 'Stacks[0].Outputs[?OutputKey==`APIEndpoint`].OutputValue' --output text)" 