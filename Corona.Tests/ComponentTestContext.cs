using Bunit;
using Corona.Theming;

namespace Corona.Tests;

public abstract class ComponentTestContext : BunitContext
{
    protected ComponentTestContext()
    {
        Services.AddCoronaTheming(CoronaThemes.Light());
    }
}
