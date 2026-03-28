using Bunit;
using Corona.Theming;

namespace Corona.Tests;

public abstract class ComponentTestContext : TestContext
{
    protected ComponentTestContext()
    {
        Services.AddCoronaTheming(CoronaThemes.Light());
    }
}
