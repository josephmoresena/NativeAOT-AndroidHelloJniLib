using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;


namespace utils{
    public class ConsoleLog{
        public static void e(string msg  , [CallerMemberName] string memberName = "",
        [CallerFilePath]   string   sourceFilePath   = "",
        [CallerLineNumber] int      sourceLineNumber = 0){
            
            StackTrace stackTrace = new StackTrace();
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < stackTrace.FrameCount; i++)
            {
                StackFrame frame = stackTrace.GetFrame(i);
                sb.AppendLine($"    {frame.GetFileName()}#{frame.GetMethod()}(line:{frame.GetFileLineNumber()})");
            }
            
            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            string line = sourceFilePath.Substring(
                                                   sourceFilePath.LastIndexOf(Path.DirectorySeparatorChar)+1);
            var id = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"[Error][{id}][{DateTime.Now:MM/dd HH:mm:ss:fff}][{line}#{memberName}(line:{sourceLineNumber})]    {msg} ")
                            ;
            Console.WriteLine($"Stack:");
            Console.WriteLine($"{sb}");
            Console.ForegroundColor = old;

        }

        public static void d(string  msg, [CallerMemberName] string memberName = "",
                             [CallerFilePath]   string sourceFilePath   = "",
                             [CallerLineNumber] int    sourceLineNumber = 0){

            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            string line = sourceFilePath.Substring(
                                     sourceFilePath.LastIndexOf(Path.DirectorySeparatorChar)+1);
            var id = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"[Debug][{id}][{DateTime.Now:MM/dd HH:mm:ss:fff}][{line}#{memberName}(line:{sourceLineNumber})]    {msg} "
                            );
            Console.ForegroundColor = old;
        }
    }
}