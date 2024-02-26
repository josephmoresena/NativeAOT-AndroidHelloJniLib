using System.Runtime.InteropServices;

namespace HelloJniLib;
//android logcat api , export from android log library(-llog)
public static class Log {
    public static string tag = "csharp";
    public const int ANDROID_LOG_VERBOSE = 2;
    public const int ANDROID_LOG_DEBUG = 3;
    public const int ANDROID_LOG_INFO = 4;
    public const int ANDROID_LOG_WARN = 5;
    public const int ANDROID_LOG_ERROR = 6;
    public const int ANDROID_LOG_FATAL = 7;
    public const int ANDROID_LOG_SILENT = 8;
    [DllImport("log",EntryPoint="__android_log_print")]
    public static extern void log(int level, string tag, string msg , params string[] args);
    public static void d(string msg , params string[] args) =>log(ANDROID_LOG_DEBUG,tag,msg,args);
    public static void w(string msg , params string[] args) =>log(ANDROID_LOG_WARN,tag,msg,args);
    public static void e(string msg , params string[] args) =>log(ANDROID_LOG_ERROR,tag,msg,args);
    
}
