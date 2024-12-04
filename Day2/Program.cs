namespace Day2;
using Spectre.Console;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        using StreamReader r = new("input.txt");
        var txt = r.ReadToEnd();
        r.Close();
        int safes = 0;
        int safesdamp = 0;
        foreach(string reportline in txt.Split("\n", StringSplitOptions.RemoveEmptyEntries))
        {
            var report = reportline.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s));
            bool safe = Utils.ReportSafety(report);
            bool safedamp = Utils.ReportSafetyWithDamper(report.ToList());
            AnsiConsole.Write($"  {reportline} : ");
            if(safe) {
                AnsiConsole.Markup("[Green]SAFE[/]");
                safes += 1;
            }
            else {
                AnsiConsole.Markup("[Red]UNSAFE[/]");
            }
            if(safedamp) {
                AnsiConsole.Markup(" | With Damper: [Green4]SAFE[/]");
                safesdamp += 1;
            }
            else {
                AnsiConsole.Markup(" | With Damper: [Red3]UNSAFE[/]");
            }
            AnsiConsole.WriteLine();
        }
        AnsiConsole.Write(new Rule());
        AnsiConsole.MarkupInterpolated($"  COUNT OF SAFE REPORTS: [Lime]{safes}[/]");
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupInterpolated($"  WITH DAMPER: [gold3]{safesdamp}[/]");
        AnsiConsole.WriteLine();
    }
}

public static class Utils {
    public static bool ReportSafety(IEnumerable<int> report) {
        int? lag = null;
        int? sign = null;
        foreach(int current in report) {
            if(lag is null) // current is first
            {
                lag = current;
                continue;
            }
            if(!Safe(lag.Value, current, ref sign))
                 return false;
            lag = current; // prep for next iter
        }
        return true;
    }
    public static bool ReportSafetyWithDamper(IList<int> report, bool recurse = true) {
        bool damperUsed = !recurse;
        bool fail = false;
        int? sign = null;
        LinkedList<int> q = new();
        q.AddFirst(report[0]);
        int i = 1;
        var w = q.First;
        while(i < report.Count) 
        {
            if(Safe(w?.Value, report[i], ref sign))
            {
                q.AddLast(report[i]);
                if(w is not null)
                    w = w!.Next;
                i += 1;
                continue;
            }
            else if(damperUsed)
            {
                fail = true;
                break;
            }
            else if(Safe(w?.Previous?.Value, report[i], ref sign)) // try skip last addition
            {
                w = w?.Previous;
                q.RemoveLast();
                q.AddLast(report[i]);
                w = w?.Next ?? q.Last;
                i += 1;
                damperUsed = true;
                continue;
            }
            else 
            {
                damperUsed = true;
                i += 1;
                continue;
            }
        }
        if(fail && recurse)
        {
            // kludgy. but goddammit it's 2 am and i need to go to sleep
            var r = report.Skip(1).ToList();
            var r2 = report.Skip(2).Prepend(report.First()).ToList();
            bool dumbcheck = ReportSafetyWithDamper(r, false);
            bool dumbcheck2 = ReportSafetyWithDamper(r2, false);
            if(dumbcheck)
            {
                fail = false;
                q = new LinkedList<int>(r);
            }
            else if(dumbcheck2)
            {
                fail = false;
                q = new LinkedList<int>(r2);
            }
        }
        var t = q.First;
        AnsiConsole.Write(":");
        foreach(int n in report)
        {
            AnsiConsole.Write(" ");
            if(n == t?.Value)
            {
                AnsiConsole.Write(
                    new Text($"{n}", new Style(Color.Aqua, Color.Black))
                );
                t = t.Next;
            }
            else
            {
                AnsiConsole.Write(
                    new Text($"{n}", new Style(Color.DarkCyan, Color.Black, Decoration.Strikethrough))
                );
            }
        }
        AnsiConsole.WriteLine();
        return !fail;
    }
    public static bool Safe(int? x1, int? x2, ref int? sign) {
        if(x1 is null || x2 is null) return true;
        if(x1 == x2) return false;
        if(sign is null) {
            sign = (x1 < x2) ? 1 : -1;
            return Math.Abs(x1.Value - x2.Value) < 4;
        }
        if(sign == 1) return x2 > x1 && Math.Abs(x1.Value - x2.Value) < 4;
        else return x2 < x1 && Math.Abs(x1.Value - x2.Value) < 4;
    }
}