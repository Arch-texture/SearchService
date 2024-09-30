# SearchService API

Este proyecto es una API REST desarrollada con .NET 8 que utiliza MongoDB como base de datos. Está diseñada para realizar operaciones de búsqueda optimizadas. La API está desplegada y accesible en la siguiente URL:

[https://searchservice-1.onrender.com](https://searchservice-1.onrender.com)

## Características

- **Framework**: .NET 8
- **Base de datos**: MongoDB
- **Despliegue**: Render
- **Servicio principal**: Búsqueda avanzada

## Requisitos

- **.NET SDK 8.0** o superior
- **MongoDB** (instancia local o en la nube)
- **Render** (para el despliegue en la nube)

## Instalación

1. Clona el repositorio:

    ```bash
    git clone https://github.com/tu-usuario/tu-repositorio.git
    ```

2. Accede al directorio del proyecto:

    ```bash
    cd tu-repositorio
    ```

3. Restaura las dependencias de NuGet:

    ```bash
    dotnet restore
    ```

4. Configura la conexión a MongoDB en el archivo `appsettings.json`:

    ```json
    {
      "ConnectionStrings": {
        "MongoDb": "mongodb://localhost:27017"
      },
      "DatabaseName": "SearchServiceDB"
      "Collections"...
    }
    ```

5. Ejecuta la aplicación localmente:

    ```bash
    dotnet run
    ```

6. La API estará disponible en `https://localhost:5001` o `http://localhost:5000` o en cualquier puerto que salga en la terminal.

## Despliegue en Render

La API está desplegada en [Render](https://render.com) y puedes accederla en:

[https://searchservice-1.onrender.com](https://searchservice-1.onrender.com)

## Endpoints

### SearchService

- `https://searchservice-1.onrender.com/api/Search/searchStudent/{keyboardEnter}`
- `https://searchservice-1.onrender.com/api/Search/searchByRestriction/{keyboardEnter}`
- `https://searchservice-1.onrender.com/api/Search/searchStudentByGrade/{min},{max}`

## Uso

Puedes utilizar herramientas como [Postman](https://www.postman.com/) o `curl` para interactuar con la API. Ejemplo de una solicitud GET:

