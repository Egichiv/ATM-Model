using System.Diagnostics.CodeAnalysis;

namespace Presentation.Console.Scenarios.Exits.ExitBank;

public class ExitScenarioProvider : IScenarioProvider
{
    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        scenario = new ExitScenario();
        return true;
    }
}