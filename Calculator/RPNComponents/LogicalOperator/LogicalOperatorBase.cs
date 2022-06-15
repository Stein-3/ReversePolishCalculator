namespace Calculator.RPNComponents
{
    internal abstract class LogicalOperatorBase : CalculatonTargetBase
    {
        public override void Execute(IRPNStack calculationTargets)
        {
            var value1 = GetTwoNumberFromStack(calculationTargets);
            var value2 = GetTwoNumberFromStack(calculationTargets);

            calculationTargets.Push(Compare(value1, value2));
        }

        public abstract ICalculationTarget Compare(NumberTarget value1, NumberTarget value2);

        protected NumberTarget ToNumberTarget(bool value) => value ? new NumberTarget(1, 1) : new NumberTarget(1, 0);
    }
}
