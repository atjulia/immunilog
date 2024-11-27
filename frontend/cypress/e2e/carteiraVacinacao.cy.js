/// <reference types="cypress" />


it('Testa visualização da carteirinha sem vacinas', () => {
    cy.get('[data-cy="carteiraVacinacao"]').click()
    cy.get('[data-cy="pessoa_0"]').click()
    cy.wait(3000);

    cy.get('.mt-2 > .v-btn').click().wait(3500);

    cy.get('.v-card-actions > .v-btn').click()
});

it('Testa visualização da carteirinha com vacinas', () => {
    cy.get('[data-cy="carteiraVacinacao"]').click()
    cy.get('[data-cy="pessoa_1"]').click()
    cy.wait(3000);
});