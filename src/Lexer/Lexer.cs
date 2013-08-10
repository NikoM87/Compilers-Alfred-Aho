using System;
using System.Collections;
using System.Globalization;
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
                var buf = new StringBuilder();
                do
                {
                    buf.Append( _peek );
                    _peek = (char) reader.Read();
                } while( Char.IsDigit( _peek ) );

                if ( _peek == '.' )
                {
                    buf.Append( _peek );
                    _peek = (char) reader.Read();

                    while( Char.IsDigit( _peek ) )
                    {
                        buf.Append( _peek );
                        _peek = (char)reader.Read();
                    }

                    CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                    ci.NumberFormat.CurrencyDecimalSeparator = ".";

                    double d = double.Parse( buf.ToString(), NumberStyles.Any, ci );

                    return new Float( Tag.Float, d );
                }


                return new Num( Tag.Num, int.Parse( buf.ToString() ) );
            }

            if ( _peek == '.' )
            {
                var buf = new StringBuilder();

                buf.Append( _peek );
                _peek = (char)reader.Read();

                while ( Char.IsDigit( _peek ) )
                {
                    buf.Append( _peek );
                    _peek = (char)reader.Read();
                }

                CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                ci.NumberFormat.CurrencyDecimalSeparator = ".";

                double d = double.Parse( buf.ToString(), NumberStyles.Any, ci );

                return new Float( Tag.Float, d );
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

            if ( _peek == '/' )
            {
                _peek = (char) reader.Read();
                if ( _peek == '/' )
                {
                    _peek = (char) reader.Read();
                    var buf = new StringBuilder();
                    do
                    {
                        buf.Append( _peek );
                        _peek = (char) reader.Read();
                    } while( _peek != '\n' );

                    return new Comment( Tag.Comment, buf.ToString() );
                }

                if ( _peek == '*' )
                {
                    var buf = new StringBuilder();

                    _peek = (char) reader.Read();
                                       
                    while( true )
                    {
                        if ( _peek == '*' )
                        {
                            _peek = (char) reader.Read();
                            if ( _peek == '/' )
                            {
                                return new Comment( Tag.Comment, buf.ToString() );
                            }

                            buf.Append( '*' );
                        }
                        else
                        {
                            buf.Append( _peek );
                            _peek = (char) reader.Read();
                        }
                    }
                }
            }

            if ( _peek == '<' )
            {
                var buf = new StringBuilder();
                buf.Append( _peek );
                _peek = (char) reader.Read();

                if ( _peek == '=' )
                {
                    buf.Append( _peek );
                    _peek = (char) reader.Read();

                    return new Word( Tag.LessOrEqual, buf.ToString() );
                }

                return new Word( Tag.Less, buf.ToString() );
            }

            if ( _peek == '>' )
            {
                var buf = new StringBuilder();
                buf.Append( _peek );
                _peek = (char) reader.Read();

                if ( _peek == '=' )
                {
                    buf.Append( _peek );
                    _peek = (char) reader.Read();

                    return new Word( Tag.BetterOrEqual, buf.ToString() );
                }

                return new Word( Tag.Better, buf.ToString() );
            }

            if ( _peek == '=' )
            {
                var buf = new StringBuilder();
                buf.Append( _peek );
                _peek = (char) reader.Read();

                if ( _peek == '=' )
                {
                    buf.Append( _peek );
                    _peek = (char) reader.Read();

                    return new Word( Tag.Equal, buf.ToString() );
                }
            }

            if ( _peek == '!' )
            {
                var buf = new StringBuilder();
                buf.Append( _peek );
                _peek = (char) reader.Read();

                if ( _peek == '=' )
                {
                    buf.Append( _peek );
                    _peek = (char) reader.Read();

                    return new Word( Tag.NotEqual, buf.ToString() );
                }
            }

            var t = new Token( _peek );
            _peek = ' ';
            return t;
        }
    }
}