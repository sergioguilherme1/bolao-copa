# Roadmap — Bolão da Copa do Mundo

## Milestone 1: Fundação (Passo 1)

**Goal:** Entidades e DbContext prontos, migrations rodando contra PostgreSQL

| Feature | Status | Notes |
|---------|--------|-------|
| Entidades: Usuario, Jogo, Aposta, CaixaBolao | done | Code-First com EF Core |
| BolaoDbContext + configurações | done | Npgsql provider |
| Migration inicial | done | |

## Milestone 2: Lógica de Negócio (Passo 2 + 3)

**Goal:** Services implementados com todas as regras de negócio

| Feature | Status | Notes |
|---------|--------|-------|
| ApostaService | done | Regra Pix (pendente + payload) vs Dinheiro (pago) |
| ApuracaoService | done | Cálculo do pote, ganhadores, acúmulo (zebra), precisão decimal |
| UsuarioService | done | Cadastro simples |

## Milestone 3: API (Passo 4)

**Goal:** Controllers REST expostos e consumíveis pelo n8n

| Feature | Status | Notes |
|---------|--------|-------|
| POST /api/usuarios | done | Retorna ID |
| POST /api/apostas | done | Retorna payload Pix ou confirmação |
| POST /api/webhooks/pagamento | done | Atualiza status para Pago |
| POST /api/jogos/{id}/finalizar | done | Aciona ApuracaoService |
| PATCH /api/apostas/{id}/cancelar | done | Cancelamento administrativo |

## Backlog / Futuro

- Autenticação administrativa (API key simples)
- Listagem de apostas por jogo
- Relatório de ganhadores histórico
- Integração real Mercado Pago (além do webhook receptor)
