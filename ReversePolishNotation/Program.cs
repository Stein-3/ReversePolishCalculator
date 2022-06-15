using Calculator;

var helpMessage = @"""pop"": Pop one stack from stackmachine
""clean"": delete all stack from stackmachine
""push"": push formula to stackmachine
""display"": Display the content of stackmachine
""run"": Run and calulate stacks
""exit"": finish calculate";

var calculator = CalculatorFactory.CreateCalculator();
var flag = true;

while (flag)
{
    var input = Console.ReadLine();

    switch (input)
    {
        case "pop":
        case "Pop":
            calculator.Pop();
            break;

        case "clean":
        case "Clean":
            calculator.Clean();
            break;

        case "push":
        case "Push":
            var formula = Console.ReadLine();
            if (formula is null) break;
            calculator.Push(formula);
            break;

        case "display":
        case "Display":
            Console.WriteLine(calculator.DisplayStack());
            break;

        case "run":
        case "Run":
            calculator.Run();
            break;

        case "commands":
            Console.WriteLine(calculator.GetAllCommand());
            break;

        case "count":
            Console.WriteLine(calculator.GetStackCount());
            break;

        case "call":
            var command = Console.ReadLine();
            string? arg;
            if (command is null) break;
            if (command == "push") arg = Console.ReadLine();
            Console.WriteLine(calculator.CallCommand(command, args));
            break;

        case "h":
        case "H":
            Console.WriteLine(helpMessage);
            break;

        case "exit":
        case "Exit":
            flag = false;
            break;
    }
}