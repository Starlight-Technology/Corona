# AGENTS.md

Guia para agentes de IA trabalhando neste repositório.

## Visão geral

Repositório de uma biblioteca de componentes Blazor (.NET 10) com sistema de theming próprio baseado em tokens de design.

| Projeto | Caminho | Descrição |
| --- | --- | --- |
| Corona | `Corona/` | Biblioteca de componentes Razor (`Microsoft.NET.Sdk.Razor`, `net10.0`) |
| BlazorApp | `BlazorApp/` | App de documentação/exemplos (Blazor Web App, render Server + WebAssembly interativo) |
| Corona.Tests | `Corona.Tests/` | Testes xUnit + bUnit |

Solução: `Corona.slnx`.

## Comandos

```powershell
dotnet restore
dotnet build
dotnet test                          # testa Corona.Tests
dotnet run --project BlazorApp/BlazorApp   # app de exemplos
```

Não há lint/analyzer configurado além dos padrões do SDK; rode `dotnet build` e `dotnet test` antes de concluir alterações.

## Arquitetura

- `Corona/Theming/` — sistema de temas:
  - `CoronaPrimitiveTokens` (cores de marca, espaçamento, raio, sombras, tipografia) e `CoronaSemanticTokens` (derivados dos primitivos por modo de cor).
  - `CoronaTheme` (record imutável) com `WithOverrides(CoronaThemeOverrides)` para sobrescrita parcial.
  - `CoronaThemes.Light()` / `CoronaThemes.Dark()` para temas built-in.
  - `CoronaThemeProvider` — serviço singleton de runtime com evento `OnChange`; registrado via `AddCoronaTheming()`.
- `Corona/Components/` — componentes; cada um com `*.razor` e, quando necessário, estilos scoped `*.razor.css`.
- `AddCoronaLayout()` (em `Corona/Components/Navigation/`) registra o `CoronaDrawerStateService` (Scoped), usado pelos componentes interativos de layout (`CoronaHeaderIsland` e `CoronaInteractiveDrawer`) para sincronizar drawer e header; registrar também no client WASM.

## Convenções

- **Nomes:** prefixo `Corona` em todos os componentes, enums e modelos (`CoronaButton`, `CoronaButtonVariant`, `CoronaSelectOption`). Namespace `Corona.Components` (declarado com `@namespace`), enums em `Corona.Components.Enums`, modelos em `Corona.Components.Models`, ícones em `Corona/Components/Iconography/CoronaIcons.cs`.
- **Theming nos componentes:** componentes que usam cores/estilos devem receber `[CascadingParameter] CoronaTheme? Theme`, injetar `CoronaThemeProvider`, se inscrever em `OnChange` (com `IDisposable` para cancelar) e expor `[Parameter] CoronaThemeOverrides? ThemeOverrides`, resolvendo o tema ativo via `(Theme ?? ThemeProvider.CurrentTheme).WithOverrides(ThemeOverrides)` — ver `CoronaButton.razor` como referência. O `CoronaThemeCascadingValue` disponibiliza o tema em cascata para a árvore.
- **Estilos:** classes no padrão BEM (`corona-button__label`); estilos scoped em `.razor.css` (alguns componentes ainda usam blocos `<style>` inline no `.razor` — prefira scoped ao criar novos). Não use hardcode de cores hex sem passar pelo sistema de tokens sempre que fizer sentido; componentes menores ("leaf" como `CoronaMetricCard`) podem usar paletas fixas.
- **C#:** `Nullable` e `ImplicitUsings` habilitados; use `record` para modelos imutáveis; `IReadOnlyList<T>` para parâmetros de coleções; `EventCallback` para eventos.
- **Comentários:** sem comentários no código, exceto doc XML em APIs públicas do `Corona/Theming/`.

## Testes

- xUnit + bUnit em `Corona.Tests/`.
- `ComponentTestContext.cs` é a classe base para renderização de componentes; injete serviços via `Services.Add...` e registre um `TestNavigationManager` (ver `CoronaSwipeNavigatorTests.cs`) para testes que dependem de navegação.
- Use `@key="Nav.Uri"`/regeneração com `TriggerEvent` para simular gestos e eventos DOM (`PointerEventArgs`, etc.).
- Padrões de parâmetro para testes de render: `.Add(x => x.Prop, value)` com `AddChildContent` quando houver `ChildContent`.

## App de exemplos

- Páginas em `BlazorApp/BlazorApp/Components/Pages/*.razor` (rota `@page "/components/<slug>"`).
- Cada página de exemplo usa `ExampleSection`, `CodeSnippet` e `ParameterTable` (em `Components/Shared/`) para documentar o componente.
- Navegação da docs é definida em `BlazorApp/BlazorApp/Components/Navigation/DocsNavigation.cs` (lista de `CoronaNavItem`) — ao criar uma nova página de exemplo, adicione o item lá.
- `Program.cs` registra theming (`AddCoronaTheming`) e assemblies adicionais para render interativo.

## Fluxo de trabalho ao adicionar um componente

1. Criar enums/modelos em `Corona/Components/Enums|Models/` (nomes `Corona*`).
2. Criar `Corona*.razor` com `@namespace Corona.Components` e estilos scoped `.razor.css`.
3. Integrar theming (cascading theme + provider) se o componente usar cores/estilo.
4. Adicionar página de exemplo em `BlazorApp/.../Pages/` + entrada em `DocsNavigation.cs`.
5. Adicionar testes bUnit em `Corona.Tests/` cobrindo parâmetros e interações principais.
6. Rodar `dotnet build` e `dotnet test`.
