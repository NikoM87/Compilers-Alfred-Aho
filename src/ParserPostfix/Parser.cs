using System;


namespace ParserPostfix
{
    internal class Parser
    {
        private static int _lookahead;


        public Parser()
        {
            _lookahead = Console.Read();
        }


        public void Expression()
        {
            Terminal();
            while( true )
            {
                switch ( _lookahead )
                {
                    case '+':
                        Match( '+' );
                        Terminal();
                        Console.Write( '+' );
                        break;
                    case '-':
                        Match( '-' );
                        Terminal();
                        Console.Write( '-' );
                        break;
                    default:
                        return;
                }
            }
        }


        private static void Terminal()
        {
            if ( Char.IsDigit( (char) _lookahead ) )
            {
                Console.Write( (char) _lookahead );
                Match( _lookahead );
            }
            else
                throw new Exception( "Syntax error" );
        }


        private static void Match( int t )
        {
            if ( _lookahead == t )
                _lookahead = Console.Read();
            else
                throw new Exception( "Syntax error" );
        }
    }
}