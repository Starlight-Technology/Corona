# Corona

Biblioteca de componentes **Blazor** com sistema de theming próprio, baseado em tokens de design (primitivos + semânticos), com suporte a temas claro/escuro e sobrescrita por componente ou aplicação.

## Estrutura do projeto

| Projeto | Descrição |
| --- | --- |
| `Corona/` | Biblioteca de componentes Razor (`net10.0`) — núcleo da solução |
| `BlazorApp/` | Aplicação de documentação/exemplos (Blazor Web App com render interativo Server + WebAssembly) |
| `Corona.Tests/` | Testes de unidade e renderização (xUnit + bUnit) |

## Componentes

- **Layout:** `CoronaContainer`, `CoronaStack`, `CoronaLayoutShell`, `CoronaPageHeader`
- **Inputs:** `CoronaButton`, `CoronaInput`, `CoronaTextArea`, `CoronaToggle`, `CoronaSelect`, `CoronaFileUpload`, `CoronaFileDropZone`
- **Data Display:** `CoronaBadge`, `CoronaCard`, `CoronaMetricCard`, `CoronaChart`, `CoronaDonutChart`, `CoronaIcon`, `CoronaList`
- **Feedback:** `CoronaDialog`, `CoronaLoading`, `CoronaProgressBar`
- **Navegação:** `CoronaTabs`, `CoronaTabItem`, `CoronaNavMenu`, `CoronaDrawer`, `CoronaFloatingAction`, `CoronaSwipeNavigator`
- **Extras:** `CoronaChat`, `CoronaThemeCascadingValue`

A maioria dos componentes possui estilos scoped (`.razor.css`) com classes no padrão BEM (`corona-button__label`).

## Theming

O sistema de temas usa dois níveis de tokens:

- **Primitivos** (`CoronaPrimitiveTokens`): cores de marca, espaçamento, raios, sombras e tipografia.
- **Semânticos** (`CoronaSemanticTokens`): derivados dos primitivos, definem cores de superfície, texto, bordas, elevação etc.

Recursos:

- Temas built-in claro e escuro via `CoronaThemes.Light()` / `CoronaThemes.Dark()`.
- Provider de runtime (`CoronaThemeProvider`) com evento `OnChange`, permitindo alternância dinâmica de tema em toda a UI.
- Sobrescrita parcial de tokens (`CoronaThemeOverrides`) por componente (`ThemeOverrides`) ou globalmente.
- Registro via DI: `builder.Services.AddCoronaTheming(initialTheme)`.

## Como executar

Pré-requisitos: SDK .NET 10.

```powershell
dotnet restore
dotnet run --project BlazorApp/BlazorApp
```

A aplicação de exemplos é aberta em `https://localhost:5001` (verifique as portas em `launchSettings.json`).

## Testes

```powershell
dotnet test
```

A suíte cobre o sistema de theming (padrões claro/escuro, overrides, provider), a renderização dos componentes com bUnit e a navegação por gesto (`CoronaSwipeNavigator`).

## Estrutura de pastas (Corona)

```
Corona/
├── Components/
│   ├── Enums/          # Variantes, tamanhos, posições etc.
│   ├── Iconography/    # Conjunto de ícones
│   ├── Models/         # Modelos de dados dos componentes
│   ├── Navigation/     # Exemplo de navegação para docs
│   └── ... (*.razor)   # Componentes (com .razor.css scoped)
└── Theming/            # Tokens, temas e provider
```
