using System.Reflection;
using Calculator;

Console.WriteLine("RPN Calculator (Preview)");

var calculator = CalculatorFactory.CreateCalculator();

var commands = new Dictionary<string, Action>();

commands["exit"] = () => { calculator = null; };
commands["quit"] = () => { calculator = null; };
commands["help"] = () => { Console.WriteLine(string.Join(", ", commands.Keys)); };


while (calculator is not null)
{
    var methods = calculator.GetType().GetMethods().Where(method => method.GetCustomAttribute<CommandAttribute>() is not null);
    foreach (var method in methods)
    {
        var attr = method.GetCustomAttribute<CommandAttribute>();
        if (attr != null)
        {
            foreach (var name in attr.GetCallName())
            {
                commands[name] = () => method.Invoke(calculator, null);
            }

        }
    }

    var stack = calculator.DisplayStack();
    Console.WriteLine(stack);

    Console.Write($"[{calculator.GetStackCount()}]>");

    var input = Console.ReadLine();

    if (input == null) continue;

    try
    {
        if (commands.TryGetValue(input, out var command))
        {
            command();
            continue;
        }

        calculator.Push(input);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}