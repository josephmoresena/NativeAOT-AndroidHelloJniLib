using System.Runtime.InteropServices;

namespace HelloJniLib;

public class Program {
    public static void Main(string[] args) {
        string helloworld = GetRuntimeInformation();
        Console.WriteLine($"Welcome Android \n {helloworld}");
        
    }
    
    private static String GetRuntimeInformation()
        => GetRuntimeReflectionInformation();

    private static String GetRuntimeReflectionInformation() {
        return $"Framework Version: {Environment.Version}" + Environment.NewLine
            + $"Runtime Name: {RuntimeInformation.FrameworkDescription}" + Environment.NewLine
            + $"Runtime Path: {RuntimeEnvironment.GetRuntimeDirectory()}" + Environment.NewLine
            + $"Runtime Version: {RuntimeEnvironment.GetSystemVersion()}" + Environment.NewLine + Environment.NewLine;
    }
}
