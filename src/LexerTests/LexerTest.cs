using System.IO;

using Lexer;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace LexerTests
{
    [TestClass]
    public class LexerTest
    {
        [TestMethod]
        public void TestScanNum()
        {
            var lex = new Lexer.Lexer();
            var reader = new StringReader( "1234567890" );

            var t = lex.Scan( reader );
            Assert.AreEqual( typeof (Num), t.GetType() );
            Assert.AreEqual( 1234567890, ( (Num) t ).Value );
        }

        [TestMethod]
        public void TestScanWord()
        {
            var lex = new Lexer.Lexer();
            var reader = new StringReader( "abc2" );

            var t = lex.Scan( reader );
            Assert.AreEqual( typeof( Word ), t.GetType() );
            Assert.AreEqual( "abc2", ( (Word)t ).Lexeme );
        }
    }
}