# Redbrow Backend Test API 🚀

Este proyecto consiste en una Web API RESTful diseñada para gestionar usuarios de manera paginada y permitir la creación de nuevos usuarios. Está implementada usando las mejores prácticas y patrones de diseño modernos en el desarrollo de APIs.

## Características 📋

- **Proyecto:** Web API REST
- **Base de Datos:** PostgreSQL
- **Tecnologías/Librerías Utilizadas:**
  - .NET 6 / C# 10
  - Entity Framework Core
  - Npgsql
  - AutoMapper
  - Docker
  - Azure

## Funcionalidad 🔍

La API implementa el patrón `GenericRepository` para las operaciones de base de datos, utiliza AutoMapper y DTOs para mapear los resultados, y expone dos endpoints principales:

1. `/api/user/pageNumber/pageSize` (GET): Obtener usuarios de forma paginada.
2. `/api/user` (POST): Crear un nuevo usuario.

## Enlaces Rápidos 🌐

- **Repositorio GitHub:** [HC809/RedbrowBETest](https://github.com/HC809/RedbrowBETest)
- **Swagger UI en Azure:** [Redbrow API Documentation](https://redbrow-be-test.azurewebsites.net/swagger/index.html)

## Despliegue con Docker 🐳

La imagen de Docker está disponible en Docker Hub para ser descargada y ejecutada localmente.

- **Imagen Docker Hub:** [caballero809/redbrow-be-test](https://hub.docker.com/r/caballero809/redbrow-be-test)
- **Comando para descargar la imagen:**
  ```sh
  docker pull caballero809/redbrow-be-test

O puedes descargar el proyecto y hacerlo de manera manual:
```sh
git clone https://github.com/HC809/RedbrowBETest.git
cd RedbrowBETest/RedbrowBackendTest
docker build -t tuusuario/redbrow-be-test .
docker run -d -p 8080:80 --name redbrow-backend-test tuusuario/redbrow-be-test

