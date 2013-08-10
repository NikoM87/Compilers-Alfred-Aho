namespace Lexer
{
    public class Float : Token
    {
        public double Value;


        public Float( int t, double value )
            : base( t )
        {
            Value = value;
        }
    }
}