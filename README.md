# Carteira Digital

Utilizar Visual Code 
->
Abrir um projeto ou uma solução
->
CarteiraDigital.sln

Basta Rodar a Carteira Digital:

![alt text](image.png)

(Pode ser preciso instalar IIS Express)

# Adicionar Banco de Dados: 
``` 
CREATE DATABASE CarteiraDigital
```

# Adicionar Usuário Admin:
```
USE [CarteiraDigital]
GO

INSERT INTO [dbo].[Person]
           ([Id]
           ,[Name]
           ,[Email]
           ,[Salary]
           ,[AccountLimit]
           ,[MinimumValue]
           ,[Balance]
           ,[Username]
           ,[Password])
     VALUES
           (1
           ,'admin'
           ,'admin@example.com'
           ,5000.00
           ,10000.00
           ,100.00
           ,1500.00
           ,'admin'
           ,'123')
GO
```

# Sobre o Projeto:

Em um projeto .NET com NHibernate, os conceitos de Controllers, Models, Repositories e Views seguem um padrão comum de arquitetura de software conhecido como MVC (Model-View-Controller) ou uma variação dele. Em resumo, no contexto de um projeto .NET com NHibernate:

Models representam os dados e a lógica de negócios.

Repositories fornecem uma abstração sobre o acesso aos dados.

Controllers recebem solicitações do cliente e coordenam a lógica de negócios.

Views representam a interface do usuário e exibem os dados aos usuários.


# Models:
Os Models representam a estrutura de dados da sua aplicação. Eles são responsáveis por armazenar e gerenciar os dados, bem como definir as regras de negócio. Em um projeto .NET com NHibernate, os Models geralmente mapeiam diretamente para as entidades do banco de dados. Eles podem conter propriedades que correspondem aos campos da tabela do banco de dados e métodos que executam operações relacionadas a esses dados. Os Models encapsulam a lógica de negócios da aplicação e são independentes da interface do usuário.

# Repositories:
Os Repositories são responsáveis por isolar o acesso aos dados. Eles fornecem uma abstração sobre o mecanismo de persistência de dados (como NHibernate) e encapsulam as operações de consulta, inserção, atualização e exclusão de dados. Em um projeto .NET com NHibernate, os Repositories geralmente contêm métodos para recuperar, salvar, atualizar e excluir objetos do banco de dados. Eles ajudam a manter a separação de preocupações (conceito de Separation of Concerns), garantindo que a lógica de acesso aos dados não esteja diretamente acoplada aos Models ou aos Controllers.

# Controllers:
Os Controllers são responsáveis por receber as solicitações do cliente, processá-las e retornar uma resposta apropriada. Eles atuam como intermediários entre as Views e os Models. Em um projeto .NET, os Controllers são componentes que geralmente lidam com o roteamento de URLs, a validação de entrada, a chamada de métodos nos Repositories para acessar os dados e a preparação dos dados para serem enviados para as Views. Eles ajudam a manter a lógica de apresentação separada da lógica de negócios.

# Views:
As Views representam a interface do usuário da aplicação. Elas são responsáveis por exibir os dados aos usuários e capturar interações do usuário. Em um projeto .NET com NHibernate, as Views geralmente são páginas da web, interfaces de usuário desktop ou interfaces de usuário móvel que são renderizadas para os usuários. Elas podem conter marcação HTML, código C# (ou outra linguagem de backend) e referências aos objetos Model que contêm os dados a serem exibidos. As Views não devem conter lógica de negócios, mas podem conter lógica de apresentação para formatar e exibir os dados de forma adequada.
