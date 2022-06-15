namespace Calculator.RPNComponents.FlowControl
{
    internal class End : FlowControlBase<End>
    {
        protected override string Name => ";";

        public override void Execute(IRPNStack calculationTargets) { }
    }
}
