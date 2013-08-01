namespace Lexer
{
    public class Num : Token
    {
        public int Value;


        public Num( int t, int value )
            : base( t )
        {
            Value = value;
        }
    }
}