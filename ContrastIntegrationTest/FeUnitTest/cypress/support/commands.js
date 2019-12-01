// ***********************************************
// This example commands.js shows you how to
// create various custom commands and overwrite
// existing commands.
//
// For more comprehensive examples of custom
// commands please read more here:
// https://on.cypress.io/custom-commands
// ***********************************************

Cypress.Commands.add("login", (username, password) =>{
    cy.visit('/static/ng/index.html#/pages/signin');
    cy.get("input[name='username']").type(username)
    cy.get("button").click();
    cy.get("input[name='password']").type(password)
    cy.get("button").click();
})
