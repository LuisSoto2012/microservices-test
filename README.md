# Microservices Architecture: Clients and Accounts/Transactions

Este repositorio contiene una implementación de microservicios para la gestión de Clientes y Cuentas/Transacciones, junto con un microservicio adicional para la generación de reportes. La arquitectura se basa en buenas prácticas de desarrollo y comunicación entre microservicios.

## Consideraciones Generales

- **Nomenclatura y Estructura de Datos:** Siguiendo las buenas prácticas, el código y la nomenclatura de los endpoints están en inglés.
- **Base de Datos:** Se ha utilizado SQL Server como base de datos. En lugar de generar scripts para la base de datos, se implementó el enfoque **Code First** de Entity Framework Core con migraciones y seeders.
- **Comunicación Asíncrona:** Para la comunicación asíncrona entre microservicios, se utilizó RabbitMQ como EventBus. Por ejemplo, al eliminar un cliente, también se eliminan sus cuentas asociadas.
- **Generación de Reportes:** Se creó un microservicio adicional para la generación de reportes, respetando el principio de **responsabilidad única por microservicio** e independencia. Se utilizó el enfoque **Event Push Model with Dedicated Database**, lo que permite mantener la **consistencia eventual de los datos** sin perturbar los microservicios principales. Cada vez que se registra un cliente o una transacción, se genera un evento enviado por el EventBus que el servicio de reportes consume y agrega a su base de datos. Para simplificar el proyecto, se utilizó una base de datos **in-memory** para los reportes. Este enfoque es ideal porque permite desacoplar la lógica del negocio y mejorar el rendimiento de los servicios principales.
- **Pruebas Unitarias y de Integración:** Se han agregado pruebas unitarias y de integración para verificar la correcta funcionalidad del reporte.
- **Dockerización:** Todos los servicios, así como la base de datos SQL Server y RabbitMQ, están dockerizados para facilitar su despliegue y gestión.
- **Archivos POSTMAN:** Se incluyen archivos JSON de POSTMAN con los endpoints para probar los microservicios.

## Requisitos

- [.NET 7.0 o superior](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started)
- [RabbitMQ](https://www.rabbitmq.com/download.html)

## Instalación

1. Clona el repositorio:

   ```bash
   git clone https://github.com/LuisSoto2012/microservices-test.git
   cd microservices-test
   ```

2. Construye las imágenes de Docker:
   docker-compose build

3. Levanta los servicios:
   docker-compose up

4. Importa los archivos JSON en POSTMAN para probar los endpoints.
   Attempt | #1 | #2 | #3 | #4 | #5 | #6 | #7 | #8 | #9 | #10 | #11
   --- | --- | --- | --- |--- |--- |--- |--- |--- |--- |--- |---
   Seconds | 301 | 283 | 290 | 286 | 289 | 285 | 287 | 287 | 272 | 276 | 269
