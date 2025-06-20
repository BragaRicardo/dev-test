# DevTest-API

**API REST de usuarios** construida en .NET 8, integrada con PostgreSQL (Supabase) y desplegada en Azure App Service.

---

## 🌐 URL pública

> Swagger UI en Azure:  
> [https://devtestapi-usuarios-hxa3emehg5gbaccb.brazilsouth-01.azurewebsites.net/swagger/index.html](https://devtestapi-usuarios-hxa3emehg5gbaccb.brazilsouth-01.azurewebsites.net/swagger/index.html)

---

## 🔍 Acerca del Proyecto

Este proyecto, mantenido por Softec, cumple con los siguientes retos:

1. **CRUD de Usuarios**

   - Crear, modificar, eliminar y listar usuarios.
   - Filtros por estado (ACTIVO/INACTIVO) y nombre.
   - Campos mínimos:
     - Identificador único
     - Nombre completo
     - Correo electrónico
     - Contraseña (cifrada con BCrypt)
     - Estado (ACTIVO / INACTIVO)
     - Rol (ADMIN / CONSULTOR)
   - Reglas de acceso:
     - **ADMIN**: todos los endpoints.
     - **CONSULTOR**: únicamente `GET`.
     - Usuarios inactivos NO pueden iniciar sesión.

2. **Autenticación y autorización**

   - JWT con clave secreta (`Jwt:Key`).
   - Políticas de roles (`SoloAdmin`, `AdminOConsultor`).

3. **Manejo de errores global**

   - Hellang.Middleware.ProblemDetails para responder en JSON con:
     - `400 BadRequest`
     - `401 Unauthorized`
     - `403 Forbidden`
     - `204 NoContent`
     - `500 InternalServerError`

4. **Pruebas unitarias**

   - xUnit + Moq + coverlet
   - Tests en `DevTest-API.Tests` para CRUD y validación de credenciales.

5. **Despliegue en la nube**

   - Azure App Service para la API.
   - Variables de entorno para conexiones y secretos.

---

## 🧰 Tecnologías

- **.NET 8 (Web API)**
- **Entity Framework Core** + PostgreSQL (Supabase)
- **Docker Compose** (opcional, para desarrollo local)
- **JWT** (System.IdentityModel.Tokens)
- **BCrypt.Net-Next** (hash de contraseña)
- **FluentValidation** (validaciones de entrada)
- **Hellang.Middleware.ProblemDetails** (errores globales)
- **Swashbuckle/Swagger** (documentación)
- **xUnit + Moq + coverlet** (pruebas unitarias)

---

## ⚙️ Prerrequisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Cuenta en Supabase (PostgreSQL)
- Cuenta en Azure (App Service)

---

## 💻 Desarrollo local

1. **Clonar y cambiar de rama**

   ```bash
   git clone https://github.com/BragaRicardo/dev-test.git
   cd dev-test
   git checkout ricardo.braga/desarrollo/DevTest-API
   ```

2. **Configurar secretos (no exponer en el repo)**

   ```bash
   cd DevTest-API
   dotnet user-secrets init

   dotnet user-secrets set "Jwt:Key"      "<TU_JWT_SECRET>"
   dotnet user-secrets set "DB_PASSWORD"  "<TU_DB_PASSWORD>"
   ```

   > El `appsettings.json` mantiene la cadena sin contraseña:
   >
   > ```json
   > "ConnectionStrings": {
   >   "DefaultConnection": "Host=aws-0-us-east-2.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.fstiushspvzplpwuhplq;SSL Mode=Require;Trust Server Certificate=true"
   > }
   > ```

3. **(Opcional) Levantar PostgreSQL en Docker**

   ```bash
   docker-compose up -d
   ```

4. **Aplicar migraciones**

   ```bash
   dotnet ef database update
   ```

5. **Ejecutar la API**

   ```bash
   dotnet run
   ```

   Luego abrir: `https://localhost:5001/swagger/index.html`

---

## 📫 Endpoints disponibles

### Usuarios (`/api/usuarios`)

- `GET  /`  
  Lista usuarios (filtros opcionales) → **ADMIN/CONSULTOR**
- `GET  /{id}`  
  Obtener por ID → **ADMIN/CONSULTOR**
- `POST /`  
  Crear nuevo → **Solo ADMIN**
- `PUT  /{id}`  
  Actualizar → **Solo ADMIN**
- `DELETE /{id}`  
  Eliminar → **Solo ADMIN**

### Autenticación (`/api/auth/login`)

- `POST`  
  Request: `{ "correo": "...", "password": "..." }`  
  Response `200`: `{ token, expiraEn }`  
  Response `401` si falla.

---

## ✅ Manejo de errores

- **400 BadRequest** → Validaciones fallidas
- **401 Unauthorized** → JWT inválido o ausente
- **403 Forbidden** → Rol sin permiso
- **204 NoContent** → Recurso no encontrado
- **500 InternalServerError** → Error inesperado (ProblemDetails JSON)

---

## 🧪 Pruebas unitarias

En `DevTest-API.Tests` hay tests para:

- `CrearAsync`, `ObtenerTodosAsync`, `ObtenerPorIdAsync`
- `ActualizarAsync`, `EliminarAsync`
- `ValidarCredencialesAsync` (éxito y fallo)

**Ejecutar**:

```bash
cd DevTest-API.Tests
dotnet test
```

---

## 🚀 Despliegue en Azure App Service

1. **Publicar** desde Visual Studio → Azure App Service.
2. En el portal de Azure → **Configuración** → **Configuración de la aplicación**, añadir:
   - `ConnectionStrings__DefaultConnection`
     ```text
     Host=aws-0-us-east-2.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.fstiushspvzplpwuhplq;Password=<TU_DB_PASSWORD>;SSL Mode=Require;Trust Server Certificate=true
     ```
   - `Jwt__Key` = `<TU_JWT_SECRET>`
3. **Reiniciar** App Service.

---