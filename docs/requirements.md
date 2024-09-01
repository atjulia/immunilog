
# Requisitos

## Requisitos Funcionais
**Cadastro de usuário**: O usuário deve poder criar uma conta pessoal com suas informações básicas, como nome, e-mail e senha.
**Login do usuário**: Usuários já registrados previamente devem conseguir acessar a aplicação usando suas credenciais de acesso (e-mail e senha).
**Registrar Vacina**: O usuário deve conseguir registrar as vacinas que já foram tomadas.
**Validação de vacina registrada**: Ao cadastrar uma vacina, o sistema deve receber algum tipo de comprovação que a mesma foi realmente realizada, a fim de aprovar ou recusar o registro.
**Adicionar dependentes**: O usuário deve conseguir adicionar dependentes a sua conta, limitando a 5 dependentes por pessoa.
**Consultar calendário vacinal PNI**: O usuário deve conseguir consultar o calendário básico vacinal PNI (SUS).
**Consultar calendário vacinal Sociedade Brasileira de Imunizações**: O usuário deve conseguir consultar o calendário da Sociedade Brasileira de Imunizações.
**Notificar vacinas**: O usuário deve receber uma notificação por e-mail quando a data da sua próxima vacina estiver chegando.
**Integração com API própria**: A aplicação deve integrar com uma API dedicada para base de dados das vacinas disponíveis conforme calendário vacinal.

## Requisitos Não-Funcionais
**Segurança de Dados**: Garantir que todas as informações dos usuários, incluindo dados pessoais e registros de vacinação, sejam armazenadas de forma segura.
**Desempenho**: O aplicativo deve ser responsivo e ter tempos de carregamento rápidos.
**Usabilidade e Interface do Usuário**: A interface do usuário deve ser intuitiva e fácil de usar, com design responsivo e navegação simplificada, para garantir que os usuários possam facilmente acessar e inserir informações.
**Manutenibilidade**: O código-fonte do aplicativo deve ser bem estruturado e documentado, facilitando a manutenção e futuras atualizações do sistema.
**Testabilidade**: O sistema deve ser facilmente testável, com a capacidade de realizar testes de unidade, integração e aceitação para garantir a qualidade do software e a precisão dos dados registrados pelos usuários.