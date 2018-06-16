namespace Woof.DebugEx {

    /// <summary>
    /// String extensions for debugging purpose.
    /// </summary>
    public static class StringExtensions {

        /// <summary>
        /// Replaces whitespace with visible characters.
        /// </summary>
        /// <param name="text">Input text.</param>
        /// <returns>Text with all whitespace made visible.</returns>
        public static string WhitespaceVisible(this string text) => text.Replace('\r', '←').Replace('\n', '↓').Replace(' ', '·').Replace('\t', '→');

    }

}