namespace Calculator.RPNComponents.FlowControl
{
    internal abstract class FlowControlBase<T> : CalculatonTargetBase
        where T : class, ICalculationTarget, new()
    {
        static public T DefinitionInstance = new T { IsDefinitionInstance = true };

        protected NumberTarget GetParameter(IRPNStack calculationTargets)
        {
            var target = calculationTargets.Pop();
            NumberTarget param;
            if (target is NumberTarget)
                param = (NumberTarget)target;
            else
            {
                target.Execute(calculationTargets);
                param = (NumberTarget)calculationTargets.Pop();
            }
            return param;
        }

        protected override ICalculationTarget Create() => new T();
    }
}
