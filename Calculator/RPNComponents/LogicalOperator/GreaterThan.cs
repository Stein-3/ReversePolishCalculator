namespace Calculator.RPNComponents.LogicalOperator
{
    internal class GreaterThan : LogicalOperatorBase
    {
        public static ICalculationTarget DefinitionInstance = new GreaterThan { IsDefinitionInstance = true };

        protected override string Name => "<";

        public override ICalculationTarget Compare(NumberTarget value1, NumberTarget value2) => ToNumberTarget(value1.ToDouble() < value2.ToDouble());

        protected override ICalculationTarget Create() => new GreaterThan();
    }
}
