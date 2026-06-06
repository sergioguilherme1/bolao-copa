# State — Bolão da Copa do Mundo

## Decisions

- **Arquitetura 3 camadas simples:** Controllers → Services → DbContext. Proibido Clean Architecture e DDD. Motivação: foco em simplicidade e entrega rápida.
- **CaixaBolao com ID fixo = 1:** Registro único, sem múltiplos bolões. Simplifica queries de saldo.
- **Precisão decimal na apuração:** Usar `decimal` (não `double`/`float`) em toda lógica financeira. Divisão de prêmio arredondada para baixo em 2 casas (Math.Floor com fator 100).
- **Pix simulado:** v1 não integra Mercado Pago real — apenas simula o payload "Copia e Cola" no response. Webhook receptor existe para quando a integração real for adicionada.
- **Dinheiro = Pago imediato:** Apostas com FormaPagamento.Dinheiro entram com StatusPagamento.Pago diretamente, sem aguardar confirmação.

## Blockers

_(nenhum)_

## Todos

- [ ] Criar solution e projeto .NET 8 Web API
- [ ] Configurar Npgsql + EF Core
- [ ] Implementar entidades e DbContext (Milestone 1)
- [ ] Implementar Services (Milestone 2)
- [ ] Implementar Controllers (Milestone 3)

## Lessons

_(nenhum ainda)_

## Deferred Ideas

- Autenticação administrativa via API key
- Listagem/histórico de apostas e ganhadores
- Integração real Mercado Pago
- Suporte a múltiplos bolões simultâneos
