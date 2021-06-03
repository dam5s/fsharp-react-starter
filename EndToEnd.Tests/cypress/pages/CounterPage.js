export const CounterPage = {
    selectTab: () => cy
        .get('nav a')
        .contains('Counter')
        .click(),
    checkIsDisplayed: () => cy
        .get('h1')
        .should('have.text', 'Counter'),
    checkCounterIsAt: (number) => cy
        .get('button')
        .contains(`Clicked ${number} times`)
        .should('be.visible'),
    increment: () => cy
        .get('button')
        .click()
}
