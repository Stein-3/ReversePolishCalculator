namespace Calculator.RPNComponents.Operator
{
    /// <summary>
    /// 加算を扱うクラス
    /// </summary>
    internal class Addition : OperatorBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Addition()
        { }

        internal static ICalculationTarget DefinitionInstance => new Addition { IsDefinitionInstance = true };

        protected override string Name => "+";

        protected override ICalculationTarget Create() => new Addition();

        protected override NumberModel Calcurate(NumberTarget numberTarget1, NumberTarget numberTarget2)
            => new()
            {
                Denominator = (double)(numberTarget1.Denominator * numberTarget2.Denominator),
                Numerator = (double)(numberTarget1.Numerator * numberTarget2.Denominator +
                    numberTarget2.Numerator * numberTarget2.Denominator),
            };
    }
}