namespace Calculator.RPNComponents.Operator
{
    /// <summary>
    /// 減算を扱うクラス
    /// </summary>
    internal class Subtraction : OperatorBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Subtraction()
        { }

        internal static ICalculationTarget DefinitionInstance => new Subtraction { IsDefinitionInstance = true };

        protected override string Name => "-";

        protected override NumberModel Calcurate(NumberTarget numberTarget1, NumberTarget numberTarget2)
            => new()
            {
                Numerator = (double)(numberTarget1.Numerator * numberTarget2.Denominator -
                    numberTarget2.Numerator * numberTarget2.Denominator),
                Denominator = (double)(numberTarget1.Denominator * numberTarget2.Denominator),
            };

        protected override ICalculationTarget Create() => new Subtraction();
    }
}