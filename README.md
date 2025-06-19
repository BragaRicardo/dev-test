# DevTest‑API

API REST en .NET 8 para gestión de usuarios con autenticación JWT, CRUD, filtros, roles (**ADMIN / CONSULTOR**) y middleware consolidado.

## 🛠️ Tecnologías

* **.NET 8 (Web API)**
* **Entity Framework Core** + PostgreSQL
* **Docker Compose** (para DB, opcional con Supabase)
* **JWT** para autenticación/autorización
* **BCrypt** para hash de contraseña
* **FluentValidation** para validación de entrada
* **Hellang.Middleware.ProblemDetails** para manejo de errores global
* **xUnit + Moq + coverlet** para pruebas unitarias

## 🔧 Requisitos

* .NET 8 SDK
* Docker (opcional, si vas a utilizar containerizado)
* Visual Studio 2022 o VS Code
* (Opcional) Cuenta y credenciales de Supabase

## 🚀 Levantar la API

1. Clona el repositorio y sitúate en la rama `ricardo.braga/desarrollo/DevTest-API`.
2. Crea un archivo **User Secrets** para las claves:

   ```bash
   dotnet user-secrets set "Jwt:Key" "<tu–clave–secreta>"
   dotnet user-secrets set "DB_PASSWORD" "<tu–password–base>"
   ```
3. (Opcional) Ejecuta `docker-compose up` para levantar PostgreSQL local.
4. O bien, configúrala en Supabase y actualiza `DefaultConnection` y `DB_PASSWORD`.
5. Ejecuta la aplicación con `dotnet run` o desde Visual Studio.
   La API estará en [https://localhost:5186](https://localhost:5186) (JSON y Swagger).

---

## 📄 Endpoints disponibles

### Usuarios (`/api/usuarios`)

* **GET** `/` (listar con filtros opcionales) — Roles ADMIN/CONSULTOR
* **GET** `/{id}` (buscar por ID) — ADMIN/CONSULTOR
* **POST** / **PUT** / **DELETE** — Solo rol ADMIN

### Autenticación (`/api/auth/login`)

* **POST** con `{ correo, password }`
  Devuelve token JWT en caso de éxito.

---

## ✅ Manejo de errores

* **400 BadRequest** – Validaciones fallidas
* **401 Unauthorized** – JWT inválido o ausente
* **403 Forbidden** – Rol sin permiso
* **204 NoContent** – No se encontró recurso
* **500 InternalServerError** – Error inesperado (será transformado en `ProblemDetails` JSON)

---

## 🧪 Pruebas unitarias

Se han agregado tests con **xUnit** y **Moq** en el proyecto `DevTest_API.Tests`:

* `CrearAsync`, `ObtenerTodosAsync`, `ObtenerPorIdAsync`, `ActualizarAsync`, `EliminarAsync`
* `ValidarCredencialesAsync` (login correcto y fallo)

Ejecútalos desde Visual Studio con el explorador de pruebas, o por CLI:

```bash
dotnet test DevTest_API.Tests/DevTest_API.Tests.csproj
```

Cobertura de tests integrada con **coverlet**.

---

## 📘 Swagger

Disponible en modo desarrollo vía
[https://localhost:5186/swagger](https://localhost:5186/swagger)

Incluye secciones para:

* Autenticación (Bearer)
* Filtros
* Respuestas posibles
