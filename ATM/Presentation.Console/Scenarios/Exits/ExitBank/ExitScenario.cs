namespace Presentation.Console.Scenarios.Exits.ExitBank;

public class ExitScenario : IScenario
{
    public string Name => "Exit";
    public void Run()
    {
        Environment.Exit(0);
    }
}