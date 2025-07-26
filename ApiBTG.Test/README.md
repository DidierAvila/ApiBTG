# Pruebas Unitarias - ApiBTG.Test

Este proyecto contiene las pruebas unitarias para las entidades del dominio de la aplicación ApiBTG.

## Estructura del Proyecto

```
ApiBTG.Test/
├── Clientes/
│   └── ClienteTests.cs
├── Inscripciones/
│   └── InscripcionTests.cs
├── Productos/
│   └── ProductoTests.cs
├── Sucursales/
│   └── SucursalTests.cs
├── Disponibilidades/
│   └── DisponibilidadTests.cs
├── Visitas/
│   └── VisitaTests.cs
├── Users/
│   └── UserTests.cs
└── README.md
```

## Dependencias

- **xUnit**: Framework de pruebas unitarias
- **FluentAssertions**: Biblioteca para aserciones más legibles
- **Moq**: Framework para mocking
- **Microsoft.EntityFrameworkCore.InMemory**: Para pruebas con base de datos en memoria

## Entidades Probadas

### 1. Cliente (`ClienteTests.cs`)

**Propiedades probadas:**
- Id (int)
- Nombre (string, max 255 caracteres)
- Apellidos (string, max 255 caracteres)
- Ciudad (string, max 255 caracteres)
- Monto (decimal)
- UsuarioId (int?, nullable)
- Inscripciones (ICollection<Inscripcion>)
- Visitas (ICollection<Visita>)
- Usuario (User, nullable)

**Casos de prueba incluyen:**
- Creación con datos válidos
- Validación de valores límite (0, valores positivos, negativos)
- Validación de strings vacíos y largos
- Validación de colecciones vacías y nulas
- Validación de relaciones con otras entidades
- Pruebas parametrizadas con diferentes valores válidos

### 2. Inscripcion (`InscripcionTests.cs`)

**Propiedades probadas:**
- Id (int)
- IdCliente (int)
- IdDisponibilidad (int)
- Cliente (Cliente, navigation property)
- Disponibilidad (Disponibilidad, navigation property)

**Casos de prueba incluyen:**
- Creación con datos válidos
- Validación de IDs (cero, positivos, negativos, máximo, mínimo)
- Validación de navigation properties nulas y asignadas
- Validación de relaciones entre entidades
- Pruebas parametrizadas con diferentes combinaciones de IDs

### 3. Producto (`ProductoTests.cs`)

**Propiedades probadas:**
- Id (int)
- Nombre (string, max 255 caracteres)
- TipoProducto (string, max 255 caracteres)
- Disponibilidades (ICollection<Disponibilidad>)

**Casos de prueba incluyen:**
- Creación con datos válidos
- Validación de strings vacíos y largos
- Validación de caracteres especiales y números en nombres
- Validación de colecciones de disponibilidades
- Pruebas parametrizadas con diferentes tipos de productos

### 4. Sucursal (`SucursalTests.cs`)

**Propiedades probadas:**
- Id (int)
- Nombre (string, max 255 caracteres)
- Ciudad (string, max 255 caracteres)
- Disponibilidades (ICollection<Disponibilidad>)
- Visitas (ICollection<Visita>)

**Casos de prueba incluyen:**
- Creación con datos válidos
- Validación de nombres y ciudades con caracteres especiales
- Validación de colecciones de disponibilidades y visitas
- Pruebas parametrizadas con diferentes ciudades españolas

### 5. Disponibilidad (`DisponibilidadTests.cs`)

**Propiedades probadas:**
- Id (int)
- IdSucursal (int)
- IdProducto (int)
- MontoMinimo (decimal)
- Sucursal (Sucursal, navigation property)
- Producto (Producto, navigation property)

**Casos de prueba incluyen:**
- Creación con datos válidos
- Validación de montos mínimos (cero, negativos, decimales, máximo)
- Validación de navigation properties
- Validación de relaciones entre sucursal y producto
- Pruebas parametrizadas con diferentes montos

### 6. Visita (`VisitaTests.cs`)

**Propiedades probadas:**
- Id (int)
- IdSucursal (int)
- IdCliente (int)
- FechaVisita (DateTime)
- TipoAccion (string, max 50 caracteres)
- Sucursal (Sucursal, navigation property)
- Cliente (Cliente, navigation property)

**Casos de prueba incluyen:**
- Creación con datos válidos
- Validación de fechas (actual, pasada, futura, con hora específica)
- Validación de tipos de acción (consulta, pago, solicitud, etc.)
- Validación de navigation properties
- Pruebas parametrizadas con diferentes tipos de acciones

### 7. User (`UserTests.cs`)

**Propiedades probadas:**
- Id (int)
- FirstName (string, max 255 caracteres)
- LastName (string, max 255 caracteres)
- Email (string, max 100 caracteres)
- Password (string, max 20 caracteres)
- Role (string, max 255 caracteres)
- NotificacionPreferida (string, max 10 caracteres, default "Email")
- Telefono (string, max 20 caracteres, nullable)

**Casos de prueba incluyen:**
- Creación con datos válidos
- Validación de emails con diferentes formatos
- Validación de nombres con caracteres especiales
- Validación de preferencias de notificación (Email/SMS)
- Validación de teléfonos con formato español
- Pruebas parametrizadas con diferentes roles y tipos de datos

## Convenciones de Nomenclatura

### Nombres de Tests
- Formato: `Entidad_ConCondicion_DebeResultado`
- Ejemplo: `Cliente_ConDatosValidos_DebeCrearseCorrectamente`

### Organización de Tests
- **Arrange**: Preparación de datos y objetos
- **Act**: Ejecución de la acción a probar
- **Assert**: Verificación de resultados esperados

### Tipos de Tests
- **Tests Unitarios**: Prueban una funcionalidad específica
- **Tests Parametrizados**: Usan `[Theory]` y `[InlineData]` para probar múltiples valores
- **Tests de Validación**: Verifican restricciones y validaciones
- **Tests de Relaciones**: Verifican navigation properties y relaciones entre entidades

## Ejecución de Pruebas

### Desde Visual Studio
1. Abrir el proyecto `ApiBTG.Test`
2. Ir a Test Explorer
3. Ejecutar todas las pruebas o seleccionar pruebas específicas

### Desde Línea de Comandos
```bash
# Ejecutar todas las pruebas
dotnet test

# Ejecutar pruebas con cobertura
dotnet test --collect:"XPlat Code Coverage"

# Ejecutar pruebas específicas
dotnet test --filter "FullyQualifiedName~ClienteTests"
```

## Cobertura de Pruebas

Las pruebas cubren:
- ✅ Creación de entidades con datos válidos
- ✅ Validación de propiedades requeridas
- ✅ Validación de restricciones de longitud
- ✅ Validación de tipos de datos
- ✅ Validación de valores límite
- ✅ Validación de navigation properties
- ✅ Validación de colecciones
- ✅ Validación de valores por defecto
- ✅ Validación de caracteres especiales
- ✅ Validación de formatos específicos (email, teléfono)

## Mantenimiento

### Agregar Nuevas Pruebas
1. Crear el archivo de pruebas en la carpeta correspondiente
2. Seguir las convenciones de nomenclatura
3. Incluir casos de prueba para valores válidos, límite y edge cases
4. Documentar casos especiales en comentarios

### Actualizar Pruebas Existentes
1. Mantener la consistencia con las convenciones establecidas
2. Actualizar documentación si se cambian los casos de prueba
3. Verificar que las pruebas sigan siendo relevantes después de cambios en las entidades

## Notas Importantes

- Todas las pruebas usan **FluentAssertions** para aserciones más legibles
- Las pruebas están diseñadas para ser independientes y no tener efectos secundarios
- Se incluyen casos de prueba para valores límite y edge cases
- Las pruebas parametrizadas ayudan a reducir duplicación de código
- Se mantiene consistencia en el estilo y estructura de todas las pruebas 