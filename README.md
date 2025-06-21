# DevTest API + Frontend

**Sistema completo para gestión de usuarios**, desarrollado como parte del desafío técnico solicitado por Softec. Incluye:

- API REST en .NET 8 con PostgreSQL (Supabase)
- Autenticación JWT y control de roles
- Frontend en Next.js desplegado en Vercel
- UI moderna y funcional, con validaciones y SweetAlert2

---

## 🌐 URLs públicas

- 🔐 **Frontend login y gestión:**  
  https://dev-test-32y9f4hnw-ricardo-bragas-projects-add6e9d9.vercel.app/login

- 🔧 **API (Swagger UI):**  
  https://devtestapi-usuarios-hxa3emehg5gbaccb.brazilsouth-01.azurewebsites.net/swagger/index.html

---

## 🔍 Acerca del Proyecto

Este proyecto cumple con todos los desafíos planteados por Softec, tanto del lado backend como frontend:

### ✔ Backend (.NET API)

- CRUD completo de usuarios
- Roles (ADMIN y CONSULTOR)
- Estados (ACTIVO / INACTIVO)
- Autenticación con JWT
- Password cifrada con BCrypt
- Validaciones con FluentValidation
- Manejo global de errores con `ProblemDetails`
- Pruebas unitarias (xUnit + Moq)

### ✔ Frontend (Next.js + Tailwind)

- Login de usuario con token JWT
- Redirección por sesión activa
- Pantalla de **lista de usuarios** con:
  - Tabla de datos completa
  - Botón Editar (redirección)
  - Botón Eliminar con confirmación SweetAlert2
- Pantalla de **alta de usuario**
- Pantalla de **edición de usuario**, con todos los campos:
  - Nombre completo, cédula, sexo, dirección, correo, rol, contraseña
- Validaciones detalladas por campo
- Navegación clara y responsive

---

## 📦 Tecnologías utilizadas

### Backend
- .NET 8 Web API
- Entity Framework Core
- PostgreSQL (Supabase)
- JWT + roles
- BCrypt.Net-Next
- FluentValidation
- xUnit, Moq
- Swagger
- Docker (opcional)
- Azure App Service

### Frontend
- Next.js 14 con App Router
- React Hook Form
- Axios
- SweetAlert2
- Tailwind CSS
- Vercel para despliegue

---

## ⚙️ Cómo ejecutar localmente

### 🔧 API

```bash
git clone https://github.com/BragaRicardo/dev-test.git
cd dev-test
git checkout ricardo.braga/desarrollo/DevTest-API
cd DevTest-API
dotnet user-secrets init

# Configurar secretos
dotnet user-secrets set "Jwt:Key" "<TU_JWT_SECRET>"
dotnet user-secrets set "DB_PASSWORD" "<TU_DB_PASSWORD>"
```

Revisar `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=aws-0-us-east-2.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.fstiushspvzplpwuhplq;SSL Mode=Require;Trust Server Certificate=true"
}
```

```bash
# Opcional: levantar PostgreSQL
docker-compose up -d

# Aplicar migraciones
dotnet ef database update

# Ejecutar API
dotnet run
```

### 🖥 Frontend

```bash
cd DevTest-web
npm install
npm run dev
```

Configurar `.env.local` con la API pública:

```
NEXT_PUBLIC_API_URL=https://devtestapi-usuarios-hxa3emehg5gbaccb.brazilsouth-01.azurewebsites.net
```

---

## 📫 Endpoints disponibles (API)

### `/api/usuarios`

| Método | Ruta           | Rol        | Descripción              |
|--------|----------------|------------|--------------------------|
| GET    | `/`            | ADMIN, CONSULTOR | Listar usuarios         |
| GET    | `/{id}`        | ADMIN, CONSULTOR | Obtener usuario por ID  |
| POST   | `/`            | ADMIN      | Crear usuario            |
| PUT    | `/{id}`        | ADMIN      | Editar usuario           |
| DELETE | `/{id}`        | ADMIN      | Eliminar usuario         |

### `/api/auth/login`

- `POST`  
  Input: `{ "correo": "user@example.com", "password": "..." }`  
  Output: `{ token, expiraEn }`

---

## ✅ Validaciones / errores

| Código | Caso                              |
|--------|------------------------------------|
| 400    | Campos inválidos (FluentValidation) |
| 401    | Token inválido o ausente           |
| 403    | Rol sin permiso                    |
| 204    | No encontrado                      |
| 500    | Error interno                      |

---

## 🧪 Pruebas unitarias

Ubicadas en `DevTest-API.Tests`. Ejecutar con:

```bash
cd DevTest-API.Tests
dotnet test
```

Cobertura sobre:
- Crear, editar, eliminar, obtener usuario
- Validación de login

---

## 🚀 Despliegue en Azure App Service

1. Publicar API desde Visual Studio
2. En Azure → Configuración de aplicación:

```env
ConnectionStrings__DefaultConnection = Host=aws-0-us-east-2.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.fstiushspvzplpwuhplq;Password=<TU_DB_PASSWORD>;SSL Mode=Require;Trust Server Certificate=true
Jwt__Key = <TU_JWT_SECRET>
```

3. Reiniciar servicio.

---

## ✅ Cumplimiento completo del desafío Softec

> **BACKEND**
- ✅ CRUD usuarios (POST, PUT, GET, DELETE)
- ✅ Roles (ADMIN / CONSULTOR)
- ✅ Estado (ACTIVO / INACTIVO)
- ✅ Autenticación y autorización
- ✅ JWT con control de acceso
- ✅ Pruebas unitarias

> **FRONTEND**
- ✅ Login funcional con JWT
- ✅ Pantalla de listado de usuarios
- ✅ Alta de usuarios
- ✅ Edición de usuarios
- ✅ Eliminación con confirmación
- ✅ UI amigable y validada
- ✅ Manejo de errores y expiración de token

---

**🌟 Valor agregado**

- UI moderna y responsive
- SweetAlert para interacción con el usuario
- Roles completamente funcionales
- Integración API-Frontend validada
- Readme detallado para facilitar revisión y pruebas

**🎉 Proyecto completo, desplegado y funcional.**

**🙏 Agradecimientos**

Gracias por la oportunidad de realizar este desafío. El resultado fue un proyecto fullstack profesional, con todas las funcionalidades pedidas funcionando de extremo a extremo.

```Autor: Ricardo Javier González BragaContacto: bragaricardo2022@gmail.com```