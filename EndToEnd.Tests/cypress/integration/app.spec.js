/// <reference types="cypress" />
import {HomePage} from "../pages/HomePage";
import {CounterPage} from "../pages/CounterPage";
import {JokePage} from "../pages/JokePage";

describe('App', () => {

    beforeEach(() => {
        cy.visit('http://localhost:3000');
    });

    it('navigates between tabs', () => {
        HomePage.checkIsDisplayed();

        CounterPage.selectTab();
        CounterPage.checkIsDisplayed();

        JokePage.selectTab();
        JokePage.checkIsDisplayed();

        HomePage.selectTab();
        HomePage.checkIsDisplayed();
    });

    it('maintains state of the counter', () => {
        CounterPage.selectTab();
        CounterPage.checkIsDisplayed();

        CounterPage.checkCounterIsAt(0);
        CounterPage.increment();
        CounterPage.checkCounterIsAt(1);
        CounterPage.increment();
        CounterPage.checkCounterIsAt(2);

        HomePage.selectTab();
        HomePage.checkIsDisplayed();

        CounterPage.selectTab();
        CounterPage.checkIsDisplayed();
        CounterPage.checkCounterIsAt(2);
    });
});
