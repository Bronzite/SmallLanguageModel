using SmallLanguageModels.Tokenizer;
using System.Text;

namespace SmallLanguageModelsTests
{
    
    public class UTF8TokenizerTests
    {
        private string TestSentence;
        [SetUp]
        public void Setup()
        {
            TestSentence = "The quick brown fox jumped over the lazy dog.";
        }

        [Test(Author = "John Brewer <jbrewer@haystackid.com>", Description = "Tests of UTF8Tokenizer")]
        public void UTF8TokenizerBaseByteArrayTest()
        {
            string sTestSentence = "The quick brown fox jumped over the lazy dog.";

            byte[] bData = UTF8Encoding.UTF8.GetBytes(sTestSentence);
            UTF8Tokenizer oUTF8Tokenizer = new UTF8Tokenizer();
            IEnumerable<object> oTokens = oUTF8Tokenizer.GetTokens(bData);

            List<string> strings = oTokens as List<string>;
            string[] sTokens = new string[] { "The", " ", "quick", " ", "brown", " ", "fox", " ", "jumped", " ", "over", " ", "the", " ", "lazy", " ", "dog", "." };

            Assert.NotNull(strings);
            for (int i = 0; i < strings.Count; i++)
            {
                Assert.That(strings[i], Is.EqualTo(sTokens[i]));
            }

            Assert.Pass();
        }

        [Test(Author = "John Brewer <jbrewer@haystackid.com>", Description = "Tests of UTF8Tokenizer")]
        public void UTF8TokenizerBaseMemoryStreamTest()
        {

            byte[] bData = UTF8Encoding.UTF8.GetBytes(TestSentence);
            UTF8Tokenizer oUTF8Tokenizer = new UTF8Tokenizer();
            MemoryStream ms = new MemoryStream(bData);
            IEnumerable<object> oTokens = oUTF8Tokenizer.GetTokens(ms);

            List<string> strings = oTokens as List<string>;
            string[] sTokens = new string[] { "The", " ", "quick", " ", "brown", " ", "fox", " ", "jumped", " ", "over", " ", "the", " ", "lazy", " ", "dog", "." };

            Assert.NotNull(strings);
            for (int i = 0; i < strings.Count; i++)
            {
                Assert.That(strings[i], Is.EqualTo(sTokens[i]));
            }

            Assert.Pass();
        }

        [Test(Author = "John Brewer <jbrewer@haystackid.com>", Description = "Tests of UTF8Tokenizer")]
        public void UTF8TokenizerBaseStringTest()
        {
            UTF8Tokenizer oUTF8Tokenizer = new UTF8Tokenizer();
            IEnumerable<object> oTokens = oUTF8Tokenizer.GetTokens(TestSentence);

            List<string> strings = oTokens as List<string>;
            string[] sTokens = new string[] { "The", " ", "quick", " ", "brown", " ", "fox", " ", "jumped", " ", "over", " ", "the", " ", "lazy", " ", "dog", "." };

            Assert.NotNull(strings);
            for (int i = 0; i < strings.Count; i++)
            {
                Assert.That(strings[i], Is.EqualTo(sTokens[i]));
            }

            Assert.Pass();
        }
    }
}