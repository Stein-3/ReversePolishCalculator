using Calculator.RPNException;
using System.Diagnostics.CodeAnalysis;

namespace Calculator.RPNComponents.Function
{
    internal class FuncInitTarget : ICalculationTarget
    {
        public bool IsDefinitionInstance { get; init; } = false;

        public FuncInitTarget(bool isDefinitionInstance) => IsDefinitionInstance = isDefinitionInstance;

        internal static FuncInitTarget DefinitionInstance => new(true);

        public string Display() => IsDefinitionInstance ? "" : ":";

        public void Execute(IRPNStack calculationTargets)
            => throw new RuntimeException("定義式を実行できません：「:」");

        public bool TryParse(string token, [NotNullWhen(true)] out ICalculationTarget result)
        {
            result = default!;

            if (token == ":")
            {
                if (Calculator.Mode == ParseMode.Definition)
                    throw new SyntaxException("関数定義中に「:」は使用できません");

                Calculator.Mode = ParseMode.Definition;
                result = new FuncInitTarget(false);
                return true;
            }

            return false;
        }
    }
}