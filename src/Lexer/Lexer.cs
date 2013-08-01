using System;
using System.Collections;
using System.IO;
using System.Text;


namespace Lexer
{
    public class Lexer
    {
        public int Line = 1;
        private char _peek = ' ';
        private readonly Hashtable _words = new Hashtable();


        private void Reserve( Word word )
        {
            _words.Add( word.Lexeme, word );
        }


        public Lexer()
        {
            Reserve( new Word( Tag.True, "true" ) );
            Reserve( new Word( Tag.False, "false" ) );
        }


        public Token Scan( StringReader reader )
        {
            for ( ;; _peek = (char) reader.Read() )
            {
                if ( _peek == ' ' || _peek == '\t' )
                    continue;

                if ( _peek == '\n' )
                    Line += 1;
                else
                    break;
            }
            if ( Char.IsDigit( _peek ) )
            {
                int val = 0;
                do
                {
                    val = 10*val + int.Parse( new string( _peek, 1 ) );
                    _peek = (char) reader.Read();
                } while( Char.IsDigit( _peek ) );

                return new Num( Tag.Num, val );
            }

            if ( Char.IsLetter( _peek ) )
            {
                var buf = new StringBuilder();
                do
                {
                    buf.Append( _peek );
                    _peek = (char) reader.Read();
                } while( Char.IsLetterOrDigit( _peek ) );
                string s = buf.ToString();
                var w = (Word) _words[s];
                if ( w != null )
                    return w;
                w = new Word( Tag.Id, s );
                _words.Add( s, w );
                return w;
            }
            var t = new Token( _peek );
            _peek = ' ';
            return t;
        }
    }
}