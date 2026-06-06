# Bolão da Copa do Mundo

**Vision:** API RESTful para gerenciar um bolão da Copa do Mundo entre amigos, integrada ao n8n via WhatsApp para automação do fluxo de apostas e pagamentos.
**For:** Grupos de amigos que organizam bolões da Copa do Mundo
**Solves:** Automatiza o cadastro de apostas, controle de pagamentos (Pix/Dinheiro), apuração de resultados e distribuição de prêmios — eliminando o controle manual via planilhas ou anotações.

## Goals

- Fornecer uma API funcional que o n8n consuma via WhatsApp para registrar usuários, apostas e pagamentos sem intervenção manual
- Apurar resultados automaticamente ao finalizar um jogo, calculando e distribuindo o prêmio com precisão decimal sem perdas de arredondamento

## Tech Stack

**Core:**

- Framework: ASP.NET Core (Web API)
- Language: C# (.NET 8)
- Database: PostgreSQL
- ORM: Entity Framework Core (Code-First)

**Key dependencies:**
- Entity Framework Core + Npgsql provider
- ASP.NET Core minimal/controller-based APIs
- FluentValidation (opcional, para validação de requests)

**Arquitetura:** 3 camadas simples — Controllers → Services → DbContext (sem Clean Architecture, sem DDD)

## Scope

**v1 includes:**

- CRUD de usuários (cadastro com Nome, Telefone, ChavePix)
- Cadastro de apostas com suporte a pagamento Pix (pendente + payload "Copia e Cola") e Dinheiro (pago imediatamente)
- Webhook de confirmação de pagamento (Mercado Pago)
- Finalização de jogo com apuração automática: cálculo do pote, identificação de ganhadores, distribuição proporcional ou acúmulo (zebra)
- Cancelamento administrativo de apostas
- Controle de caixa do bolão (CaixaBolao com saldo acumulado)

**Explicitly out of scope:**

- Autenticação/autorização de usuários (sem JWT, sem login)
- Interface web ou mobile
- Integração real com gateway Mercado Pago (apenas webhook receptor)
- Múltiplos bolões simultâneos
- Histórico de temporadas anteriores

## Constraints

- Timeline: entrega rápida (MVP funcional)
- Technical: arquitetura simples de 3 camadas — proibido Clean Architecture, DDD ou múltiplas camadas de abstração
- Resources: projeto individual / pequeno time
