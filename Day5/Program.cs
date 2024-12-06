using System.Text;

namespace Day5;

class Program
{
    static void Main(string[] args)
    {
        string input = @"47|53
97|13
97|61
97|47
75|29
61|13
75|53
29|13
97|29
53|29
61|53
97|53
61|29
47|13
75|47
97|75
47|61
75|61
47|29
75|13
53|13

75,47,61,53,29
97,61,53,29,13
75,29,13
75,97,47,61,53
61,13,29
97,13,75,29,47";
        using StreamReader r = new("input.txt");
        input = r.ReadToEnd();

        var split = input.Split("\n");
        var rulestext = split.TakeWhile(line => !line.Trim().Equals(string.Empty)).ToList();
        int num_of_rules = rulestext.Count;
        var books = split.Skip(num_of_rules + 1).ToList();

        Dictionary<string, HashSet<string>> rules = new(); // key = before, value = all afters
        //build rules dict
        foreach(string line in rulestext)
        {
            var pair = line.Trim().Split("|");
            if(rules.ContainsKey(pair[0]))
            {
                rules[pair[0]].Add(pair[1]);
            }
            else
            {
                rules.Add(pair[0], new([pair[1]]));
            }
        }
        // int result = SolvePart1(rules,books);
        // Console.WriteLine($"===\n  Day 5 Part 1: {result}\n===");
        int result = SolvePart2(rules,books);
        Console.WriteLine($"===\n  Day 5 Part 2: {result}\n===");
    }

    private static int SolvePart1(Dictionary<string, HashSet<string>> rules, IEnumerable<string> books){
        int acc = 0;
        foreach(string book in books)
        {
            if(book.Trim().Equals(string.Empty)) continue;
            var pages = book.Trim().Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
            int middleIndex = (pages.Count - 1) / 2; 
            // if there are five elements then the middle is index=2, etc
            if(ValidateBook(rules, pages)){
                acc += int.Parse(pages[middleIndex]);
            } 
        }
        return acc;
    }
    private static bool ValidateBook(Dictionary<string, HashSet<string>> rules, IList<string> pages)
    {
        HashSet<string> afters = new([pages.Last()]);
        int bookLength = pages.Count;
        for(int i = bookLength - 2; i >= 0; i--)
        {
            string before = pages[i];
            foreach(string after in afters)
            {
                if(rules.ContainsKey(after))
                {
                    if(rules[after].Contains(before)) return false;
                }
            }
            afters.Add(before);
        }
        return true;
    }
    private static int SolvePart2(Dictionary<string, HashSet<string>> rules, IEnumerable<string> books){
        int acc = 0;
        ElvenPageSorter s = new(rules);
        foreach(string book in books)
        {
            if(book.Trim().Equals(string.Empty)) continue;
            var pages = book.Trim().Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
            int middleIndex = (pages.Count - 1) / 2; 
            // if there are five elements then the middle is index=2, etc
            if(!ValidateBook(rules, pages)){
                //re-order pages to be valid
                Console.WriteLine($"{book.Trim()} - invalid");
                var newOrder = pages.OrderBy(page => page, s).ToList();
                if(ValidateBook(rules, newOrder))
                    acc += int.Parse(newOrder[middleIndex]);
                else
                    Console.WriteLine("Whoops! That didn't fucking work!");
            } 
        }
        return acc;
    }

    private class ElvenPageSorter : IComparer<string>
    {
        private readonly Dictionary<string, HashSet<string>> rules;
        public ElvenPageSorter(Dictionary<string, HashSet<string>> the_rules)
        {
            rules = the_rules;
        }
        public int Compare(string? x, string? y)
        {
            if(x is null || y is null) return 0;
            if(rules.ContainsKey(x) && rules[x].Contains(y)) return -1;
            if(rules.ContainsKey(y) && rules[y].Contains(x)) return 1;
            return 0;
        }
    }
}
