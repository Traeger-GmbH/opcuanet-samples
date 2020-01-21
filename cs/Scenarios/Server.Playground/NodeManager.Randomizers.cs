// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Server.Playground
{
    using System;
    using System.Text;

    internal partial class NodeManager
    {
        #region ---------- Private methods ----------

        private T RandomEnum<T>()
        {
            return this.RandomEnum<T>(old: default(T), random: 3);
        }

        private T RandomEnum<T>(T old, int random)
        {
            var values = Enum.GetValues(typeof(T));

            if (typeof(T).IsDefined(typeof(FlagsAttribute), inherit: false)) {
                var value = Math.Min(random, Math.Pow(2, values.Length - 1));
                var baseValue = Convert.ChangeType(value, Enum.GetUnderlyingType(typeof(T)));

                return (T)Enum.ToObject(typeof(T), baseValue);
            }

            var newValue = DateTime.Now.Second * random % values.Length;

            if (Enum.IsDefined(typeof(T), newValue))
                return (T)Enum.ToObject(typeof(T), newValue);

            return old;
        }

        public string RandomString()
        {
            return this.RandomString(old: string.Empty, random: 3);
        }

        public string RandomString(string old, int random)
        {
            int minWords = 1;
            int maxWords = random;
            int minSentences = 1;
            int maxSentences = 1;
            int paragraphCount = 1;

            var words = new[] {
                "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
                "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
                "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"
            };

            int sentenceCount = this.valueRandom.Next(Math.Max(maxSentences - minSentences, 1)) + minSentences + 1;
            int wordCount = this.valueRandom.Next(Math.Max(maxWords - minWords, 1)) + minWords + 1;

            var result = new StringBuilder();

            for (int paragraphIndex = 0; paragraphIndex < paragraphCount; paragraphIndex++) {
                for (int sentenceIndex = 0; sentenceIndex < sentenceCount; sentenceIndex++) {
                    for (int wordIndex = 0; wordIndex < wordCount; wordIndex++) {
                        if (wordIndex > 0)
                            result.Append(" ");

                        var word = words[this.indexRandom.Next(words.Length)];

                        if (wordIndex == 0)
                            word = char.ToUpper(word[0]) + word.Substring(1);

                        result.Append(word);
                    }

                    result.Append(".");

                    if (sentenceIndex + 1 < sentenceCount)
                        result.Append(" ");
                }

                if (paragraphIndex + 1 < paragraphCount)
                    result.Append(Environment.NewLine);
            }

            return result.ToString();
        }

        #endregion
    }
}
