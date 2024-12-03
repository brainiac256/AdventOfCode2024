namespace AOCDay1;

using System.Collections.Generic;
using Spectre.Console;

class Program
{
    static void Main(string[] args)
    {
        //AnsiConsole.Clear();
        // test
        List<int> test1 = new List<int>() { 3,4,2,1,3,3};
        List<int> test2 = new List<int>() { 4,3,5,3,9,3};
        //GetDistance(test1, test2);
        //GetSimilarity(test1, test2);
        //return;
        using StreamReader r = new(@"..\..\..\input.txt");
        var txt = r.ReadToEnd().Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Split(" ", StringSplitOptions.RemoveEmptyEntries)).ToList();
        
        List<int> inp1 = new();
        List<int> inp2 = new();

        txt.ForEach(p => {
            if(p.Count() != 2) return;
            int.TryParse(p[0], out int i1);
            int.TryParse(p[1], out int i2);
            inp1.Add(i1);
            inp2.Add(i2);
        });
        GetSimilarity(inp1, inp2);
    }

    static int GetDistance(IEnumerable<int> list1, IEnumerable<int> list2) 
    {
        if(list1 is IList<int> l1 && list2 is IList<int> l2) {
            if(l1.Count != l2.Count) throw new InvalidOperationException(
                $"{nameof(GetDistance)}: sets supplied must be of the same size ({l1.Count} != {l2.Count})");
        }
        var sort1 = list1.Order();
        var sort2 = list2.Order();
        if(sort1.Count() != sort2.Count()) throw new InvalidOperationException(
            $"{nameof(GetDistance)}: sets supplied must be of the same size ({sort1.Count()} != {sort2.Count()})");
        var dbo = new Spectre.Console.Table();
        dbo.AddColumn("Sort1");
        dbo.AddColumn("Sort2");
        dbo.AddColumn("Distance");
        List<int> distances = new List<int>();
        int totald = 0;
        foreach((int x, int y) in sort1.Zip(sort2) ) {
            int d = Math.Abs(x-y);
            totald += d;
            dbo.AddRow(new List<int>() {x,y,d}.Select(i => new Text(i.ToString(), new Style(Color.SkyBlue1))));
        }
        dbo.Caption($"Total distance: {totald}", new Style(Color.Red));
        AnsiConsole.Write(dbo);
        return totald;
    }

    static int GetSimilarity(IEnumerable<int> list1, IEnumerable<int> list2) {
        Dictionary<int, int> sort2 = new();
        foreach(int i in list2) {
            if(!sort2.ContainsKey(i))
                sort2.Add(i,1);
            else
                sort2[i] = sort2[i]+1;
        }
        var dbo = new Spectre.Console.Table();
        dbo.AddColumn("List1");
        dbo.AddColumn("Similarity");
        List<int> sim = new List<int>();
        int totalsim = 0;
        foreach(int i in list1) {
            int s = 0;
            if(sort2.ContainsKey(i)) {
                s = i * sort2[i];
            }
            sim.Add(s);
            totalsim += s;
            dbo.AddRow($"{i}", $"{s}");
        }
        dbo.Caption($"Total similarity: {totalsim}");
        AnsiConsole.Write(dbo);
        return totalsim;
    }
}
