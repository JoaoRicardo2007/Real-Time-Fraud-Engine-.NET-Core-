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
| :--- | :--- |
| **Linguagem & Framework** | C# / .NET (ASP.NET Core Minimal APIs) |
| **Arquitetura** | Clean Architecture, CQRS (MediatR), Chain of Responsibility |
| **Banco de Dados** | PostgreSQL + Entity Framework Core |
| **Mensageria** | RabbitMQ + MassTransit (Publish/Subscribe) |
| **Resiliência & Tolerância** | Polly (Retry & Circuit Breaker) |
| **Testes Automatizados** | xUnit, NSubstitute, Testcontainers |
| **Infraestrutura Local** | Docker & Docker Compose |

## 🏗️ Arquitetura do Sistema

A solution é dividida em quatro projetos principais para garantir o desacoplamento e a manutenibilidade:

```text
📁 FraudEngine.sln
 ├── 📁 src/
 │   ├── 📁 FraudEngine.Domain        # Entidades, Value Objects e Contratos (Regras puras de negócio)
 │   ├── 📁 FraudEngine.Application   # Casos de uso, Commands, Queries e Handlers (MediatR)
 │   ├── 📁 FraudEngine.Infrastructure# Persistência (EF Core/PostgreSQL), Mensageria (MassTransit)
 │   └── 📁 FraudEngine.Api           # Minimal APIs, Configurações e Endpoints HTTP
 └── 📁 tests/
     └── 📁 FraudEngine.Domain.Tests  # Testes unitários do Motor de Regras e Entidades
