using System;

namespace GitGooWee
{
    class Program
    {
        static void Main(string[] args)
        {
            var res = Terminal.Send(GitCommands.GetBranchs);
            Console.WriteLine(Terminal.Send(GitCommands.GetBranchs));
            Console.ReadKey();
        }
    }
}
