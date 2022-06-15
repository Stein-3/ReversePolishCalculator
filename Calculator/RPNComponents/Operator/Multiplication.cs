namespace Calculator.RPNComponents.Operator
{
    /// <summary>
    /// 乗算を扱うクラス
    /// </summary>
    internal class Multiplication : OperatorBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Multiplication()
        { }

        internal static ICalculationTarget DefinitionInstance => new Multiplication { IsDefinitionInstance = true };

        protected override string Name => "*";

        protected override NumberModel Calcurate(NumberTarget numberTarget1, NumberTarget numberTarget2)
            => new()
            {
                Denominator = (double)(numberTarget1.Denominator * numberTarget2.Denominator),
                Numerator = (double)(numberTarget1.Numerator * numberTarget2.Numerator),
            };

        protected override ICalculationTarget Create() => new Multiplication();
    }
}