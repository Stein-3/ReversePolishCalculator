using Calculator.RPNException;
using System.Diagnostics.CodeAnalysis;

namespace Calculator.RPNComponents.Function
{
    internal class FuncNameTarget : ICalculationTarget
    {
        public bool IsDefinitionInstance { get; init; } = false;

        /// <summary>
        /// 関数定義の要素かどうかを表す
        /// 関数定義の要素の場合はTrue, 関数実行用の場合はFalse
        /// </summary>
        public readonly bool IsFuncDefinitionInstance;

        private string Name { get; set; } = "";

        public FuncNameTarget(bool isDefinitionInstance, bool isFuncDefinitionInstance = false)
        {
            IsDefinitionInstance = isDefinitionInstance;
            IsFuncDefinitionInstance = isFuncDefinitionInstance;
        }

        public FuncNameTarget(string name, bool isFuncDefinitionInstance)
        {
            Name = name;
            IsFuncDefinitionInstance = isFuncDefinitionInstance;
        }

        internal static FuncNameTarget DefinitionInstance => new(true, false);

        public string Display() => IsDefinitionInstance ? "" : Name;

        public void Execute(IRPNStack calculationTargets)
        {
            var arrayCalcTarget = calculationTargets.ToArray();

            var argCount = 0;
            var funcContent = new List<ICalculationTarget>();

            for (var i = arrayCalcTarget.Length - 1; i > -1; i--)
            {
                if (arrayCalcTarget[i] is not FunctionTarget funcTarget || funcTarget.IsDefinitionInstance)
                    continue;

                if (arrayCalcTarget[i - 1] is not FuncNameTarget funcNameTarget)
                    throw new RuntimeException("関数定義が不正です。");
                else if (funcNameTarget.Name != Name)
                    continue;

                if (arrayCalcTarget[i - 2] is not NumberTarget target || !int.TryParse(target.Display(), out var parseResult))
                    throw new RuntimeException("関数定義が不正です。");
                else
                    argCount = parseResult;

                for (var i2 = i - 3; arrayCalcTarget[i2] is not FuncInitTarget; i2--)
                {
                    funcContent.Add(arrayCalcTarget[i2]);
                }

                break;
            }

            var popedTarget = new List<ICalculationTarget>();
            for (var i3 = 0; i3 < argCount; i3++)
            {
                if (!calculationTargets.TryPeek(out var item) || item is FunctionTarget)
                    throw new RuntimeException("引数の中に関数定義を含むことはできません。");

                popedTarget.Add(calculationTargets.Pop());
            }

            var argIndex = funcContent.FindIndex(f => f is FuncArgTarget);

            while (argIndex > -1)
            {
                if (funcContent[argIndex] is not FuncArgTarget popTarget)
                    throw new RuntimeException("不明なエラー");

                var popTargetIndex = popTarget.ArgumentNumber - 1;

                if (popTargetIndex > funcContent.Count)
                    throw new RuntimeException($"関数に指定した引数の数より大きいインデックスの引数が存在します「${popTargetIndex}」");

                funcContent[argIndex] = popedTarget[popTargetIndex];

                argIndex = funcContent.FindIndex(f => f is FuncArgTarget);
            }

            funcContent.Reverse();
            foreach (var target in funcContent)
            {
                calculationTargets.Push(target);
            }

            calculationTargets.Pop().Execute(calculationTargets);
        }

        public bool TryParse(string token, [NotNullWhen(true)] out ICalculationTarget result)
        {
            result = new FuncNameTarget(token, (Calculator.Mode == ParseMode.Definition));

            return true;
        }
    }
}