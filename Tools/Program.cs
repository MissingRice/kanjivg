namespace KVGTools;

public static class Program {
    private const bool CanWrite = true;

    public static void Main(string[] args) {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        FixKaishoNumbers.Execute(int.Parse(args[0]), CanWrite);
    }
}
