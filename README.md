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
2. La capa de acceso a datos 
