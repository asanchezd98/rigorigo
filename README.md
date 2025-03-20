Rigo Rigo - Sistema de Ventas
Este proyecto es un sistema de ventas para la tienda de ciclismo "Rigo Rigo". Consiste en un backend desarrollado en .NET 8 y un frontend desarrollado en Angular 17.

Tabla de Contenidos
Requisitos

Configuración del Backend (.NET)

Configuración del Frontend (Angular)

Ejecución del Proyecto

Estructura del Proyecto

Endpoints de la API

Stored Procedures

Contribución

Licencia

Requisitos
.NET 8 SDK: Descargar .NET 8

Node.js: Descargar Node.js

Angular CLI: Instalar con npm install -g @angular/cli

SQL Server: Asegúrate de tener SQL Server instalado y configurado.

Configuración del Backend (.NET)
Clonar el repositorio:
git clone https://github.com/tu-usuario/rigo-rigo-backend.git
cd rigo-rigo-backend

Configurar la cadena de conexión:

Abre el archivo appsettings.json.

Modifica la cadena de conexión para que apunte a tu instancia de SQL Server:
"ConnectionStrings": {
"DefaultConnection": "Server=TU_SERVIDOR;Database=RigoRigo;User Id=TU_USUARIO;Password=TU_CONTRASEÑA;TrustServerCertificate=True;"
}

Aplicar migraciones:

Ejecuta las migraciones para crear la base de datos:
dotnet ef migrations add InitialCreate
dotnet ef database update

Ejecutar el backend:
dotnet run

El backend estará disponible en https://localhost:5001.

Configuración del Frontend (Angular)
Clonar el repositorio:
git clone https://github.com/tu-usuario/rigo-rigo-frontend.git
cd rigo-rigo-frontend

Instalar dependencias:
npm install

Configurar la URL del backend:

Abre el archivo src/environments/environment.ts.

Modifica la URL del backend:
export const environment = {
production: false,
apiUrl: 'https://localhost:5001/api'
};

Ejecutar el frontend:
ng serve --open

El frontend estará disponible en http://localhost:4200.

Ejecución del Proyecto
Iniciar el backend:

Navega a la carpeta del backend y ejecuta:
dotnet run

Iniciar el frontend:

Navega a la carpeta del frontend y ejecuta:
ng serve --open

Acceder a la aplicación:

Abre tu navegador y visita http://localhost:4200.

Estructura del Proyecto
Backend (.NET):

RigoRigo.API: Contiene los controladores y la lógica de la API.

RigoRigo.Business: Contiene la lógica de negocio.

RigoRigo.Data: Contiene los repositorios y el acceso a la base de datos.

RigoRigo.Entities: Contiene los modelos de la base de datos.

Frontend (Angular):

src/app/components: Contiene los componentes de la aplicación.

src/app/services: Contiene los servicios para consumir la API.

src/app/models: Contiene los modelos de datos.

Endpoints de la API
GET /api/productos: Obtiene la lista de productos.

POST /api/pedidos/con-cliente: Crea un nuevo pedido con los datos del cliente.

Stored Procedures
CrearPedidoConCliente: Crea un pedido y un cliente si no existe, y guarda los detalles del pedido.