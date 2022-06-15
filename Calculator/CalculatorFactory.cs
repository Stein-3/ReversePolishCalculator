namespace Calculator
{
    /// <summary>
    /// Calculatorのファクトリクラス
    /// シングルトン
    /// </summary>
    public class CalculatorFactory
    {
        private static ICalculator? Calculator { get; set; } = null;

        /// <summary>
        /// Calculatorを作成する
        /// </summary>
        /// <returns></returns>
        public static ICalculator CreateCalculator()
        {
            Calculator ??= new Calculator(new RPNStack());
            return Calculator;
        }
    }
}