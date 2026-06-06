# Tasks — Bolão da Copa do Mundo API

## T1: Solution + projeto Web API .NET 8
**What:** Criar a solution e o projeto ASP.NET Core Web API com estrutura de pastas
**Where:** `/` (raiz do repositório)
**Depends on:** —
**Done when:** `dotnet build` passa sem erros
**Gate:** build

## T2: Entidades e DbContext
**What:** Criar as 4 entidades (Usuario, Jogo, Aposta, CaixaBolao) e o BolaoDbContext com configurações EF Core
**Where:** `src/Models/`, `src/Data/`
**Depends on:** T1
**Done when:** `dotnet build` passa; entidades têm todas as propriedades e enums corretos
**Gate:** build

## T3: Migration inicial + seed CaixaBolao
**What:** Criar a migration inicial e seed do registro CaixaBolao (ID=1, SaldoAcumulado=0)
**Where:** `src/Data/Migrations/`, `src/Data/BolaoDbContext.cs`
**Depends on:** T2
**Done when:** Migration gerada; seed do CaixaBolao presente
**Gate:** build

## T4: UsuarioService
**What:** Serviço para cadastro de usuário (sem duplicidade por Telefone)
**Where:** `src/Services/UsuarioService.cs`
**Depends on:** T2
**Done when:** `dotnet build` passa
**Gate:** build

## T5: ApostaService
**What:** Serviço com regras Pix (pendente + payload simulado) vs Dinheiro (pago imediato); validação de jogo existente e não finalizado
**Where:** `src/Services/ApostaService.cs`
**Depends on:** T2
**Done when:** `dotnet build` passa; ambos os cenários de pagamento implementados
**Gate:** build

## T6: ApuracaoService
**What:** Apuração ao finalizar jogo — soma pote, identifica ganhadores, distribui (arredondamento para baixo 2 casas) ou acumula (zebra)
**Where:** `src/Services/ApuracaoService.cs`
**Depends on:** T2
**Done when:** `dotnet build` passa; ambos os cenários (com/sem ganhadores) implementados com precisão decimal
**Gate:** build

## T7: Controllers (5 endpoints)
**What:** UsuariosController, ApostasController, WebhooksController, JogosController com os 5 endpoints definidos no plano
**Where:** `src/Controllers/`
**Depends on:** T4, T5, T6
**Done when:** `dotnet build` passa; todos os endpoints mapeados corretamente
**Gate:** build

## Status

| Task | Status |
|------|--------|
| T1 | pending |
| T2 | pending |
| T3 | pending |
| T4 | pending |
| T5 | pending |
| T6 | pending |
| T7 | pending |
