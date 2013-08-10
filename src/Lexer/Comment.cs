namespace Lexer
{
    public class Comment : Token
    {
        public string Value;


        public Comment( int tag, string value ) : base( tag )
        {
            Value = value;
        }
    }
}