# Calculator Service

Es un proyecto con una arquitectura en N-Capas, implementando como protocolo de comunicación HTTP (APIRest), capaz de realizar operaciones
matemáticas como suma, resta, multiplicación, división y raiz cuadrada.

Cada que se realiza una operación se guardará registro de la transacción para luego poderla consultar también mediante el servicio web
expuesto en la aplicación

## Requisitos

* Visual Studio 2019
* Net Framework 4.7
* C# 8.0
* Web Api 2
* Net Standar 2.0

## How to build application
1. La capa de datos es la encargada de contener la información y persistirla en un almacenador de datos, con fines de optimización, ésta labor se llevó a cabo con una lista estática para realizar las pruebas de una forma más ágil. Sin embargo se hubiera poder haber hecho uso de un motor de datos SQLServer
2. La capa de acceso a datos permite guardar la información de las operaciones y consultarlas en la capa de datos. En ésta capa se implementa un patrón de diseño llamado Repositorio y otro de Fábrica para realizar operaciones controladas a la capa de datos
3. La capa de lógica de negocios implementa las validaciones de negocio ideales para realizar operaciones, de ésta manera asegura que la información que se traslade a la capa de acceso a datos reciba información confiable y 100% verídica
4. La lógica de negocios es expuesta a través de un servicio web con el protocolo HTTP (REST) para ser consumida por cualquier tipo de aplicación cliente
5. La capa de contrato de servicio, expone aquellas funcionalidades que públicamente está visibles en el API, de tal manera que estandariza al servicio web y a la clase proxy que mapea los servicios
6. La aplicación cliente es aquella encargada de consumir los servicios expuestos en el API y de capturar la información digitada por el usuario

## How to deploy the application
Para hacer el correcto despliegue de la aplicación se debe:
1. 
