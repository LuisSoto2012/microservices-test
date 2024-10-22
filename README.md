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

2. Si estás detrás de un proxy, configura las variables de entorno para el proxy:

	```bash
	export http_proxy=http://proxy.empleador.com:8080
	export https_proxy=https://proxy.empleador.com:8080
	```
	
	Si tu proxy requiere autenticación, incluye el usuario y contraseña en las variables de entorno:
	
	```bash
	export http_proxy=http://username:password@proxy.empleador.com:8080
	export https_proxy=https://username:password@proxy.empleador.com:8080
	```

2. Construye las imágenes de Docker:

   ```bash
   docker-compose build
   ```

3. Levanta los servicios:

   ```bash
   docker-compose up
   ```

4. Importa los archivos JSON en POSTMAN para probar los endpoints.

## Endpoints

| Método | Endpoint                     | Descripción                       |
| ------ | ---------------------------- | --------------------------------- |
| POST   | /api/v1/clients              | Crear un nuevo cliente            |
| GET    | /api/v1/clients              | Obtener todos los clientes        |
| PUT    | /api/v1/clients              | Actualizar un cliente             |
| DELETE | /api/v1/clients?id={id}      | Eliminar un cliente               |
| POST   | /api/v1/accounts             | Crear una nueva cuenta            |
| GET    | /api/v1/accounts             | Obtener todas las cuentas         |
| PUT    | /api/v1/accounts             | Actualizar una cuenta             |
| DELETE | /api/v1/accounts?id={id}     | Eliminar una cuenta               |
| POST   | /api/v1/transactions         | Crear una nueva transacción       |
| GET    | /api/v1/transactions         | Obtener todas las transacciones   |
| PUT    | /api/v1/transactions         | Actualizar una transacción        |
| DELETE | /api/v1/transactions?id={id} | Eliminar una transacción          |
| GET    | /api/v1/client-transactions  | Obtener transacciones por cliente |

## Contenedores

| Contenedor                           | Estado  | Puerto |
| ------------------------------------ | ------- | ------ |
| microservices-test_portainer_1       | running | 9000   |
| microservices-test_rabbitmq_1        | running | 15672  |
| microservices-test_client_service_1  | running | 5002   |
| microservices-test_account_service_1 | running | 5001   |
| microservices-test_report_service_1  | running | 5003   |
| microservices-test_sql-server_1      | running | 1401   |

## Solución de Errores Comunes

### Error NU1301

**NU1301: Unable to load the service index for source https://api.nuget.org/v3/index.json**, generalmente ocurre cuando hay problemas de red o al acceder a la fuente de paquetes NuGet.

**Verifica la conexión a internet:**
Asegúrate de que el proceso de compilación de Docker tenga acceso a internet. Si estás trabajando detrás de un proxy o firewall, es posible que necesites configurar Docker para usar tu configuración de proxy.

### Configuración de proxy (opcional)

Si la red requiere acceso a través de un proxy, por favor configure las siguientes variables de entorno antes de ejecutar `docker-compose`:

```bash
export http_proxy=http://proxy.empleador.com:8080
export https_proxy=https://proxy.empleador.com:8080
docker-compose up
```

Si tu proxy requiere autenticación, incluye el usuario y contraseña en las variables de entorno:
	
```bash
export http_proxy=http://username:password@proxy.empleador.com:8080
export https_proxy=https://username:password@proxy.empleador.com:8080
```