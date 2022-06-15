namespace Calculator.RPNComponents.Operator
{
    internal abstract class OperatorBase : CalculatonTargetBase
    {
        protected abstract NumberModel Calcurate(NumberTarget numberTarget1, NumberTarget numberTarget2);

        /// <summary>
        /// 指定したスタックから2つの値を取り出し、乗算を行ってスタックに返す
        /// </summary>
        /// <param name="calculationTargets">操作するスタック</param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Execute(IRPNStack calculationTargets)
            => Execute(calculationTargets, Calcurate);

    }
}
