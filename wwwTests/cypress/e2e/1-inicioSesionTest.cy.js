// cypress/e2e/1-inicioSesionTest.cy.ts

describe('Pruebas de Inicio de Sesión', () => {
    beforeEach(() => {
        // Accede a la página de inicio de sesión
        cy.visit('http://localhost:64447/inicio.aspx')
    })

    it('Permite el inicio de sesión con credenciales correctas', () => {
        cy.get('#txtEmail').type('prenedo@ubu.es')
        cy.get('#txtPass').type('Profesor1')
        cy.get('#btnAceptar').click()

        // Comprueba que el título de la página sea "Gestión e Articulos", es decir redirige correctamente
        cy.title().should('eq', 'Gestión de Articulos')
    })

    it('Muestra un mensaje de error con credenciales incorrectas', () => {
        cy.get('#txtEmail').type('iml1012@alu.ubu.es')
        cy.get('#txtPass').type('ContraseñaIncorrecta')
        cy.get('#btnAceptar').click()

        // Verifica que el mensaje de error sea visible
        cy.get('#lblError').should('be.visible').and('contain', 'Usuario y/o contraseña incorrectos')
    })

    it('Redirige a cambio de contraseña si la contraseña está caducada', () => {
        cy.get('#txtEmail').type('jsr1002@alu.ubu.es')
        cy.get('#txtPass').type('Estudiante2')
        cy.get('#btnAceptar').click()

        // Comprueba la redirección a la página de cambio de contraseña
        cy.url().should('include', 'cambioContrasena.aspx')
    })

    it('Muestra error si el usuario está deshabilitado', () => {
        cy.get('#txtEmail').type('iml1012@alu.ubu.es') // Usuario deshabilitado en la base de datos
        cy.get('#txtPass').type('Estudiante1')
        cy.get('#btnAceptar').click()

        // Verifica que el mensaje de usuario deshabilitado sea visible
        cy.get('#lblError').should('be.visible').and('contain', 'Usuario deshabilitado por un gestor')
    })

    it('Redirige a la página de registro al hacer clic en el botón de registro', () => {
        cy.get('#btnRegistro').click()

        // Comprueba que el título de la página sea correcto, es decir redirige correctamente
        cy.title().should('eq', 'Registro de Usuario UBUWareHouse10')
    })
})
