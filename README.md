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

## Manual de uso dos componentes Corona

Este tópico descreve, de forma prática, como adicionar a biblioteca Corona ao seu projeto Blazor e como utilizar o sistema de theming e os componentes principais.

1) Adicionar a biblioteca

- Se estiver usando o projeto local (dentro da solução):

```powershell
dotnet add <YourBlazorProject> reference Corona/Corona.csproj
```

- Se a biblioteca for publicada como pacote NuGet:

```powershell
dotnet add <YourBlazorProject> package Starlight.Corona
```

2) Registrar serviços e theming

No Program.cs do seu projeto Blazor (WASM ou Server) registre os serviços de theming no container DI. Exemplos:

- Blazor WebAssembly (Program.cs):

```csharp
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>('#app');

// Registra o sistema de theming
builder.Services.AddCoronaTheming(CoronaThemes.Light());

await builder.Build().RunAsync();
```

- Blazor Server / Hosted (Program.cs):

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddCoronaTheming(CoronaThemes.Light());

var app = builder.Build();
// ...
app.Run();
```

Observação: os nomes dos métodos de extensão são os fornecidos pela biblioteca Corona do repositório (ex.: AddCoronaTheming, AddCoronaComponents). Ajuste caso sua versão exponha APIs diferentes.

3) Envolver a aplicação com o ThemeProvider

No App.razor (ou no MainLayout) envolva a aplicação com o provider para que o tema esteja disponível via cascading:

```razor
<CoronaThemeProvider>
	<Router AppAssembly="@typeof(Program).Assembly">
		<!-- ... -->
	</Router>
</CoronaThemeProvider>
```

4) Alternar tema em runtime

Injete o provedor e altere o tema programaticamente:

```razor
@inject CoronaThemeProvider ThemeProvider

<button @onclick="Toggle">Alternar tema</button>

@code {
	void Toggle() => ThemeProvider.SetTheme(CoronaThemes.Dark());
}
```

5) Usar componentes básicos

Exemplos de uso direto em .razor:

```razor
<CoronaButton Variant="CoronaButtonVariant.Primary" OnClick="OnSave">Salvar</CoronaButton>

<CoronaCard>
  <CardHeader>Relatório</CardHeader>
  <CardBody>
	<p>Conteúdo do card</p>
  </CardBody>
</CoronaCard>

<CoronaInput @bind-Value="model.Name" Placeholder="Nome" />
```

6) Sobrescrita de tema por componente

Você pode aplicar overrides de tokens para um componente específico (ex.: cor primária local):

```razor
<CoronaButton ThemeOverrides="new CoronaThemeOverrides { Primary = "#FF8800" }">Atenção</CoronaButton>
```

Também é possível passar um objeto de overrides no provider para afetar toda a aplicação.

7) Ícones e utilitários

Use o componente de ícone e helpers documentados na pasta Iconography:

```razor
<CoronaIcon Name="check" Size="24" />
```

8) Exemplos e documentação

A pasta BlazorApp contém exemplos práticos e demos dos componentes. Execute a aplicação de exemplo para ver o comportamento de theming, overrides e componentes interativos.

```powershell
dotnet run --project BlazorApp/BlazorApp
```

Dicas rápidas:
- Prefira usar CoronaThemeProvider no topo da aplicação para consistência.
- Use ThemeOverrides para ajustes locais sem quebrar o tema global.
- Consulte os componentesda pasta Corona/Components para parâmetros disponíveis.
```
