namespace Calculator.RPNComponents.FlowControl
{
    internal class If : FlowControlBase<If>
    {
        protected override string Name => "if";

        public override void Execute(IRPNStack calculationTargets)
        {
            var condition = GetParameter(calculationTargets).ToDouble();
            if (condition == 0)
            {
                while (calculationTargets.Pop() is not End) ;
            }
            calculationTargets.Pop().Execute(calculationTargets);
        }
    }
}
