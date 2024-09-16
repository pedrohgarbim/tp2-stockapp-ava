# 📦 tp2-stockapp

Para total visualização de tudo o que foi feito pela equipe, todos os meus commits e do meu grupo, seguir para: [Repositório no GitHub](https://github.com/joaoveasey/tp2-stockapp-ava)

## 📝 Visão Geral

O repositório `tp2-stockapp` é um projeto desenvolvido em C# utilizando .NET Core, seguindo os princípios da Clean Architecture. A aplicação serve para gerenciar o estoque de produtos de uma empresa, permitindo o cadastro, atualização, visualização e remoção de itens no estoque. A aplicação possui endpoints documentados utilizando Swagger, mas ainda não implementa autenticação JWT. A comunicação com o banco de dados é realizada via Azure SQL Server, utilizando Entity Framework.

## 🛠️ Tecnologias Utilizadas

- **Backend**:
  - **C#**: Linguagem de programação. 🧑‍💻
  - **.NET Core**: Framework para construção da aplicação. 🛠️
  - **Swagger**: Ferramenta para documentação de APIs. 📖
  - **Entity Framework**: ORM (Object-Relational Mapping) para interação com o banco de dados. 🗄️
  - **Azure SQL Server**: Serviço de banco de dados relacional na nuvem da Microsoft. ☁️

## 🏗️ Camadas da Arquitetura

A arquitetura da aplicação é organizada em diversas camadas, cada uma com responsabilidades específicas:

1. **Application** 📲
2. **Domain** 🌐
3. **Domain.Test** 🧪
4. **Infra.IoC** 🧩
5. **Infra.Data** 🗂️
6. **API** 🌍

## 📦 Detalhamento das Camadas

1. **Application** 📲:
   - **Função**: Esta camada contém a lógica de aplicação, incluindo os casos de uso e os serviços de aplicação.
   - **Componentes**:
     - **DTOs (Data Transfer Objects)**: Objetos utilizados para transferência de dados entre as camadas. 📤
     - **Mediators**: Implementação do padrão Mediator para tratar as requisições e respostas. 🗣️
     - **Services**: Serviços específicos da aplicação que orquestram as operações de negócio. 🎯

2. **Domain** 🌐:
   - **Função**: Esta camada representa o núcleo da aplicação, contendo as entidades de domínio e a lógica de negócio.
   - **Componentes**:
     - **Entities**: Entidades de domínio que representam os objetos de negócio. 🧱
     - **Interfaces**: Contratos que definem os serviços e repositórios utilizados pela aplicação. 🤝
     - **Specifications**: Regras de negócio e validações específicas do domínio. 📜

3. **Domain.Test** 🧪:
   - **Função**: Esta camada contém os testes unitários para a lógica de negócio na camada de domínio.
   - **Componentes**:
     - **Unit Tests**: Testes que validam o comportamento das entidades de domínio e das regras de negócio. ✔️

4. **Infra.IoC** 🧩:
   - **Função**: Esta camada é responsável pela configuração da Injeção de Dependências (IoC - Inversion of Control).
   - **Componentes**:
     - **Dependency Injection Configurations**: Configurações para registrar e resolver dependências entre as camadas. ⚙️

5. **Infra.Data** 🗂️:
   - **Função**: Esta camada contém a implementação dos repositórios e a configuração do banco de dados.
   - **Componentes**:
     - **Repositories**: Implementações dos repositórios utilizando Entity Framework para interagir com o Azure SQL Server. 🛢️
     - **Migrations**: Configurações e migrações do banco de dados para gerenciar a estrutura de dados. 🛠️

6. **API** 🌍:
   - **Função**: Esta camada expõe os endpoints da API para interação com a aplicação.
   - **Componentes**:
     - **Controllers**: Controladores que definem os endpoints da API utilizando ASP.NET Core. 🚦
     - **Swagger Configuration**: Configurações para a documentação da API com Swagger. 📑
     - **Middlewares**: Incluem configurações para tratamento de erros, log e outras funcionalidades transversais. 🔄

## 🗂️ Diagrama de Arquitetura

```plaintext
                          +---------------------------------+
                          |              API               |
                          |        (Controllers, Swagger)  |
                          +---------------+-----------------+
                                          |
                                          v
                          +---------------+-----------------+
                          |            Application          |
                          |         (DTOs, Mediators)       |
                          +---------------+-----------------+
                                          |
                                          v
                          +---------------+-----------------+
                          |               Domain            |
                          |  (Entities, Interfaces, Specs)  |
                          +---------------+-----------------+
                                          |
                                          v
                          +---------------+-----------------+
                          |          Infra.IoC              |
                          |  (Dependency Injection Config)  |
                          +---------------+-----------------+
                                          |
                                          v
                          +---------------+-----------------+
                          |           Infra.Data            |
                          |  (Repositories, Migrations)     |
                          +---------------+-----------------+
                                          |
                                          v
                          +---------------+-----------------+
                          |         Azure SQL Server        |
                          |      (Entity Framework)         |
                          +---------------------------------+
```

Neste diagrama:

- **API** 🌍: Contém os controllers que definem os endpoints da API e a configuração do Swagger para documentação.
- **Application** 📲: Contém a lógica de aplicação, incluindo os DTOs e os mediators que tratam as requisições.
- **Domain** 🌐: Contém as entidades de domínio, interfaces e especificações que implementam a lógica de negócio.
- **Infra.IoC** 🧩: Contém as configurações para injeção de dependências.
- **Infra.Data** 🗂️: Contém as implementações dos repositórios e a configuração do Entity Framework para interação com o Azure SQL Server.
- **Azure SQL Server** ☁️: Banco de dados relacional utilizado para armazenar os dados da aplicação.

## 🚀 Melhorias Aplicadas

1. **Documentação e Comentários XML na API** 📚:
   - **Tarefa 46**: Adiciona comentários XML nos controladores e métodos da API, melhorando a documentação automática com Swagger e a compreensão do código.

2. **Configuração de CORS** 🌐:
   - **Tarefa 22**: Configura CORS (Cross-Origin Resource Sharing) para permitir requisições de diferentes origens, facilitando a integração com frontend e outras APIs.

3. **Paginação** 📄:
   - **Tarefa 23**: Implementa paginação nos endpoints de leitura para melhorar a performance e usabilidade ao lidar com grandes volumes de dados.

4. **Logging** 📝:
   - **Tarefa 24**: Adiciona logging usando Serilog para registrar requisições e erros, facilitando a depuração e manutenção do sistema.

5. **Caching** 💾:
   - **Tarefa 25**: Implementa caching usando Redis para melhorar a performance das leituras frequentes.

6. **Relatórios de Estoque Baixo** 📊:
   - **Tarefa 26**: Adiciona um endpoint para gerar relatórios de produtos com estoque baixo, ajudando na gestão do estoque.

7. **Atualização em Massa** 🔄:
   - **Tarefa 27**: Implementa um endpoint de atualização em massa para facilitar a administração de múltiplos produtos.

8. **Exportação de Relatórios** 📁:
   - **Tarefa 28**: Adiciona suporte à exportação de relatórios em formato CSV, permitindo a análise offline e integração com outras ferramentas.

9. **Autenticação Multi-Fator (MFA)** 🔒:
   - **Tarefa 29**: Implementa autenticação multi-fator para aumentar a segurança do sistema.

10. **Reposição Automática de Estoque** 📦:
    - **Tarefa 30**: Adiciona funcionalidade para repor automaticamente o estoque de produtos com baixo estoque, garantindo disponibilidade contínua.

11. **Upload de Imagem de Produtos** 🖼️:
    - **Tarefa 31**: Implementa a funcionalidade de upload de imagem para os produtos, melhorando a apresentação visual no frontend.

12. **Notificações em Tempo Real** 📡:
    - **Tarefa 32**: Implementa notificações em tempo real usando SignalR para alterações no estoque, proporcionando atualizações instantâneas aos usuários.

13. **Auditoria de Mudanças no Estoque** 📋:
    - **Tarefa 33**: Adiciona auditoria de mudanças no estoque para registrar e monitorar alterações, melhorando a rastreabilidade.

14. **Sistema de Permissões Granulares** 🛡️:
    - **Tarefa 34**: Implementa um sistema de permissões granulares para controlar o acesso a diferentes funcionalidades com base em roles.

15. **Integração com API Externa para Cotação de Preços** 🔗:
    - **Tarefa 35**: Adiciona integração com uma API externa para obter cotações de preços dos produtos, auxiliando na precificação dinâmica.

16. **Filtragem Avançada nos Relatórios** 🎛️:
    - **Tarefa 36**: Implementa funcionalidade de filtragem avançada nos relatórios de produtos, facilitando a análise e a tomada de decisões.

17. **Backup Automático do Banco de Dados** 💽:
    - **Tarefa 37**: Adiciona funcionalidade de backup automático do banco de dados para garantir a segurança e a recuperação de dados.

18. **Importação de Dados em Massa** 📥:
    - **Tarefa 38**: Implementa a funcionalidade para importar dados de produtos em massa a partir de um arquivo CSV, agilizando a inserção de grandes volumes de dados.

19. **Notificações por Email** ✉️:
    - **Tarefa 39**: Adiciona sistema de notificações por email para alertar sobre eventos importantes como baixo estoque.

20. **Relatórios Gráficos** 📈:
    - **Tarefa 40**: Implementa funcionalidade de geração de relatórios gráficos de estoque e vendas, facilitando a visualização de dados e tendências.

21. **Avaliação de Produtos pelos Clientes** ⭐:
    - **Tarefa 41**: Adiciona funcionalidade para que os clientes possam avaliar os produtos, proporcionando feedback valioso.

22. **Busca Avançada** 🔍:
    - **Tarefa 42**: Implementa busca avançada com suporte a filtros e ordenação, melhorando a experiência do usuário na busca de produtos.

23. **Recomendação de Produtos** 🎯:
    - **Tarefa 43**: Adiciona sistema de recomendação de produtos baseado no histórico de compras dos clientes, incentivando vendas cruzadas.

24. **Carrinho de Compras** 🛒:
    - **Tarefa 44**: Implementa funcionalidade de carrinho de compras para os clientes, facilitando a gestão de pedidos.

25. **Checkout e Processamento de Pagamentos** 💳:
    - **Tarefa 45**: Adiciona funcionalidade de checkout e integração com um serviço de processamento de pagamentos, completando o ciclo de vendas.

26. **Recuperação de Senha** 🔑:
    - **Tarefa 51**: Adiciona uma funcionalidade para recuperação de senha via email, aumentando a segurança e usabilidade do sistema.

27. **Autenticação com Redes Sociais** 🌐:
    - **Tarefa 52**: Implementa autenticação usando provedores de redes sociais como Google e Facebook, facilitando o login para os usuários.

28. **Testes de Carga** 🏋️‍♂️:
    - **Tarefa 53**: Configura e executa testes de carga para avaliar o desempenho da API sob condições de alta carga.

29. **Webhook** 🪝:
    - **Tarefa 54**: Implementa uma funcionalidade de webhook para notificar sistemas externos sobre eventos importantes.

30. **Suporte a GraphQL** 🔗:
    - **Tarefa 55**: Adiciona suporte a GraphQL para consultas mais flexíveis e eficientes.

31. **Análise de Sentimento** 😊😞:
    - **Tarefa 56**: Adiciona uma funcionalidade para analisar o sentimento das avaliações dos produtos.

32. **Integração com Serviço de Mensagens (SMS)** 📱:
    - **Tarefa 57**: Adiciona suporte para envio de mensagens SMS para notificações críticas.

33. **Pesquisa de Texto Completo** 🔍:
    - **Tarefa 58**: Implementa uma funcionalidade de pesquisa de texto completo para melhorar a busca de produtos.

34. **Sistema de Backup Incremental** 💽:
    - **Tarefa 59**: Adiciona um sistema de backup incremental para reduzir o tempo e os recursos necessários para backups.

35. **Agendamento de Tarefas** ⏰:
    - **Tarefa 60**: Implementa uma funcionalidade para agendamento de tarefas recorrentes.

36. **Integração com ERP Externo** 🔄:
    - **Tarefa 61**: Adiciona integração com um sistema ERP externo para sincronizar dados de produtos e estoque.

37. **Controle de Acesso Baseado em Claims** 🔐:
    - **Tarefa 62**: Adiciona um sistema de controle de acesso baseado em claims para gerenciar permissões detalhadas.

38. **Comparação de Produtos** ⚖️:
    - **Tarefa 63**: Implementa uma funcionalidade para permitir que os usuários comparem diferentes produtos.

39. **Cache Distribuído com Redis** 💾:
    - **Tarefa 64**: Adiciona suporte para cache distribuído usando Redis, melhorando a escalabilidade.

40. **Recomendação Personalizada com Machine Learning** 🤖:
    - **Tarefa 65**: Adiciona uma funcionalidade de recomendação personalizada usando modelos de machine learning.

41. **Monitoramento e Alertas com Prometheus e Grafana** 📈:
    - **Tarefa 66**: Configura monitoramento e alertas usando Prometheus e Grafana para monitorar a saúde do sistema.

42. **Sistema de Workflow** 📋:
    - **Tarefa 67**: Adiciona um sistema de workflow para gerenciar processos de negócios complexos.

43. **Análise Preditiva de Vendas** 📊:
    - **Tarefa 68**: Adiciona uma funcionalidade para análise preditiva de vendas usando algoritmos de machine learning.

44. **Gestão de Inventário Just-in-Time** 📦:
    - **Tarefa 69**: Adiciona uma funcionalidade para gestão de inventário just-in-time, otimizando o estoque com base na demanda.

45. **Sistema de Feedback com Análise de Sentimento** 📝😊😞:
    - **Tarefa 70**: Adiciona um sistema de feedback para clientes com análise de sentimento para avaliar a satisfação.

46. **Nova Classe e Repositório de Fornecedores** 📦🗂️:
   - **Tarefas 71-74**: Introdução da classe `Supplier`, interface de repositório, implementação do repositório e criação do controlador `SuppliersController`. Essas tarefas ajudam na gestão de fornecedores.

47. **Procedures e Endpoints em MySQL** 🛠️:
   - **Tarefas 75-76, 80-81, 85-86, 95-96**: Criação de procedures para relatórios de vendas, estoque, compras e lucros, além de endpoints para chamar essas procedures. Isso facilita a geração de relatórios e análise de dados.

48. **Triggers em MySQL** 🔄:
   - **Tarefa 77**: Criação de triggers para atualização automática de estoque, melhorando a integridade e a automação dos dados.

49. **Dashboards de Vendas e Estoque** 📊📈:
   - **Tarefas 78, 83, 93**: Implementação de endpoints que retornam dados para dashboards de vendas, estoque e compras, agregando informações relevantes para a visualização e análise.

50. **Funcionalidade de Busca Avançada** 🔎:
   - **Tarefas 79, 84**: Adição de funcionalidades de busca avançada para fornecedores e produtos, incluindo filtros.

51. **Notificações e Alertas** 🔔:
   - **Tarefas 82, 94**: Implementação de sistemas de notificações push e alertas personalizados, melhorando a comunicação com os usuários.

52. **Relatórios de Impostos e Sistema de Cálculo de Impostos** 💰:
   - **Tarefas 88-89**: Adição de um sistema de cálculo de impostos e um endpoint para gerar relatórios de impostos, auxiliando na conformidade fiscal.

53. **Sistema de Descontos e Promoções** 🏷️:
   - **Tarefa 90**: Implementação de um sistema para aplicar descontos e promoções, incentivando as vendas.

54. **Integração com Serviços Externos** 🌐🔗:
   - **Tarefas 91, 100, 111**: Integração com serviços de entrega, CRM e pagamentos externos, expandindo a funcionalidade do sistema.

55. **Moderação de Reviews** 📝:
    - **Tarefa 92**: Implementação de um sistema para moderação de reviews dos produtos pelos clientes, mantendo a qualidade dos comentários.

56. **Feedback via SMS e Sistema de Feedback Anônimo** 📱📝:
    - **Tarefas 97, 101**: Adição de sistemas para coleta de feedback via SMS e feedback anônimo, proporcionando mais canais de comunicação com os clientes.

57. **Análise de Tendências de Mercado** 📊📈:
    - **Tarefa 98**: Implementação de um sistema para análise de tendências de mercado, auxiliando na tomada de decisões estratégicas.

58. **Sistema de Devolução de Produtos** ↩️:
    - **Tarefa 99**: Adição de um sistema para gerenciar a devolução de produtos, melhorando a experiência do cliente.

59. **Métricas de Performance e Monitoramento de Qualidade** 📈📋:
    - **Tarefas 102, 122**: Implementação de sistemas para medir e monitorar a performance do sistema e a qualidade dos produtos.

60. **Backup em Nuvem e Gestão de Recursos Financeiros** ☁️💰:
    - **Tarefas 103, 123**: Adição de um sistema para backup em nuvem e gerenciamento de recursos financeiros, garantindo a segurança dos dados e a saúde financeira.

61. **Automação de Processos e Planejamento de Produção** 🤖📅:
    - **Tarefas 121, 120**: Implementação de sistemas para automação de processos empresariais e planejamento de produção, aumentando a eficiência operacional.

62. **Rastreamento de Entregas e Gestão de Inventário em Tempo Real** 🚚🕒:
    - **Tarefas 112, 107**: Adição de sistemas para rastrear as entregas dos pedidos dos clientes e gestão de inventário em tempo real, proporcionando atualizações instantâneas das quantidades de produtos.

63. **Relatórios Personalizados e Gestão de Relacionamento com Clientes** 📊👥:
    - **Tarefas 108, 116**: Implementação de sistemas para criação e visualização de relatórios personalizados e gerenciamento do relacionamento com clientes.

64. **Análise de Competitividade e Gestão de Contratos** 📈📜:
    - **Tarefas 124, 117**: Adição de sistemas para análise de competitividade e gestão de contratos com fornecedores e clientes.

65. **Avaliação de Desempenho de Funcionários e Viabilidade de Projetos** 👷‍♂️📊:
    - **Tarefas 118, 119**: Implementação de sistemas para avaliação de desempenho dos funcionários e análise de viabilidade de novos projetos.
