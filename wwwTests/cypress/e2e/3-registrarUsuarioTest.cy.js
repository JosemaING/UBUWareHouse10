// cypress/e2e/3-registroUsuarioTest.cy.ts

describe('Pruebas de Registro de Usuario', () => {

     // Variable para almacenar la contraseña generada
    let passGeneradaSegura;
    
    // Visitamos la página de inicio y hacemos clic en el botón de registro una vez al inicio
    beforeEach(() => {
        cy.visit('http://localhost:64447/inicio.aspx')
        cy.get('#btnRegistro').click()

        // Comprueba que el título de la página correcto, es decir redirige correctamente
        cy.title().should('eq', 'Registro de Usuario UBUWareHouse10')
    })

    it('Intenta registrar un usuario con un email ya registrado', () => {
        // Completa el formulario con un email ya registrado
        cy.get('#txtNombre').type('Ismael')
        cy.get('#txtApellidos').type('Manzanera López')
        cy.get('#txtEmail').type('iml1012@alu.ubu.es') // Email ya registrado
        cy.get('#txtPassword').type('PassTest1')
        cy.get('#btnRegistrarse').click()

        // Verifica que se muestra el mensaje de error
        cy.get('#lblError').should('be.visible').and('contain', 'El email ya está registrado. Intente con otro.')

        // Reset para evitar conflictos en siguientes tests
        // cy.reload()
    })

    it('Registra un usuario nuevo ingresando la contraseña manualmente', () => {
        // Completa el formulario con un email no registrado y contraseña manual
        cy.get('#txtNombre').type('Nuevo')
        cy.get('#txtApellidos').type('Usuario Registrado')
        cy.get('#txtEmail').type('nuevo.usuario@ubu.es')
        cy.get('#txtPassword').type('Password123')
        cy.get('#btnRegistrarse').click()

        // Verifica que se muestra el mensaje de éxito
        cy.get('#lblMensaje').should('be.visible').and('contain', 'Registro exitoso. Ahora puedes iniciar sesión.')

        // Reset para evitar conflictos en siguientes tests
        cy.reload()
    })

    it('Inicia sesión con el nuevo usuario y contraseña manual', () => {
        cy.get('#btnVolverInicio').click()
        cy.url().should('include', 'inicio.aspx')

        cy.get('#txtEmail').type('nuevo.usuario@ubu.es')
        cy.get('#txtPass').type('Password123')
        cy.get('#btnAceptar').click()

        // Comprueba el título de la página, es decir si redirige correctamente
        cy.title().should('eq', 'Gestión de Articulos')
        // Desconectamos el usuario nuevo
        cy.get('#btnDesconectar').click()
        cy.url().should('include', 'inicio.aspx')
    })

    it('Registra un usuario nuevo utilizando la opción de generar una contraseña segura', () => {
        // Completa el formulario con un email no registrado
        cy.get('#txtNombre').type('Seguro')
        cy.get('#txtApellidos').type('Usuario Registrado')
        cy.get('#txtEmail').type('seguro.usuario@ubu.es')

        // Genera una contraseña segura
        cy.get('#btnGenerarPassword').click()

        // Guarda la contraseña generada en una variable para usarla después
        cy.get('#txtPassword').invoke('val').then((passwordGenerada) => {
            cy.wrap(passwordGenerada).as('passwordGenerada')
            passGeneradaSegura = passwordGenerada; // Almacenamos en la variable global
            cy.get('#btnRegistrarse').click()

            // Verifica que el registro fue exitoso
            cy.get('#lblMensaje').should('be.visible').and('contain', 'Registro exitoso. Ahora puedes iniciar sesión.')
        })
    })

    it('Inicia sesión con la cuenta registrada con la contraseña generada', function () {
        cy.get('#btnVolverInicio').click()
        cy.url().should('include', 'inicio.aspx')

        // Usa el email del usuario registrado y la contraseña generada
        cy.get('#txtEmail').type('seguro.usuario@ubu.es')
        cy.get('#txtPass').type(passGeneradaSegura)

        // Intenta iniciar sesión
        cy.get('#btnAceptar').click()

        // Verifica que el usuario ha sido redirigido correctamente tras iniciar sesión
        cy.title().should('eq', 'Gestión de Articulos')
    })
})
