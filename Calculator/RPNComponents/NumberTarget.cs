using System.Diagnostics.CodeAnalysis;

namespace Calculator.RPNComponents
{
    /// <summary>
    /// 数値全般を表すクラス
    /// </summary>
    internal class NumberTarget : ICalculationTarget
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="denominator">分母</param>
        /// <param name="numerator">分子</param>
        public NumberTarget(double denominator, double numerator)
        {
            Denominator = denominator;
            Numerator = numerator;
        }

        public NumberTarget(bool isDefinitionInstance) => IsDefinitionInstance = isDefinitionInstance;

        internal static NumberTarget DefinitionInstance => new NumberTarget(true);

        /// <summary>
        /// 分母
        /// nullの場合は定義インスタンスのため、処理は行わない
        /// </summary>
        public double Denominator { get; }

        /// <summary>
        /// 分子
        /// nullの場合は定義インスタンスのため、処理は行わない
        /// </summary>
        public double Numerator { get; }

        public bool IsDefinitionInstance { get; init; } = false;

        /// <summary>
        /// 指定したスタックにこのクラスのインスタンスをプッシュする
        /// </summary>
        /// <param name="stackTarget">操作するスタック</param>
        public void Execute(IRPNStack stackTarget)
            => stackTarget.Push(this);

        /// <summary>
        /// 少数点表示で数値の文字列を表示する
        /// </summary>
        /// <returns></returns>
        public string DisplayWithDecimalShape()
            => (IsDefinitionInstance || Denominator is double.NaN || Numerator is double.NaN) ? "" : ToDouble().ToString();

        /// <summary>
        /// 分数表示で数値の文字列を表示する
        /// </summary>
        /// <returns></returns>
        public string DisplayWithFractionShape()
            => (IsDefinitionInstance || Denominator is double.NaN || Numerator is double.NaN) ? "" :
                (Denominator == 1) ? Numerator.ToString() : $"{Numerator}/{Denominator}";

        /// <summary>
        /// 実際の画面に表示する形式を出力する
        /// </summary>
        /// <returns></returns>
        public string Display()
            => DisplayWithFractionShape();

        /// <summary>
        /// 文字列が数値形式の場合は、インスタンスを作成し返す
        /// </summary>
        /// <param name="token"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryParse(string token, [NotNullWhen(true)] out ICalculationTarget result)
        {
            if (double.TryParse(token, out var d))
            {
                result = new NumberTarget(1, d);
                return true;
            }

            var fraction = token.Split("/");
            if (fraction.Length == 2)
            {
                var numeratorTryParseResult = double.TryParse(fraction[0], out var numerator);
                var denominatorTryParseResult = double.TryParse(fraction[1], out var denominator);

                if (numeratorTryParseResult && denominatorTryParseResult)
                {
                    result = new NumberTarget(numerator, denominator);
                    return true;
                }
            }

            result = default!;
            return false;
        }

        /// <summary>
        /// doubleに変換します
        /// </summary>
        /// <returns></returns>
        internal double ToDouble() => Numerator / Denominator;
    }
}