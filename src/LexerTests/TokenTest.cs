using Lexer;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace LexerTests
{
    [TestClass]
    public class TokenTest
    {
        [TestMethod]
        public void TestCreateToken()
        {
            var t = new Token( '+' );

            Assert.AreEqual( '+', t.Tag );
        }


        [TestMethod]
        public void TestTokenToString()
        {
            var t = new Token( '+' );
            string s = t.ToString();

            Assert.AreEqual( "+", s );
        }


        [TestMethod]
        public void TestCreateWord()
        {
            var w = new Word( Tag.True, "true" );

            Assert.AreEqual( Tag.True, w.Tag );
            Assert.AreEqual( "true", w.Lexeme );
        }


        [TestMethod]
        public void TestCreateNum()
        {
            var n = new Num( Tag.Num, 54321 );

            Assert.AreEqual( Tag.Num, n.Tag );
            Assert.AreEqual( 54321, n.Value );
        }
    }
}