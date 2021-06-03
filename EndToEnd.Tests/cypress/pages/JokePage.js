export const JokePage = {
    selectTab: () => cy
        .get('nav a')
        .contains('Joke')
        .click(),
    checkIsDisplayed: () => cy
        .get('h1')
        .should('have.text', 'Joke'),
}
