namespace Calculator.RPNComponents.FlowControl
{
    internal class Loop : FlowControlBase<Loop>
    {
        protected override string Name => "loop";

        public override void Execute(IRPNStack calculationTargets)
        {
            var counter = (int)GetParameter(calculationTargets).ToDouble();
            // 現在は1つのコマンドを繰り返す仕様。文節や式をまとめる構文（関数）が出来れば、繰り返しにすることができる。
            var command = calculationTargets.Pop();
            for (var index = 0; index < counter; index++)
            {
                command.Execute(calculationTargets);
            }
        }
    }
}