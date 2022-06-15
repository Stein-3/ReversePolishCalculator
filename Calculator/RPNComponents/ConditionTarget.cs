using System.Diagnostics.CodeAnalysis;

namespace Calculator.RPNComponents
{
    /// <summary>
    /// 条件式を扱うクラス
    /// </summary>
    internal class ConditionTarget : ICalculationTarget
    {
        public bool IsDefinitionInstance { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }

        public string Display()
        {
            throw new NotImplementedException();
        }

        public void Execute(IRPNStack calculationTargets)
        {
            throw new NotImplementedException();
        }

        public bool TryParse(string token, [NotNullWhen(true)] out ICalculationTarget result)
        {
            throw new NotImplementedException();
        }
    }
}