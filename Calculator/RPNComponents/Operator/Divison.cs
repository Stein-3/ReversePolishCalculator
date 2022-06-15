using Calculator.RPNException;

namespace Calculator.RPNComponents.Operator
{
    /// <summary>
    /// 除算を扱うクラス
    /// </summary>
    internal class Divison : OperatorBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Divison()
        { }

        internal static ICalculationTarget DefinitionInstance => new Divison { IsDefinitionInstance = true };

        protected override string Name => "/";

        protected override NumberModel Calcurate(NumberTarget numberTarget1, NumberTarget numberTarget2)
        {
            if (numberTarget2.Numerator == 0) throw new RuntimeException("0で値を割ることはできません");

            return new NumberModel
            {
                Denominator = (double)(numberTarget1.Denominator * numberTarget2.Numerator),
                Numerator = (double)(numberTarget1.Numerator * numberTarget2.Denominator)
            };
        }

        protected override ICalculationTarget Create() => new Subtraction();
    }
}