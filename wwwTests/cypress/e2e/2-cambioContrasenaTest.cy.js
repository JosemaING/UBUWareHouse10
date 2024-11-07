// cypress/e2e/2-cambioContrasenaTest.cy.ts

describe('Pruebas de Cambio de Contraseña', () => {

    it('Muestra error cuando la contraseña actual es incorrecta', () => {
        // Visita la página de inicio de sesión y autentica con el usuario que tiene contraseña caducada
        cy.visit('http://localhost:64447/inicio.aspx')
        cy.get('#txtEmail').type('jsr1002@alu.ubu.es')
        cy.get('#txtPass').type('Estudiante2')
        cy.get('#btnAceptar').click()

        // Asegúrate de que redirige a la página de cambio de contraseña
        cy.url().should('include', 'cambioContrasena.aspx')

        // Muestra error cuando la contraseña actual es incorrecta
        cy.get('#txtContrasenaActual').type('ContraseñaIncorrecta')
        cy.get('#txtNuevaContrasena').type('NuevaContraseña123')
        cy.get('#btnGuardar').click()
        cy.get('#lblMensaje').should('be.visible').and('contain', 'La contraseña actual es incorrecta')
    })

    it('Impide usar una contraseña ya utilizada', () => {
        // Visita la página de inicio de sesión y autentica con el usuario que tiene contraseña caducada
        cy.visit('http://localhost:64447/inicio.aspx')
        cy.get('#txtEmail').type('jsr1002@alu.ubu.es')
        cy.get('#txtPass').type('Estudiante2')
        cy.get('#btnAceptar').click()

        // Asegúrate de que redirige a la página de cambio de contraseña
        cy.url().should('include', 'cambioContrasena.aspx')

        // Intenta usar una contraseña anterior ("Estudiante2") como nueva y muestra el mensaje adecuado
        cy.get('#txtContrasenaActual').type('Estudiante2')
        cy.get('#txtNuevaContrasena').type('Estudiante2')
        cy.get('#btnGuardar').click()
        cy.get('#lblMensaje').should('be.visible').and('contain', 'La nueva contraseña ya ha sido utilizada recientemente')
    })

    it('Muestra mensaje de contraseña débil al intentar cambiarla a "1234"', () => {
        // Visita la página de inicio de sesión y autentica con el usuario que tiene contraseña caducada
        cy.visit('http://localhost:64447/inicio.aspx')
        cy.get('#txtEmail').type('jsr1002@alu.ubu.es')
        cy.get('#txtPass').type('Estudiante2')
        cy.get('#btnAceptar').click()

        // Asegúrate de que redirige a la página de cambio de contraseña
        cy.url().should('include', 'cambioContrasena.aspx')

        // Cambia la contraseña a "1234" y verifica que es débil
        cy.get('#txtContrasenaActual').type('Estudiante2')
        cy.get('#txtNuevaContrasena').type('1234')
        cy.get('#btnGuardar').click()
        cy.get('#lblMensaje').should('be.visible').and('contain', 'Contraseña actualizada con éxito')
        cy.get('#lblSeguridad').should('be.visible').and('contain', 'Nivel de dificultad de la contraseña: Débil')
    })

    it('Permite iniciar sesión con la nueva contraseña y acceder al panel de control para volver a cambiar la contraseña', () => {
        // Visita la página de inicio de sesión y autentica con el usuario que tiene contraseña caducada
        cy.visit('http://localhost:64447/inicio.aspx')
        cy.get('#txtEmail').type('jsr1002@alu.ubu.es')
        cy.get('#txtPass').type('1234') // La contraseña actual ahora es "1234"
        cy.get('#btnAceptar').click()

        // Asegurarse de que iniciamos sesión con la nueva contrase
        cy.title().should('eq', 'Gestión de Articulos')
        cy.get('#btnPanelControl').click()
        cy.get('#btnCambiarContrasena').click()
        // Asegúrate de que redirige a la página de cambio de contraseña
        cy.url().should('include', 'cambioContrasena.aspx')
    })

    it('Permite cambiar la contraseña a una contraseña fuerte', () => {
        // Visita la página de inicio de sesión y autentica con el usuario que tiene contraseña caducada
        cy.visit('http://localhost:64447/inicio.aspx')
        cy.get('#txtEmail').type('jsr1002@alu.ubu.es')
        cy.get('#txtPass').type('1234') // La contraseña actual ahora es "1234"
        cy.get('#btnAceptar').click()
        // Asegurarse de que iniciamos sesión con la nueva contrase
        cy.title().should('eq', 'Gestión de Articulos')
        cy.get('#btnPanelControl').click()
        cy.get('#btnCambiarContrasena').click()
        // Asegurarse de que redirige a la página de cambio de contraseña
        cy.url().should('include', 'cambioContrasena.aspx')

        // Cambia la contraseña a una contraseña fuerte y verifica el éxito
        cy.get('#txtContrasenaActual').type('1234')
        cy.get('#txtNuevaContrasena').type('_Password1!?')
        cy.get('#btnGuardar').click()
        cy.get('#lblMensaje').should('be.visible').and('contain', 'Contraseña actualizada con éxito')
        cy.get('#lblSeguridad').should('be.visible').and('contain', 'Nivel de dificultad de la contraseña: Fuerte')
    })

    it('Permite iniciar sesión con la nueva contraseña después del cambio', () => {
        // Visita la página de inicio de sesión e intenta autenticar con la nueva contraseña fuerte
        cy.visit('http://localhost:64447/inicio.aspx')
        cy.get('#txtEmail').type('jsr1002@alu.ubu.es')
        cy.get('#txtPass').type('_Password1!?') // La contraseña actual ahora es "_Password1!?"
        cy.get('#btnAceptar').click()

        // Verifica que el usuario se redirige correctamente a la página principal del sistema
        cy.title().should('eq', 'Gestión de Articulos')
    })
})
