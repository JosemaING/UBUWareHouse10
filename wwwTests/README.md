
# Instrucciones para la Configuración de Cypress en UBUWhareHouse10

## Requisitos Previos

- **Tener Node.js instalado** en tu sistema.

## Preparación de la Estructura de Archivos

1. **Descomprime** el archivo `node_modules.zip` en la ruta `/UBUWhareHouse10/wwwTests`.
2. La estructura de directorios debería quedar organizada de la siguiente manera:

   ```
   /UBUWhareHouse10
      ├──./wwwTests/
         ├── cypress/
         ├── node_modules/
         ├── cypress.config.js
         ├── package.json
         ├── package-lock.json
         └── README.md
   ```

## Instalación de Cypress

- Si aún no tienes Cypress instalado, puedes hacerlo con el siguiente comando:

   ```bash
   npm install cypress
   ```

> **Nota:** No es necesario instalar Cypress manualmente si ya descomprimiste `node_modules.zip` en el paso anterior.

## Ejecución de Cypress

- Abre la terminal y ejecuta el siguiente comando para iniciar Cypress y realizar pruebas en **www**:

   ```bash
   npx cypress open
   ```

## Usuarios para la Realización de Tests

En la base de datos (BBDD) existen usuarios configurados para realizar los tests con distintas configuraciones. A continuación, se detallan:

| Usuario        | Nombre Completo          | Email                   | Contraseña   | Es Gestor | Activo | ID  | Observaciones |
|----------------|--------------------------|-------------------------|--------------|-----------|--------|-----|---------------|
| tblUsuarios[0] | Ismael Manzanera López   | iml1012@alu.ubu.es      | Estudiante1  | No        | No     | 0   | Ismael no es gestor y está deshabilitado. |
| tblUsuarios[1] | Jose Maria Santos Romero | jsr1002@alu.ubu.es      | Estudiante2  | Sí        | Sí     | 1   | Jose Maria es gestor, tiene la contraseña caducada. |
| tblUsuarios[2] | Pedro Renedo Fernandez   | prenedo@ubu.es          | Profesor1    | No        | Sí     | 2   | Pedro tiene cuenta gratuita y está habilitado. |
| tblUsuarios[3] | Ana Gimenez Bernal       | agb1111@alu.ubu.es      | Estudiante3  | sí        | Sí     | 3   | Ana es gestora y está habilitada. |

### Detalles Adicionales de Configuración

- **tblUsuarios[0]** (Ismael)
  - `EsGestor = false` // Ismael no es gestor
  - `Activo = false` // Ismael está deshabilitado
  - `IdUsuario = 0` // Ismael tiene ID 0

- **tblUsuarios[1]** (Jose Maria)
  - `EsGestor = true` // Jose Maria es gestor
  - `CaducidadPassword = DateTime.Now` // La contraseña de Jose Maria ha caducado
  - `IdUsuario = 1` // Jose Maria tiene ID 1

- **tblUsuarios[2]** (Pedro)
  - `IdUsuario = 2` // Pedro está habilitado y tiene ID 2 con cuenta gratuita
