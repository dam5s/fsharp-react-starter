export const HomePage = {
    selectTab: () => cy
        .get('nav a')
        .contains('Home')
        .click(),
    checkIsDisplayed: () => cy
        .get('h1')
        .should('have.text', 'Home')
}
