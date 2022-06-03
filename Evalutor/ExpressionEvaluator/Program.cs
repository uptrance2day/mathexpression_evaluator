// See https://aka.ms/new-console-template for more information
using Evalutor;

Console.CancelKeyPress += Console_CancelKeyPress;
var calculator = new Calculator();
var isStopped = false;

void Console_CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
{
    e.Cancel = true;
    isStopped = true;
}

while (!isStopped)
{
    Console.WriteLine("Write mathematical expression:");
    var expression = Console.ReadLine();

    if (isStopped || expression == null)
    {
        break;
    }

    try
    {
        var result = calculator.Eval(expression);
        Console.WriteLine($"Result is: {result}");
    }
    catch (Exception e)
    {
        Console.WriteLine("Invalid");
    }
}
