# DevTest-API

**API REST de usuarios** constru铆da en .NET 8, integrada con PostgreSQL (Supabase) y desplegada en Azure App Service.

---

## 馃寪 URL p煤blica

> Swagger UI en Azure:\
> [https://devtestapi-usuarios-hxa3emehg5gbaccb.brazilsouth-01.azurewebsites.net/swagger/index.html](https://devtestapi-usuarios-hxa3emehg5gbaccb.brazilsouth-01.azurewebsites.net/swagger/index.html)

---

## 馃搵 Acerca del Proyecto

Este proyecto, mantenido por Softec, cumple con los siguientes retos:

1. **CRUD de Usuarios**

   - Crear, modificar, eliminar y listar usuarios.
   - Filtros por estado (ACTIVO/INACTIVO) y nombre.
   - Campos m铆nimos:
     - Identificador 煤nico
     - Nombre completo
     - Correo electr贸nico
     - Contrase帽a (cifrada con BCrypt)
     - Estado (ACTIVO / INACTIVO)
     - Rol (ADMIN / CONSULTOR)
   - Reglas de acceso:
     - **ADMIN**: todos los endpoints.
     - **CONSULTOR**: 煤nicamente `GET`.
     - Usuarios inactivos NO pueden iniciar sesi贸n.

2. **Autenticaci贸n y autorizaci贸n**

   - JWT con clave secreta (`Jwt:Key`).
   - Pol铆ticas de roles (`SoloAdmin`, `AdminOConsultor`).

3. **Manejo de errores global**

   - Hellang.Middleware.ProblemDetails para responder en JSON con
     - `400 BadRequest`
     - `401 Unauthorized`
     - `403 Forbidden`
     - `204 NoContent`
     - `500 InternalServerError`

4. **Pruebas unitarias**

   - xUnit + Moq + coverlet
   - Tests en `DevTest-API.Tests` para CRUD y validaci贸n de credenciales.

5. **Despliegue en la nube**

   - Azure App Service para la API.
   - Variables de entorno para conexiones y secretos.

---

## 馃洜锔?Tecnolog铆as

- **.NET 8 (Web API)**
- **Entity Framework Core** + PostgreSQL (Supabase)
- **Docker Compose** (opcional, para desarrollo local)
- **JWT** (System.IdentityModel.Tokens)
- **BCrypt.Net-Next** (hash de contrase帽a)
- **FluentValidation** (validaciones de entrada)
- **Hellang.Middleware.ProblemDetails** (errores globales)
- **Swashbuckle/Swagger** (documentaci贸n)
- **xUnit + Moq + coverlet** (pruebas unitarias)

---

## 鈿欙笍 Prerrequisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Cuenta en Supabase (PostgreSQL)
- Cuenta en Azure (App Service)

---

## 馃彙 Desarrollo local

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

   > El `appsettings.json` mantiene la cadena sin contrase帽a:
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

## 馃搫 Endpoints disponibles

### Usuarios (`/api/usuarios`)

- `GET  /`\
  Lista usuarios (filtros opcionales) 鈥?**ADMIN/CONSULTOR**
- `GET  /{id}`\
  Obtener por ID 鈥?**ADMIN/CONSULTOR**
- `POST /`\
  Crear nuevo 鈥?**Solo ADMIN**
- `PUT  /{id}`\
  Actualizar 鈥?**Solo ADMIN**
- `DELETE /{id}`\
  Eliminar 鈥?**Solo ADMIN**

### Autenticaci贸n (`/api/auth/login`)

- `POST`\
  Request: `{ "correo": "...", "password": "..." }`\
  Response `200`: `{ token, expiraEn }`\
  Response `401` si falla.

---

## 鉁?Manejo de errores

- **400 BadRequest** 鈥?Validaciones fallidas
- **401 Unauthorized** 鈥?JWT inv谩lido o ausente
- **403 Forbidden** 鈥?Rol sin permiso
- **204 NoContent** 鈥?Recurso no encontrado
- **500 InternalServerError** 鈥?Error inesperado (ProblemDetails JSON)

---

## 馃И Pruebas unitarias

En `DevTest-API.Tests` hay tests para:

- `CrearAsync`, `ObtenerTodosAsync`, `ObtenerPorIdAsync`
- `ActualizarAsync`, `EliminarAsync`
- `ValidarCredencialesAsync` (茅xito y fallo)

**Ejecutar**:

```bash
cd DevTest-API.Tests
dotnet test
```

---

## 馃殌 Despliegue en Azure App Service

1. **Publicar** desde Visual Studio 鈫?Azure App Service.
2. En el portal de Azure 鈫?**Configuraci贸n** 鈫?**Configuraci贸n de la aplicaci贸n**, a帽adir:
   - `ConnectionStrings__DefaultConnection`
     ```text
     Host=aws-0-us-east-2.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.fstiushspvzplpwuhplq;Password=<TU_DB_PASSWORD>;SSL Mode=Require;Trust Server Certificate=true
     ```
   - `Jwt__Key` = `<TU_JWT_SECRET>`
3. **Reiniciar** App Service.

---

