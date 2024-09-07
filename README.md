# PortalRentCar

**PortalRentCar** es una aplicación completa para la gestión y alquiler de vehículos, que incluye tanto un **Front-end** desarrollado con **C# Blazor** como un **Back-end** en **.NET Core**. Utiliza **SQL Server** como base de datos para almacenar la información de vehículos, clientes y alquileres.

## Tabla de Contenidos

1. [Descripción General](#descripción-general)
2. [Tecnologías Utilizadas](#tecnologías-utilizadas)
3. [Instalación](#instalación)
    - [Instalación del Back-end](#instalación-del-back-end)
    - [Instalación del Front-end](#instalación-del-front-end)
4. [Configuraciones Adicionales](#configuraciones-adicionales)
    - [Configuración del Servidor de Archivos](#configuración-del-servidor-de-archivos)
    - [Configuración del Correo SMTP](#configuración-del-correo-smtp)
5. [Uso](#uso)
6. [Estructura de Base de Datos](#estructura-de-base-de-datos)
7. [Licencia](#licencia)

## Descripción General

**PortalRentCar** es una plataforma que permite gestionar el alquiler de autos. Los usuarios pueden buscar vehículos disponibles, realizar reservas, y los administradores pueden gestionar el inventario de vehículos, clientes y alquileres.

## Tecnologías Utilizadas

El proyecto utiliza las siguientes tecnologías:

- **Front-end**: C# Blazor con .NET Core.
    - **Syncfusion.Blazor**: Para componentes avanzados en Blazor, como gráficos, tablas y formularios dinámicos.
    - **LeafletForBlazor**: Para integrar mapas interactivos y mostrar ubicaciones de vehículos en tiempo real.
- **Back-end**: .NET Core (C#) con Web API.
    - **FreeSpire.Office**: Para la generación y manipulación de documentos de Office como PDF, Word y Excel.
- **Base de Datos**: SQL Server.
- **ORM**: Entity Framework Core.
- **Autenticación**: JWT (JSON Web Tokens).
- **Estilos**: Bootstrap / CSS para el diseño responsivo.
- **Mapas**: Leaflet para mostrar la ubicación de los vehículos.

## Instalación

### Instalación del Back-end

1. Clona el repositorio:

    ```bash
    git clone https://github.com/raulhq85/PortalRentCar.git
    cd PortalRentCar/Backend
    ```

2. Configura la cadena de conexión a la base de datos SQL Server en `appsettings.json`:

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=tu-servidor;Database=PortalRentCarDB;User Id=usuario;Password=contraseña;"
      }
    }
    ```

3. Restaura las dependencias e inicia el servidor:

    ```bash
    dotnet restore
    dotnet run
    ```

4. Accede a la aplicación desde tu navegador en `http://localhost:7000`.

### Instalación del Front-end (C# Blazor)

1. Clona el repositorio:

    ```bash
    git clone https://github.com/raulhq85/PortalRentCar.git
    cd PortalRentCar/Frontend
    ```

2. Restaura las dependencias e inicia la aplicación:

    ```bash
    dotnet restore
    dotnet run
    ```

3. Accede a la aplicación desde tu navegador en `http://localhost:5000`.

## Configuraciones Adicionales

Para almacenar y gestionar imágenes u otros archivos del sistema, debes configurar el acceso a **Cloudinary** (u otro servicio similar). Agrega las siguientes claves de configuración en el archivo `appsettings.json`:

```json
{
    "CloudinaryConfiguration": {
        "CloudName": "dj7dinenm",
        "ApiKey": "TU_API_KEY",
        "ApiSecret": "TU_API_SECRET"
    }
}
```

Para el envío de correos electrónicos (notificaciones, confirmaciones de reserva, etc.), debes configurar los accesos del servidor SMTP en el archivo appsettings.json. El siguiente ejemplo usa Gmail como proveedor de correo:

```json
{
    "SmtpConfiguration": {
        "Servidor": "smtp.gmail.com",
        "Remitente": "App Rent Car",
        "Usuario": "graulex@gmail.com",
        "Password": "TU_CONTRASEÑA",
        "Puerto": 587,
        "UsarSsl": true
    }
}
```

## Uso

1. **Front-end**: Navega a `http://localhost:5000` para acceder a la interfaz de usuario de Blazor, donde podrás:
    - **Buscar vehículos**: Los usuarios pueden buscar vehículos disponibles según el tipo de vehículo, marca, ubicación y fechas.
    - **Reservar un vehículo**: Los usuarios registrados pueden reservar un vehículo por un período determinado.
    - **Ver ubicaciones**: Utilizando el mapa interactivo (integrado con **LeafletForBlazor**), se puede visualizar la ubicación de los vehículos en tiempo real.

2. **Back-end (API)**: La API se puede consultar desde `http://localhost:7000/api`, con las siguientes principales funcionalidades:
    - **Gestión de usuarios**: Registro, inicio de sesión y autenticación de clientes y administradores.
    - **Gestión de vehículos**: Operaciones CRUD (Crear, Leer, Actualizar, Eliminar).
    - **Gestión de Tipos de Vehiculos**: Operaciones CRUD (Crear, Leer, Actualizar, Eliminar).
    - **Gestión de Tipos de Marcas**: Operaciones CRUD (Crear, Leer, Actualizar, Eliminar).
    - **Alquiler de vehículos**: Endpoints para gestionar el proceso de alquiler, incluyendo la reserva de vehículos por los usuarios y el seguimiento de estos.
    - **Seguimiento en tiempo real**: Usando las tablas de **UbicacionVehiculo** para obtener la posición de los vehículos y mostrarlas en el mapa interactivo.
    
3. **Roles de usuario**:
    - **Clientes**: Pueden buscar vehículos, realizar reservas y ver sus reservas activas.
    - **Administradores**: Pueden gestionar el inventario de vehículos, gestionar usuarios, y ver el estado de los alquileres.

## Estructura de Base de Datos

La base de datos de **SQL Server** contiene las siguientes tablas:

- **TipoVehiculo**: Define los tipos de vehículos disponibles (sedán, SUV, etc.).
- **Cliente**: Almacena la información de los clientes que utilizan el servicio.
- **Vehiculo**: Contiene la información de los vehículos disponibles para alquiler.
- **Alquiler**: Registra las transacciones de alquiler de vehículos.
- **UbicacionVehiculo**: Registra la ubicación actual de cada vehículo, para su seguimiento en tiempo real.
- **Marca**: Almacena las marcas de los vehículos.


## Licencia

Este proyecto está bajo la licencia MIT. Al usar este proyecto, aceptas cumplir con los términos de la licencia.

### Personalización:

- **Autor**: Este proyecto ha sido desarrollado por **Raul H**. Puedes visitar el repositorio oficial en [GitHub](https://github.com/raulhq85/PortalRentCar).
- **Repositorios**: Se hace referencia al proyecto **PortalRentCar** en GitHub como base para contribuir y colaborar en el desarrollo continuo.
- **Instalación**: La instalación se detalla tanto para el **Back-end** como para el **Front-end** del sistema, incluyendo pasos para configurar la base de datos, acceso a servidores de archivos y correo.
- **Base de Datos**: El esquema de base de datos ha sido diseñado para cubrir la funcionalidad completa de un sistema de alquiler de autos, con tablas para vehículos, clientes, alquileres, ubicaciones de vehículos, entre otras.
- **Contribución**: Si deseas contribuir a este proyecto, sigue los pasos indicados en la sección de contribuciones y asegúrate de que tu código cumple con los estándares del proyecto.
