using System.Reflection;
using Calculator.RPNComponents;
using Calculator.RPNComponents.FlowControl;
using Calculator.RPNComponents.Function;
using Calculator.RPNComponents.LogicalOperator;
using Calculator.RPNComponents.Operator;
using Calculator.RPNException;

namespace Calculator
{
    /// <summary>
    /// RPN計算機の統合クラス
    /// </summary>
    internal class Calculator : ICalculator
    {
        private IRPNStack TargetStack { get; set; }

        /// <summary>
        /// スタックが変更された場合に、ViewModelに変更を通知する
        /// </summary>
        public event EventHandler<string[]>? StackChanged;

        /// <summary>
        /// パース時のモード
        /// 定義モードまたは、通常モード
        /// </summary>
        internal static ParseMode Mode { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Calculator(IRPNStack targets)
        {
            TargetStack = targets;

            TargetStack.Push(NumberTarget.DefinitionInstance);
            TargetStack.Push(Addition.DefinitionInstance);
            TargetStack.Push(Multiplication.DefinitionInstance);
            TargetStack.Push(Subtraction.DefinitionInstance);
            TargetStack.Push(Divison.DefinitionInstance);
            TargetStack.Push(GreaterThan.DefinitionInstance);
            TargetStack.Push(GreaterOrEqual.DefinitionInstance);
            TargetStack.Push(LessThan.DefinitionInstance);
            TargetStack.Push(LessOrEqual.DefinitionInstance);
            TargetStack.Push(Loop.DefinitionInstance);
            TargetStack.Push(If.DefinitionInstance);
            TargetStack.Push(End.DefinitionInstance);
            TargetStack.Push(FunctionTarget.DefinitionInstance);
            TargetStack.Push(FuncInitTarget.DefinitionInstance);
            TargetStack.Push(FuncArgTarget.DefinitionInstance);
            TargetStack.Push(FuncNameTarget.DefinitionInstance);
        }

        /// <summary>
        /// 式をパースして、スタックに挿入する
        /// </summary>
        /// <param name="formula"></param>
        /// <exception cref="SyntaxException"></exception>
        public void Push(string formula)
        {
            foreach (var token in RPNLexicalAnalyzer.Analyze(formula))
            {
                var flag = false;
                foreach (var target in TargetStack.ToArray())
                {
                    var result = target.TryParse(token, out var parseResult);
                    if (!result) continue;

                    TargetStack.Push(parseResult);
                    StackChanged?.Invoke(this, TargetStack.Select(t => t.Display()).ToArray());
                    flag = true;
                    break;
                }

                if (!flag) throw new SyntaxException("不正な式が入力されています");
            }
        }

        /// <summary>
        /// スタックの状態を表示する
        /// </summary>
        /// <returns></returns>
        [Command("display", Description = "スタックの状態を表示します。")]
        public string DisplayStack()
            => string.Join(" ", TargetStack.Select(t => t.Display()).Where(t => !string.IsNullOrEmpty(t)));

        /// <summary>
        /// スタックから1つ値を取り出す
        /// </summary>
        /// <returns></returns>
        /// <exception cref="RuntimeException"></exception>
        [Command("pop", Description = "スタックから式を1つ取り出します。")]
        public string Pop()
        {
            (var displayTarget, var result) = Pop("");

            if (result is FunctionTarget)
            {
                while (result is not FuncInitTarget)
                {
                    (displayTarget, result) = Pop(displayTarget);
                }
            }

            if (result is FuncInitTarget)
                Mode = ParseMode.Regular;

            StackChanged?.Invoke(this, TargetStack.Select(t => t.Display()).ToArray());
            return displayTarget;
        }

        private (string, ICalculationTarget) Pop(string displayTarget = "")
        {
            var peekResult = TargetStack.TryPeek(out var result);
            if (peekResult && result.IsDefinitionInstance)
                throw new RuntimeException("取り出そうとしたスタックの値が組み込み定義型のため、取り出せません");

            return (displayTarget == "") ? (TargetStack.Pop().Display(), result) : (TargetStack.Pop().Display() + " " + displayTarget, result);
        }

        /// <summary>
        /// 定義済みの値以外をすべてスタックから削除します
        /// </summary>
        /// <returns></returns>
        [Command("clear", Description = "スタック内の式をすべて削除します。")]
        public string Clean()
        {
            while (TargetStack.Any() && TargetStack.TryPeek(out var result) && !result.IsDefinitionInstance)
            {
                TargetStack.Pop();
            }

            StackChanged?.Invoke(this, TargetStack.Select(t => t.Display()).ToArray());
            Mode = ParseMode.Regular;
            return DisplayStack();
        }

        /// <summary>
        /// スタックから式を取り出して計算を開始する
        /// </summary>
        [Command("run", Description = "スタック上の式を計算します。")]
        public void Run()
        {
            if (Mode == ParseMode.Definition)
                throw new RuntimeException("関数の定義式の途中です");

            TargetStack.Pop().Execute(TargetStack);
            StackChanged?.Invoke(this, TargetStack.Select(t => t.Display()).ToArray());
        }

        /// <summary>
        /// コマンド一覧を取得する
        /// </summary>
        /// <returns></returns>
        [Command("commands", Description = "コマンド一覧を取得します。")]
        public string GetAllCommand()
            => string.Join("\n", GetType().GetMethods()
                .Where(method => method.GetCustomAttribute<CommandAttribute>()?.GetCallName()?.Any() ?? false)
                .Select(method => method.GetCustomAttribute<CommandAttribute>()?.GetCallName()[0]!));

        /// <summary>
        /// スタックに積まれている式の数を取得する
        /// </summary>
        /// <returns></returns>
        [Command("count", Description = "スタックに積まれている式の数を取得します。")]
        public int GetStackCount()
            => TargetStack.Where(target => !target.IsDefinitionInstance).Count();

        /// <summary>
        /// 文字列に一致するコマンドを実行する
        /// </summary>
        /// <param name="commandStr"></param>
        /// <param name="parameters"></param>
        [Command("call", Description = "文字列でコマンドを実行する")]
        public object? CallCommand(string commandStr, object?[]? parameters)
        {
            var command = GetType().GetMethods()
                .FirstOrDefault(method => method?.GetCustomAttribute<CommandAttribute>()?.GetCallName().Contains(commandStr) ?? false);

            if (command == null) return false;

            return command.Invoke(this, parameters);
        }
    }
}