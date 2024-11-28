/// <reference types="cypress" />

const doc = "32132132178"

it('Testa adicionar dependente', () => {
    cy.get('.v-btn').contains('Adicionar Dependente').click();
    cy.get(`[data-cy="CPF"]:visible`).clear().click().type(doc);
    cy.get(`[data-cy="Nome"]:visible`).clear().click().type('teste');
    cy.get('[data-cy="dtNascimento"]:visible')
});
