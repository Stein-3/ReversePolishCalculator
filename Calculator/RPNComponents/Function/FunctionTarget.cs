using Calculator.RPNException;
using System.Diagnostics.CodeAnalysis;

namespace Calculator.RPNComponents.Function
{
    /// <summary>
    /// ユーザーが自作した関数を扱うクラス
    /// </summary>
    internal class FunctionTarget : ICalculationTarget
    {
        public bool IsDefinitionInstance { get; init; } = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="isDefinitionInstance"></param>
        public FunctionTarget(bool isDefinitionInstance) => IsDefinitionInstance = isDefinitionInstance;

        internal static FunctionTarget DefinitionInstance => new FunctionTarget(true);

        public string Display() => IsDefinitionInstance ? "" : "func";

        public void Execute(IRPNStack calculationTargets)
        {
            calculationTargets.Push(this);
            throw new RuntimeException("定義式を実行できません：func");
        }

        public bool TryParse(string token, [NotNullWhen(true)] out ICalculationTarget result)
        {
            result = default!;

            if (token == "func" && Calculator.Mode == ParseMode.Regular)
                throw new SyntaxException("式が不正です: 関数の始まり「:」が見つかりません");

            if (token == "func")
            {
                Calculator.Mode = ParseMode.Regular;
                result = new FunctionTarget(false);
                return true;
            }
            return false;
        }
    }
}