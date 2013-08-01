using System;


namespace Lexer
{
    public class Token
    {
        public readonly int Tag;


        public Token( int tag )
        {
            Tag = tag;
        }


        public override string ToString()
        {
            return new string( (char)Tag, 1 );
        }
    }
}
