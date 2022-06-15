using System.Diagnostics.CodeAnalysis;

namespace Calculator.RPNComponents
{
    /// <summary>
    /// 逆ポーランド計算用のスタックに積むことができるクラスに実装するインターフェース
    /// </summary>
    internal interface ICalculationTarget
    {
        /// <summary>
        /// スタックに積むことができる要素の型定義を表現したクラスかどうかを表す
        /// </summary>
        bool IsDefinitionInstance { get; init; }

        /// <summary>
        /// 指定したスタックに対して処理を行う
        /// </summary>
        /// <param name="calculationTargets"></param>
        void Execute(IRPNStack calculationTargets);

        /// <summary>
        /// 画面に実際に表示する値に変換する
        /// </summary>
        /// <returns></returns>
        string Display();

        /// <summary>
        /// 与えられた式が自分と同じものか判定し、同じなら自分をインスタンス化して返す
        /// 異なる場合は、nullを返す
        /// </summary>
        /// <param name="token"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool TryParse(string token, [NotNullWhen(true)] out ICalculationTarget result);
    }
}