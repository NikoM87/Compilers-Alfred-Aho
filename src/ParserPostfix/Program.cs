using System;


namespace ParserPostfix
{
    class Program
    {
        static void Main()
        {
            var parse = new Parser();
            parse.Expression();
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
