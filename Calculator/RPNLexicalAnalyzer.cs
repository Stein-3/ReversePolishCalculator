namespace Calculator
{
    internal static class RPNLexicalAnalyzer
    {
        /// <summary>
        /// RPN式を個々の要素にパースする
        /// </summary>
        /// <param name="formula">RPN文字列式</param>
        internal static IEnumerable<string> Analyze(string formula)
            => formula.Split(' ');
    }
}