
--------------------------------------------------------------------------------
AUTORES:
--------------------------------------------------------------------------------

Autores: José María Santos, Ismael Manzanera López

Emails: jsr1002@alu.ubu.es, iml1012@alu.ubu.es

Fecha: 19/10/2024

--------------------------------------------------------------------------------
DESCRIPCIÓN:
--------------------------------------------------------------------------------
Aplicación web multiusuario, que maneja la gestión de usuarios, de contraseñas,
gestión de artículos asociados a cada usuario y registro de nuevas cuentas.

Este proyecto implementa ASP.NET WebForms, C#.

Se proporcionan las instrucciones necesarias para la puesta en marcha, pruebas y ejecución del proyecto.
Es importante no modificar la estructura básica del código para garantizar el correcto funcionamiento del sistema.

--------------------------------------------------------------------------------
REQUISITOS:
--------------------------------------------------------------------------------

1. Visual Studio Community 2022 instalado.
2. .NET Framework 4.8 o superior.
3. Node.js v22.11.0 o última versión.
4. Cypress (ya está instalado en el propio proyecto).
5. Cuenta en GitHub para el control de versiones colaborativo.

--------------------------------------------------------------------------------
DESPLIEGUE Y EJECUCIÓN DE TESTS:
--------------------------------------------------------------------------------

Para iniciar la aplicación web UBUWareHouse10:

1. Clonar el repositorio desde GitHub.
   ```bash
   git clone https://github.com/JosemaING/UBUWareHouse10.git
   ```
2. Abrir la solución `.sln` en Visual Studio Community 2022.
3. (Opc) Compilar el proyecto si fuera necesario usando la opción **Build > Build Solution**.
4. Ejecutar el proyecto seleccionando IIS Express o el servidor web disponible.
5. Acceder al sitio web en `http://localhost:xxxx` (el puerto dependerá de tu configuración local).

Para realizar los tests con Cypress:

1. Acceder a la ruta `UBUWareHouse10/wwwTests` y ejecutar Cypress:
   ```bash
   npx cypress open
   ```
2. En la ventana de Cypress seleccionar pruebas `e2e`.
3. En la pestaña Specs, ejecutar los tests.
4. IMPORTARTE: Ejecutar los tests en orden para mantener la persistencia en la base de datos.

--------------------------------------------------------------------------------
RECOMENDACIONES PARA EL CORRECTO FUNCIONAMIENTO DE LA WEB:
--------------------------------------------------------------------------------

1. Los tests están configurados para realizar las pruebas a `http://localhost:64447`.
2. Si Visual Studio Community 2022 despliega la web en otro puerto habría que modificar los tests o el puerto en VS.
3. La aplicación se tiene que desplegar desde la pagina `inicio.aspx`.
4. Leer el fichero `README.txt` de `UBUWareHouse10/wwwTests`.
5. No modificar la estructura del código para el correcto despliegue.

--------------------------------------------------------------------------------
