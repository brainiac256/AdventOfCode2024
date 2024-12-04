namespace Day3;
using Spectre.Console;
using System.Text.RegularExpressions;
class Program
{
    static void Main(string[] args)
    {
        //string tst = @"xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
        AnsiConsole.WriteLine();
        //int r = Utils.F(tst);
        using StreamReader rd = new("input.txt");
        string inp = rd.ReadToEnd();
        rd.Close();
        int acc = 0;
        foreach(string line in inp.Split("\n", StringSplitOptions.RemoveEmptyEntries))
        {
            int r = Utils.F(line);
            AnsiConsole.Write(new Text($"Line: {r}", new Style(Color.Aqua)));
            AnsiConsole.WriteLine();
            acc += r;
        }
        AnsiConsole.Write(new Text($"Total Result: {acc}", new Style(Color.Lime)));
        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Rule());
        int condr = Utils.F2(inp);
        AnsiConsole.Write(new Text($"Conditional Result: {condr}", new Style(Color.Lime)));
    }
}

public static class Utils {
    public static readonly Regex re = new Regex(@"mul\((?<X1>\d{1,3})\,(?<X2>\d{1,3})\)", RegexOptions.Compiled);
    public static readonly Regex re2 = new Regex(
        @"(?<M>mul\((?<X1>\d{1,3})\,(?<X2>\d{1,3})\))|(?<Y>do\(\))|(?<N>don't\(\))",
        RegexOptions.Compiled);
    public static int F(string input) {
        var mts = re.Matches(input);
        int acc = 0;
        foreach(Match m in mts)
        {
            bool s1 = int.TryParse(m.Groups["X1"].ValueSpan, out int x1);
            bool s2 = int.TryParse(m.Groups["X2"].ValueSpan, out int x2);
            if(s1 && s2) acc += x1 * x2;
        }
        return acc;
    }

    public static int F2(string input) {
        var mts = re2.Matches(input);
        int acc = 0;
        int mode = 1;
        foreach(Match m in mts)
        {
            if(m.Groups["N"].Success)
            {
                mode = 0;
                continue;
            }
            if(m.Groups["Y"].Success)
            {
                mode = 1;
                continue;
            }
            if(m.Groups["M"].Success)
            {
                bool s1 = int.TryParse(m.Groups["X1"].ValueSpan, out int x1);
                bool s2 = int.TryParse(m.Groups["X2"].ValueSpan, out int x2);
                if(s1 && s2) acc += x1 * x2 * mode;
            }
        }
        return acc;
    }
}
