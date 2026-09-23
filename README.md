# Real-Time-Fraud-Engine-.NET-Core-[readme.md](https://github.com/user-attachments/files/32578036/readme.md)
# 🛡️ Real-Time Fraud Engine (.NET Core)

> Um motor de prevenção a fraudes transacionais de alta performance, projetado com padrões corporativos de arquitetura de software, processamento assíncrono e mensageria.

## 📌 Sobre o Projeto

Este projeto foi concebido para simular um cenário real de engenharia de software no setor financeiro. O sistema intercepta transações financeiras em tempo real, avalia múltiplos fatores de risco através de um motor de regras modular e utiliza mensageria assíncrona para garantir escalabilidade e resiliência sem impactar a experiência do usuário.

## 🎯 Objetivos do Projeto

### **Objetivo Geral**

Desenvolver um motor de prevenção a fraudes transacionais de alta performance em .NET, aplicando princípios de *Clean Architecture* e o padrão *Chain of Responsibility* para avaliar, em tempo real, o risco de operações financeiras e mitigar perdas por atividades suspeitas.

### **Objetivos Específicos**

1. **Modelagem de Domínio e Arquitetura:** Implementar uma separação estricta de responsabilidades utilizando *Clean Architecture* e o padrão CQRS com *MediatR*.

2. **Motor de Regras Flexível:** Desenvolver um sistema baseado no padrão *Chain of Responsibility* para permitir a inclusão modular de regras de negócio independentes (geolocalização, velocidade transacional, dispositivos, etc.).

3. **Confiabilidade e Resiliência Assíncrona:** Integrar um broker de mensagens (**RabbitMQ** via **MassTransit**) para desacoplar a ingestão da transação do processamento pesado de risco.

4. **Persistência de Dados e Auditoria:** Configurar o Entity Framework Core com PostgreSQL para registrar transações, regras avaliadas e o veredito final de forma imutável.

5. **Qualidade via Testes Automatizados:** Escrever testes unitários rigorosos (utilizando *xUnit*) focados nas regras de negócio e no cálculo do *Score* de risco.

6. **Containerização Local (DevOps):** Orquestrar o ambiente de desenvolvimento completo utilizando *Docker Compose*.

## 🛠️ Stack Tecnológica

| Camada / Função | Tecnologia | 
 | ----- | ----- | 
| **Linguagem & Framework** | C# / .NET (ASP.NET Core Minimal APIs) | 
| **Arquitetura** | Clean Architecture, CQRS (MediatR), Chain of Responsibility | 
| **Banco de Dados** | PostgreSQL + Entity Framework Core | 
| **Mensageria** | RabbitMQ + MassTransit (Publish/Subscribe) | 
| **Resiliência & Tolerância** | Polly (Retry & Circuit Breaker) | 
| **Testes Automatizados** | xUnit, NSubstitute, Testcontainers | 
| **Infraestrutura Local** | Docker & Docker Compose | 

## 🏗️ Arquitetura do Sistema

A solution é dividida em quatro projetos principais para garantir o desacoplamento e a manutenibilidade:

```
📁 FraudEngine.sln
 ├── 📁 src/
 │   ├── 📁 FraudEngine.Domain        # Entidades, Value Objects e Contratos (Regras puras de negócio)
 │   ├── 📁 FraudEngine.Application   # Casos de uso, Commands, Queries e Handlers (MediatR)
 │   ├── 📁 FraudEngine.Infrastructure# Persistência (EF Core/PostgreSQL), Mensageria (MassTransit)
 │   └── 📁 FraudEngine.Api           # Minimal APIs, Configurações e Endpoints HTTP
 └── 📁 tests/
     └── 📁 FraudEngine.Domain.Tests  # Testes unitários do Motor de Regras e Entidades

```

## ⚙️ Fluxo de Funcionamento (Arquitetura Orientada a Eventos)

1. **Ingestão:** A API recebe a requisição HTTP POST contendo os dados da transação (Valor, ID do usuário, IP, Dispositivo).

2. **Registro Inicial:** A transação é persistida no PostgreSQL com o status inicial de análise (`Pending`).

3. **Desacoplamento:** Um evento `TransactionCreatedEvent` é publicado no **RabbitMQ** via **MassTransit**.

4. **Processamento Assíncrono:** Um consumidor em segundo plano intercepta o evento e aciona o **Motor de Fraudes**.

5. **Avaliação por Cadeia:** O padrão *Chain of Responsibility* executa as regras em sequência, somando pontos ao *Score* de risco:

   * *Regra de Geolocalização* (Incompatibilidade de local)

   * *Regra de Limite / Velocidade* (Múltiplas transações em curto espaço de tempo)

   * *Regra de Dispositivo* (Fingerprint desconhecido)

6. **Veredito:** O sistema atualiza o status da transação no banco (Aprovado, Em Análise Manual, ou Rejeitado) e dispara notificações se necessário.

## 🚀 Como Executar o Projeto (Em Breve)

*(Instruções para quando o código for implementado)*

1. Clone o repositório:

   ```
   git clone https://github.com/seu-usuario/fraud-engine.git
   
   ```

2. Suba a infraestrutura local com o Docker Compose:

   ```
   docker-compose up -d
   
   ```

3. Execute as migrações do banco de dados e inicie a API.

## 👨‍💻 Autor

Desenvolvido com foco em boas práticas de engenharia de software e arquitetura de sistemas robustos. Sinta-se à vontade para entrar em contato ou acompanhar o progresso!
# 🛡️ Real-Time Fraud Engine (.NET Core)

> Um motor de prevenção a fraudes transacionais de alta performance, projetado com padrões corporativos de arquitetura de software, processamento assíncrono e mensageria.

## 📌 Sobre o Projeto

Este projeto foi concebido para simular um cenário real de engenharia de software no setor financeiro. O sistema intercepta transações financeiras em tempo real, avalia múltiplos fatores de risco através de um motor de regras modular e utiliza mensageria assíncrona para garantir escalabilidade e resiliência sem impactar a experiência do usuário.

## 🎯 Objetivos do Projeto

### **Objetivo Geral**

Desenvolver um motor de prevenção a fraudes transacionais de alta performance em .NET, aplicando princípios de *Clean Architecture* e o padrão *Chain of Responsibility* para avaliar, em tempo real, o risco de operações financeiras e mitigar perdas por atividades suspeitas.

### **Objetivos Específicos**

1. **Modelagem de Domínio e Arquitetura:** Implementar uma separação estricta de responsabilidades utilizando *Clean Architecture* e o padrão CQRS com *MediatR*.

2. **Motor de Regras Flexível:** Desenvolver um sistema baseado no padrão *Chain of Responsibility* para permitir a inclusão modular de regras de negócio independentes (geolocalização, velocidade transacional, dispositivos, etc.).

3. **Confiabilidade e Resiliência Assíncrona:** Integrar um broker de mensagens (**RabbitMQ** via **MassTransit**) para desacoplar a ingestão da transação do processamento pesado de risco.

4. **Persistência de Dados e Auditoria:** Configurar o Entity Framework Core com PostgreSQL para registrar transações, regras avaliadas e o veredito final de forma imutável.

5. **Qualidade via Testes Automatizados:** Escrever testes unitários rigorosos (utilizando *xUnit*) focados nas regras de negócio e no cálculo do *Score* de risco.

6. **Containerização Local (DevOps):** Orquestrar o ambiente de desenvolvimento completo utilizando *Docker Compose*.

## 🛠️ Stack Tecnológica

| Camada / Função | Tecnologia | 
 | ----- | ----- | 
| **Linguagem & Framework** | C# / .NET (ASP.NET Core Minimal APIs) | 
| **Arquitetura** | Clean Architecture, CQRS (MediatR), Chain of Responsibility | 
| **Banco de Dados** | PostgreSQL + Entity Framework Core | 
| **Mensageria** | RabbitMQ + MassTransit (Publish/Subscribe) | 
| **Resiliência & Tolerância** | Polly (Retry & Circuit Breaker) | 
| **Testes Automatizados** | xUnit, NSubstitute, Testcontainers | 
| **Infraestrutura Local** | Docker & Docker Compose | 

## 🏗️ Arquitetura do Sistema

A solution é dividida em quatro projetos principais para garantir o desacoplamento e a manutenibilidade:

```
📁 FraudEngine.sln
 ├── 📁 src/
 │   ├── 📁 FraudEngine.Domain        # Entidades, Value Objects e Contratos (Regras puras de negócio)
 │   ├── 📁 FraudEngine.Application   # Casos de uso, Commands, Queries e Handlers (MediatR)
 │   ├── 📁 FraudEngine.Infrastructure# Persistência (EF Core/PostgreSQL), Mensageria (MassTransit)
 │   └── 📁 FraudEngine.Api           # Minimal APIs, Configurações e Endpoints HTTP
 └── 📁 tests/
     └── 📁 FraudEngine.Domain.Tests  # Testes unitários do Motor de Regras e Entidades

```

## ⚙️ Fluxo de Funcionamento (Arquitetura Orientada a Eventos)

1. **Ingestão:** A API recebe a requisição HTTP POST contendo os dados da transação (Valor, ID do usuário, IP, Dispositivo).

2. **Registro Inicial:** A transação é persistida no PostgreSQL com o status inicial de análise (`Pending`).

3. **Desacoplamento:** Um evento `TransactionCreatedEvent` é publicado no **RabbitMQ** via **MassTransit**.

4. **Processamento Assíncrono:** Um consumidor em segundo plano intercepta o evento e aciona o **Motor de Fraudes**.

5. **Avaliação por Cadeia:** O padrão *Chain of Responsibility* executa as regras em sequência, somando pontos ao *Score* de risco:

   * *Regra de Geolocalização* (Incompatibilidade de local)

   * *Regra de Limite / Velocidade* (Múltiplas transações em curto espaço de tempo)

   * *Regra de Dispositivo* (Fingerprint desconhecido)

6. **Veredito:** O sistema atualiza o status da transação no banco (Aprovado, Em Análise Manual, ou Rejeitado) e dispara notificações se necessário.

## 🚀 Como Executar o Projeto (Em Breve)

*(Instruções para quando o código for implementado)*

1. Clone o repositório:

   ```
   git clone https://github.com/seu-usuario/fraud-engine.git
   
   ```

2. Suba a infraestrutura local com o Docker Compose:

   ```
   docker-compose up -d
   
   ```

3. Execute as migrações do banco de dados e inicie a API.

## 👨‍💻 Autor

Desenvolvido com foco em boas práticas de engenharia de software e arquitetura de sistemas robustos. Sinta-se à vontade para entrar em contato ou acompanhar o progresso!
