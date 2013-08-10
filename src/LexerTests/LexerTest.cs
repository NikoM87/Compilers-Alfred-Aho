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
        public void TestScanFloatNum()
        {
            var lex = new Lexer.Lexer();
            var reader = new StringReader( "2. 3.14 .5" );

            var t = lex.Scan( reader );

            Assert.AreEqual( typeof (Float), t.GetType() );
            Assert.AreEqual( 2.0, ( (Float) t ).Value );

            t = lex.Scan( reader );

            Assert.AreEqual( typeof (Float), t.GetType() );
            Assert.AreEqual( 3.14, ( (Float) t ).Value );

            t = lex.Scan( reader );

            Assert.AreEqual( typeof (Float), t.GetType() );
            Assert.AreEqual( 0.5, ( (Float) t ).Value );
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


        [TestMethod]
        public void TestScanComment()
        {
            var lex = new Lexer.Lexer();
            var reader = new StringReader( "// Comment\n" );

            var t = lex.Scan( reader );

            Assert.AreEqual( typeof (Comment), t.GetType() );
            Assert.AreEqual( " Comment", ( (Comment) t ).Value );
        }

        [TestMethod]
        public void TestScanMultilineComment()
        {
            const string textComment = "Comment line 1\n" +
                                       "Comment line 2\n" +
                                       "Comment line *";
            var lex = new Lexer.Lexer();
            var reader = new StringReader( "/*" + textComment + "*/" );

            var t = lex.Scan( reader );

            Assert.AreEqual( typeof( Comment ), t.GetType() );
            Assert.AreEqual( textComment, ( (Comment)t ).Value );
        }

        [TestMethod]
        public void TestScanCompare()
        {
            var lex = new Lexer.Lexer();
            var reader = new StringReader( " < <= == != >= >" );

            var t = lex.Scan( reader );

            Assert.AreEqual( typeof( Word), t.GetType() );
            Assert.AreEqual( Tag.Less, t.Tag );

            t = lex.Scan( reader );

            Assert.AreEqual( typeof( Word ), t.GetType() );
            Assert.AreEqual( Tag.LessOrEqual, t.Tag );

            t = lex.Scan( reader );

            Assert.AreEqual( typeof( Word ), t.GetType() );
            Assert.AreEqual( Tag.Equal, t.Tag );

            t = lex.Scan( reader );

            Assert.AreEqual( typeof( Word ), t.GetType() );
            Assert.AreEqual( Tag.NotEqual, t.Tag );

            t = lex.Scan( reader );

            Assert.AreEqual( typeof( Word ), t.GetType() );
            Assert.AreEqual( Tag.BetterOrEqual, t.Tag );

            t = lex.Scan( reader );

            Assert.AreEqual( typeof( Word ), t.GetType() );
            Assert.AreEqual( Tag.Better, t.Tag );
        }
    }
}