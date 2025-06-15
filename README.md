<h1 align="center">Bienvenido, futuro compañero!</h1>

¡Saludos de [softec](https://softec.com.py/) Si estás leyendo esto, significa que queremos conocer tus habilidades.

### Acerca del Proyecto

Este proyecto es mantenido por softec, una empresa dedicada a crear soluciones tecnológicas innovadoras. Nuestro objetivo con este proyecto es desarrollar una API Rest simple utilizando .NET, así como integrarla con desafíos frontend. A continuación, encontrarás algunos desafíos interesantes para trabajar en este proyecto.

### Instruccion a tener en cuenta
* Para el backend es requerimiento utilizar el lenguaje **.NET**.
* Utilizar los HTTP METHODS adecuados para cada servicio.
* Para el frontend el framework es a elección
* Para la persistencia se recomienda utilizar postgres u otro motor favor facilitar el DML o un docker-compose.yml para montar.
* Se valorará la facilidad para levantar todo el proyecto y un readme con instrucciones.


### Desafíos Backend

**1. API de Usuarios:** Diseña los servicios necesarios para un CRUD de Usuarios:
* Crear, modificar, eliminar y listar usuarios
* Filtro por estado, nombre y lo que se crea pertinente
Cada Usuario debe contener minimamente los siguientes datos:
* Identificador único
* Nombre completo
* Contraseña (cifrado es opcional)
* Correo electrónico 
* Estado (ACTIVO e INACTIVO) 
* Y otra información relevante.
* Rol (Obs: Los roles pueden ser ADMIN, CONSULTOR)

*Obs: Puedes agregar cualquier servicio que se crea necesario al API.*

*Obs 2: El rol **ADMIN** puede consumir todos los servicios, el rol **CONSULTOR** solo los listados (GET)*

*Obs 3: Los usuarios inactivos no pueden iniciar sesion*

**2. Implementar autenticación y autorización:** Crea una capa de autenticación y autorización para proteger los endpoints de la API. 
* Asegúrate de que solo los usuarios autenticados y autorizados puedan acceder a ciertos recursos.
* No olvides el control de los ROLES.

*Obs: Se sugiere usar ASP.NET Core Identity junto con Authentication & Authorization middleware para la seguridad. Pero el mismo tambien es opcional*

### Test Unitarios

Escribir test unitarios o de integración que verifiquen los anteriores requerimientos funcionales. No es necesario tener full covertura, sino seleccionar los puntos más críticos de la lógica del sistema y enfocarse en eso. Por ejemplo, asegurarse de que se puedan crear correctamente los Usuarios y marcarlas como activos.

### Desafíos Frontend Integrados

1. **Diseñar una interfaz para inicio de sesion** Crea una pagina de inicio de sesion que reciba usuario y contraseña, en caso de exito redirigir a la pagina de listado de usuarios, caso contrario mostrar un mensaje de error. 

1. **Diseñar una interfaz para mostrar la lista de usuarios:** Crea una interfaz web para consumir el endpoint que obtiene la lista de usuarios. Muestra la información de manera atractiva y fácil de entender.

2. **Implementar un formulario para agregar nuevos usuarios:** Diseña un formulario que permita agregar nuevos usuarios a través de la API backend. Asegúrate de validar los campos y proporcionar mensajes de error claros.

Estos desafíos te darán una excelente oportunidad para aprender y poner en práctica tus habilidades de desarrollo en el entorno de .NET y Frontend. ¡Esperamos con ansias ver tus contribuciones a este proyecto!

### Empezando el Desafío

Para empezar crear un fork de este repositorio para implementar los ejercicios.

Adjuntar cualquier documentación al proyecto en forma de archivos con extensión `.md`.

Se recomienda ir haciendo commits a medida que se avanza con la solución. Agrupando estos commits si corresponde hacerlo.

## Cómo Correr el Proyecto

A continuación, se detallan las instrucciones para ejecutar tanto el backend como el frontend de este proyecto por separado, utilizando Docker para la base de datos.

### Requisitos Previos

- **Backend**: [.NET SDK](https://dotnet.microsoft.com/download)
- **Docker**: [Docker](https://www.docker.com/get-started)
- **Frontend**: [Node.js](https://nodejs.org/) y [npm](https://www.npmjs.com/)

### Pasos para Ejecutar el Backend

1. **Clonar el Repositorio**:

   ```
   git clone <https://github.com/BragaRicardo/dev-test.git>
   cd <DevTest.API>
   
   ```
2. **Configurar Docker para la Base de Datos**:
   - En la raíz del proyecto, encontrarás un archivo 
   
     ```
     docker-compose.yml
     ```
	 
      Que contiene la configuración para PostgreSQL.

   ```
   version: '3.8'
   services:
     db:
       image: postgres
       restart: always
       environment:
         POSTGRES_USER: user
         POSTGRES_PASSWORD: password
         POSTGRES_DB: mydatabase
       ports:
         - "5432:5432"
   
   ```

3. **Levantar la Base de Datos**:
   - Desde la raíz del proyecto, ejecuta el siguiente comando para levantar la base de datos en un contenedor Docker:

   ```
   docker-compose up -d
   
   ```
4. **Restaurar Dependencias del Backend**:

   ```
   dotnet restore
   
   ```
5. **Configurar la Conexión a la Base de Datos**:
   - Asegúrate de que tu archivo de configuración:
     
     ```
	   appsettings.json
     ```
	 Esté configurado para conectarse a la base de datos PostgreSQL en Docker. Un ejemplo de cadena de conexión podría ser:
   ```
	   "ConnectionStrings": {
		 "DefaultConnection": "Host=db;Port=5432;Database=mydatabase;Username=user;Password=password;"
	   }
   ```
6. **Ejecutar la Aplicación**:

   ```
   dotnet run
   
   ```
7. **Probar la API**:
   - Puedes usar herramientas como [Postman](https://www.postman.com/) o [cURL](https://curl.se/) para probar los endpoints de la API.

### Pasos para Ejecutar el Frontend

1. **Clonar el Repositorio**:
   (Si no lo has hecho ya)

   ```
   git clone <https://github.com/BragaRicardo/dev-test.git>
   cd <devtest-web>
   
   ```
2. **Instalar Dependencias**:

   ```
   npm install
   
   ```
3. **Ejecutar la Aplicación**:

   ```
   npm run dev
   
   ```
4. **Acceder a la Aplicación**:
   - Abre tu navegador y ve a ```
     http://localhost:3000
     ```

      Para ver la aplicación en funcionamiento.

## Envíar el código para evaluación

Luego al finalizar enviar un email con el link al fork a la persona que te envió este test.


*Ante cualquier duda podes contactarme directamente al correo  [paolo@softec.com.py](mailto:mailto:paolo@softec.com.py) y [ricardo.candia@softec.com.py](mailto:ricardo.candia@softec.com.py)*

¡Buena suerte de parte de todo el equipo de softec!
