using Calculator.RPNComponents;
using System.Diagnostics.CodeAnalysis;

namespace Calculator
{
    internal interface IRPNStack : IEnumerable<ICalculationTarget>
    {
        void Push(ICalculationTarget item);

        ICalculationTarget Pop();

        bool TryPeek([NotNullWhen(true)] out ICalculationTarget item);

        ICalculationTarget[] ToArray();
    }
}