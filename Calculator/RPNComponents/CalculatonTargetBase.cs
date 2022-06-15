using Calculator.RPNException;
using System.Diagnostics.CodeAnalysis;

namespace Calculator.RPNComponents
{

    /// <summary>
    /// 加減乗除の基本的な演算を扱う抽象クラス
    /// </summary>
    internal abstract class CalculatonTargetBase : ICalculationTarget
    {
        /// <summary>
        /// 定義のみを扱うインスタンスか、実際に計算を実行するインスタンスかを判別する
        /// </summary>
        public bool IsDefinitionInstance { get; init; } = false;


        [NotNull]
        protected abstract string Name { get; }

        /// <summary>
        /// 指定したスタックから2つ値を取り出し、演算する
        /// </summary>
        /// <param name="calculationTargets"></param>
        /// <param name="calcFunc"></param>
        /// <exception cref="Exception"></exception>
        protected void Execute(IRPNStack calculationTargets, Func<NumberTarget, NumberTarget, NumberModel> calcFunc)
        {
            if (IsDefinitionInstance) throw new RuntimeException("式が不正です");

            var numberTarget1 = GetTwoNumberFromStack(calculationTargets);
            var numberTarget2 = GetTwoNumberFromStack(calculationTargets);

            var basicCalcResult = calcFunc(numberTarget1, numberTarget2);

            var greatestCommonDivisor = CalculationHelper.CalcGreatestCommonDivisor(
                basicCalcResult.Denominator, basicCalcResult.Numerator);

            if (greatestCommonDivisor != 0)
            {
                basicCalcResult.Denominator /= greatestCommonDivisor;
                basicCalcResult.Numerator /= greatestCommonDivisor;
            }

            calculationTargets.Push(
                new NumberTarget(basicCalcResult.Denominator, basicCalcResult.Numerator));
        }

        protected static NumberTarget GetTwoNumberFromStack(IRPNStack calculationTargets)
        {
            var arg1 = calculationTargets.Pop();
            if (arg1 is not NumberTarget numberTarget1)
            {
                arg1.Execute(calculationTargets);

                arg1 = calculationTargets.Pop();

                if (arg1 is not NumberTarget)
                    throw new RuntimeException("式が不正です");

                numberTarget1 = (NumberTarget)arg1;
            }

            return numberTarget1;
        }

        public string Display() => IsDefinitionInstance ? string.Empty : Name;

        public abstract void Execute(IRPNStack calculationTargets);

        public class NumberModel
        {
            internal double Numerator { get; set; }

            internal double Denominator { get; set; }
        }

        protected abstract ICalculationTarget Create();

        /// <summary>
        /// tokenが<see cref="Name" />かどうかを識別し、正ならば自身を返す
        /// </summary>
        /// <param name="token"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryParse(string token, [NotNullWhen(true)] out ICalculationTarget result)
        {
            if (token == Name)
            {
                result = Create();
                return true;
            }

            result = default!;
            return false;
        }
    }
}
