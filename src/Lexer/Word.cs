namespace Lexer
{
    public class Word : Token
    {
        public string Lexeme;


        public Word( int tag, string lexeme )
            : base( tag )
        {
            Lexeme = lexeme;
        }
    }
}