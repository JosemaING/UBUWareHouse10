- Requisitos previos: TENER NODE.JS INSTALADO

- Instalar cypress si no lo tienes instalado:

npm install cypress

- Ejecutar el siguiente comando en la terminal para inicar Cypress y sus tests a www:

npx cypress open

- Los usuarios registrados en la BBDD para relaizar los tests con distintas configuraciones son:

tblUsuarios[0] Usuario("Ismael", "Manzanera López", "iml1012@alu.ubu.es", "Estudiante1", false);
tblUsuarios[1] Usuario("Jose Maria", "Santos Romero", "jsr1002@alu.ubu.es", "Estudiante2", false);
tblUsuarios[2] =Usuario("Pedro", "Renedo Fernandez", "prenedo@ubu.es", "Profesor1", true);

tblUsuarios[0].EsGestor = false; // Ismael no es gestor
tblUsuarios[0].Activo = false; // Ismael está deshabilitado
tblUsuarios[0].IdUsuario = 0; // Ismael tiene id 0

tblUsuarios[1].EsGestor = true; // Jose Maria es gestor
tblUsuarios[1].CaducidadPassword = DateTime.Now; // Jose Maria tiene la contraseña caducada y tiene que cambiarsela
tblUsuarios[1].IdUsuario = 1; // Jose Maria tiene id 1

tblUsuarios[2].IdUsuario = 2; // Pedro está habilitado, tiene id 0 y es cuenta gratuita