// cypress/e2e/5-panelControlTest.cy.js

describe('Pruebas del Panel de Control', () => {

    // Datos para los usuarios
    const usuarioGestor = {
        email: 'agb1111@alu.ubu.es',
        password: 'Estudiante3'
    };

    const usuarioGratuito = {
        email: 'prenedo@ubu.es',
        password: 'Profesor1'
    };

    it('Acceso y gestión de usuarios (Editar, Crear y Eliminar) con cuenta de gestor', () => {
        // Inicio de sesión como gestor
        cy.visit('http://localhost:64447/inicio.aspx');
        cy.get('#txtEmail').type(usuarioGestor.email);
        cy.get('#txtPass').type(usuarioGestor.password);
        cy.get('#btnAceptar').click();

        // Acceso al panel de control
        cy.get('#btnPanelControl').click();
        cy.url().should('include', 'gestionUsuarios.aspx');
        cy.title().should('eq', 'Gestión de Usuarios');

        // Verifica que puede ver todos los usuarios registrados
        cy.get('#gvUsuarios').find('tr').its('length').should('be.gte', 2); // Debe haber más de un usuario visible

        // Crear un nuevo usuario
        cy.get('#txtNombre').type('UsuarioTest');
        cy.get('#txtApellidos').type('ApellidosTest');
        cy.get('#txtEmail').type('usuarioparaborrar@ubu.es');
        cy.get('#txtPassword').type('Test1234');
        cy.get('#chkCuentaGratuita').check(); // Selecciona cuenta gratuita para pruebas
        cy.get('#chkActivo').check(); // Cuenta activa
        cy.get('#btnCrearUsuario').click();

        // Verificar mensaje de éxito en la creación
        cy.get('#lblMensaje').should('be.visible').and('contain', 'Usuario creado exitosamente.');

        // Seleccionar y eliminar el usuario recién creado
        cy.get('#gvUsuarios tr').contains('usuarioparaborrar@ubu.es').parent().find('td').eq(8).find('a').click();
        cy.get('#btnEliminarUsuario').click();
        cy.get('#lblMensaje').should('be.visible').and('contain', 'Usuario eliminado con éxito.');
    });

    it('Cambiar contraseña desde el panel de control (Cualquier usuario)', () => {
        // Inicio de sesión como gestor
        cy.visit('http://localhost:64447/inicio.aspx');
        cy.get('#txtEmail').type(usuarioGestor.email);
        cy.get('#txtPass').type(usuarioGestor.password);
        cy.get('#btnAceptar').click();

        // Acceso al panel de control
        cy.get('#btnPanelControl').click();
        cy.url().should('include', 'gestionUsuarios.aspx');

        // Cambiar contraseña usando el botón de cambio de contraseña
        cy.get('#btnCambiarContrasena').click();
        cy.url().should('include', 'cambioContrasena.aspx');
    });

    it('Acceso y limitaciones de gestión (No Gestor, No Crear, No eliminar) con cuenta gratuita', () => {
        // Inicio de sesión como usuario gratuito
        cy.visit('http://localhost:64447/inicio.aspx');
        cy.get('#txtEmail').type(usuarioGratuito.email);
        cy.get('#txtPass').type(usuarioGratuito.password);
        cy.get('#btnAceptar').click();

        // Acceso al panel de control
        cy.get('#btnPanelControl').click();
        cy.url().should('include', 'gestionUsuarios.aspx');
        cy.title().should('eq', 'Gestión de Usuarios');

        // Verificar que solo puede ver su propio usuario
        cy.get('#gvUsuarios').find('tr').its('length').should('eq', 2); // Solo una fila de datos más la cabecera

        // Intentar crear un nuevo usuario y verificar que aparece un mensaje de error
        cy.get('#txtNombre').type('NoPermitido');
        cy.get('#txtApellidos').type('SinPermiso');
        cy.get('#txtEmail').type('sinpermiso@ubu.es');
        cy.get('#txtPassword').type('NoPermiso123');
        cy.get('#btnCrearUsuario').click();
        // Verificar mensaje de error de permisos
        cy.get('#lblError').should('be.visible').and('contain', 'No tienes permisos para crear usuarios.');

        // Intentar eliminar usuario
        cy.get('#btnEliminarUsuario').click();
        // Verificar mensaje de error de permisos
        cy.get('#lblError').should('be.visible').and('contain', 'No tienes permisos para eliminar usuarios.');

        // Seleccionar su propio usuario para edición
        cy.get('#gvUsuarios tr').eq(1).find('td').eq(8).find('a').click();

        // Editar nombre y verificar que no puede hacerse gestor
        cy.get('#txtNombre').clear().type('UsuarioEditado');
        cy.get('#chkEsGestor').should('not.be.checked').and('be.disabled'); // El usuario no puede hacerse gestor
        cy.get('#btnEditarUsuario').click();

        // Verificar mensaje de éxito en la edición
        cy.get('#lblMensaje').should('be.visible').and('contain', 'Usuario editado con éxito.');
    });
});
