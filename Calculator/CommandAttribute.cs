using System.Globalization;

namespace Calculator
{
    /// <summary>
    /// コマンドとして使用するメソッドの説明
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute : Attribute
    {
        private readonly string Name;

        /// <summary>
        /// コマンドの説明
        /// </summary>
        public string Description;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name"></param>
        public CommandAttribute(string name)
        {
            Name = name;
            Description = "";
        }

        /// <summary>
        /// コマンド文字列を取得する
        /// </summary>
        /// <returns></returns>
        public string[] GetCallName()
            => new string[]
            {
                Name,
                CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Name)
            };
    }
}