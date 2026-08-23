using Corona.Components.Navigation;
using Corona.Theming;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddCoronaTheming(CoronaThemes.Light());
builder.Services.AddCoronaLayout();

await builder.Build().RunAsync();
