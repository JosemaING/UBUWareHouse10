// cypress/e2e/4-componentesTest.cy.js

describe('Pruebas de Gestión de Articulos en el Panel de Componentes', () => {

    // Datos para los usuarios
    const usuarioGestor = {
        email: 'agb1111@alu.ubu.es',
        password: 'Estudiante3'
    };

    const usuarioGratuito = {
        email: 'prenedo@ubu.es',
        password: 'Profesor1'
    };

    it('Inicia sesión como usuario gestor y prueba la gestión de articulos', () => {
        // Inicio de sesión
        cy.visit('http://localhost:64447/inicio.aspx');
        cy.get('#txtEmail').type(usuarioGestor.email);
        cy.get('#txtPass').type(usuarioGestor.password);
        cy.get('#btnAceptar').click();

        // Verifica la redirección a la página de componentes
        cy.title().should('eq', 'Gestión de Articulos');

        // Crear cinco articulos distintas (mas de tres porque no es cuenta gratuita)
        for (let i = 1; i <= 5; i++) {
            cy.get('#txtNomElemento').type(`Articulo ${i}`);
            cy.get('#txtDesElemento').type(`Descripción del Articulo ${i}`);
            cy.get('#btnCrearArticulo').click();
            cy.get('#lblMensaje').should('be.visible').and('contain', 'Articulo creado con éxito.');
            cy.get('#txtNomElemento').clear();
            cy.get('#txtDesElemento').clear();
        }

        // Seleccionar y editar un articulo
        cy.get('#gvArticulos tr').eq(1).find('td').eq(4).find('a').click(); // Selecciona la primera articulo en la quinta columna (botón de selección)
        cy.get('#txtNomElemento').clear().type('Articulo Editado');
        cy.get('#txtDesElemento').clear().type('Descripción Editado');
        cy.get('#btnEditarArticulo').click();
        cy.get('#lblMensaje').should('be.visible').and('contain', 'Articulo editado con éxito.');

        // Seleccionar y eliminar un articulo
        cy.get('#gvArticulos tr').eq(1).find('td').eq(4).find('a').click(); // Selecciona la primera articulo en la quinta columna (botón de selección)
        cy.get('#btnEliminarArticulo').click();
        cy.get('#lblMensaje').should('be.visible').and('contain', 'Articulo eliminado con éxito.');
    });

    it('Accede al Panel de Control y permite desconectarse desde la vista de componentes', () => {
        // Inicio de sesión
        cy.visit('http://localhost:64447/inicio.aspx');
        cy.get('#txtEmail').type(usuarioGestor.email);
        cy.get('#txtPass').type(usuarioGestor.password);
        cy.get('#btnAceptar').click();

        // Acceder al panel de control
        cy.get('#btnPanelControl').click();
        cy.url().should('include', 'gestionUsuarios.aspx');

        // Desconectarse
        cy.visit('http://localhost:64447/componentes.aspx'); // Volver a componentes para verificar la desconexión
        cy.get('#btnDesconectar').click();
        cy.url().should('include', 'inicio.aspx');
    });

    it('Inicia sesión como usuario gratuito y verifica límites de gestión de articulos', () => {
        // Inicio de sesión
        cy.visit('http://localhost:64447/inicio.aspx');
        cy.get('#txtEmail').type(usuarioGratuito.email);
        cy.get('#txtPass').type(usuarioGratuito.password);
        cy.get('#btnAceptar').click();

        // Verifica que puede crear hasta tres articulos
        for (let i = 1; i <= 3; i++) {
            cy.get('#txtNomElemento').type(`Articulo ${i}`);
            cy.get('#txtDesElemento').type(`Descripción del Articulo ${i}`);
            cy.get('#btnCrearArticulo').click();
            cy.get('#lblMensaje').should('be.visible').and('contain', 'Articulo creado con éxito.');
            cy.get('#txtNomElemento').clear();
            cy.get('#txtDesElemento').clear();
        }

        // Intenta crear una cuarta articulo y verifica el mensaje de error
        cy.get('#txtNomElemento').type('Articulo 4');
        cy.get('#txtDesElemento').type('Descripción del Articulo 4');
        cy.get('#btnCrearArticulo').click();
        cy.get('#lblError').should('be.visible').and('contain', 'No puedes crear un articulo porque has llegado al límite.');

        // Seleccionar y editar un articulo
        cy.get('#gvArticulos tr').eq(1).find('td').eq(4).find('a').click(); // Selecciona la primera articulo en la quinta columna (botón de selección)
        cy.get('#txtNomElemento').clear().type('Articulo Editado');
        cy.get('#txtDesElemento').clear().type('Descripción Editado');
        cy.get('#btnEditarArticulo').click();
        cy.get('#lblMensaje').should('be.visible').and('contain', 'Articulo editado con éxito.');

        // Seleccionar y eliminar un articulo
        cy.get('#gvArticulos tr').eq(1).find('td').eq(4).find('a').click(); // Selecciona la primera articulo en la quinta columna (botón de selección)
        cy.get('#btnEliminarArticulo').click();
        cy.get('#lblMensaje').should('be.visible').and('contain', 'Articulo eliminado con éxito.');
    });
});
