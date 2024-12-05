namespace Day4;
using Spectre.Console;
class Program
{
    static void Main(string[] args)
    {
        string test = @"MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX";
        //Xword puzzle = new Xword(test, true);

        using StreamReader r = new("input.txt");
        string p = r.ReadToEnd();
        Xword puzzle = new Xword(p, false); // change slowmode to true to watch the whole thing, SLOWLY.
        var d = AnsiConsole.Live(new Text("Starting...")).AutoClear(false);
        AnsiConsole.Clear();
        Task res = d.StartAsync(puzzle.SolvePt2);
        res.Wait();
        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Rule());
        AnsiConsole.Write(new Text($"  DONE.  {puzzle.Total}", new Style(Color.Yellow)));
    }
}

public class Xword {
    private bool _slow;
    private List<Tuple<char,Color>[]> letterGrid;
    private int width;
    private int height;
    private int? _total;
    public bool Success {get; private set;}
    public int? Total { get => Success? _total!.Value : null;}

    public Xword(string originalGrid, bool slowmode){
        letterGrid = originalGrid.Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Trim().Select(spot => new Tuple<char,Color>(spot, Color.Grey)).ToArray()).ToList();
        width = letterGrid[0].Length;
        height = letterGrid.Count;
        Success = false;
        _slow = slowmode;
    }
    private char At(int x, int y) {
        if(x < 0 || y < 0 || x >= height || y >= width) return ' ';
        return letterGrid[x][y].Item1;
    }
    private Color? ColorAt(int x, int y) {
        if(x < 0 || y < 0 || x >= height || y >= width) return null;
        return letterGrid[x][y].Item2;
    }
    private void SetColor(int x, int y, Color clr) {
        char c = At(x,y);
        if(c == ' ') return;
        letterGrid[x][y] = new Tuple<char, Color>(c, clr);
    }
    private void SetPosition(int x, int y, char newChar, Color newClr) {
        char c = At(x,y);
        if(c == ' ') return;
        letterGrid[x][y] = new Tuple<char, Color>(newChar, newClr);
    }
    public async Task SolvePt1(LiveDisplayContext ctx) {
        _total = null;
        Success = false;
        if(_slow) Draw(ctx);
        if(_slow) await Task.Delay(50);
        for(int x = 0; x < height; x++) {
            for(int y = 0; y < width; y++) {
                if(At(x,y) == 'X') SetColor(x,y,Color.Aqua);
            }
        }
        Draw(ctx);
        if(_slow) await Task.Delay(50);
        _total = 0;
        for(int x = 0; x < height; x++) {
            for(int y = 0; y < width; y++) {
                char c = At(x,y);
                if(c != 'X') continue;
                SetColor(x,y, Color.Red);
                if(_slow) Draw(ctx);
                if(_slow) await Task.Delay(50);
                SetColor(x,y, Color.Grey);
                
                // check for XMAS in all eight directions
                if(At(x+1,y) == 'M' && At(x+2,y) == 'A' && At(x+3,y) == 'S') {
                    SetColor(x,y, Color.Lime);
                    SetColor(x+1,y, Color.DarkGreen);
                    SetColor(x+2,y, Color.DarkGreen);
                    SetColor(x+3,y, Color.DarkGreen);
                    _total += 1;
                }
                if(At(x-1,y) == 'M' && At(x-2,y) == 'A' && At(x-3,y) == 'S') {
                    SetColor(x,y, Color.Lime);
                    SetColor(x-1,y, Color.DarkGreen);
                    SetColor(x-2,y, Color.DarkGreen);
                    SetColor(x-3,y, Color.DarkGreen);
                    _total += 1;
                }
                if(At(x,y+1) == 'M' && At(x,y+2) == 'A' && At(x,y+3) == 'S') {
                    SetColor(x,y, Color.Lime);
                    SetColor(x,y+1, Color.DarkGreen);
                    SetColor(x,y+2, Color.DarkGreen);
                    SetColor(x,y+3, Color.DarkGreen);
                    _total += 1;
                }
                if(At(x,y-1) == 'M' && At(x,y-2) == 'A' && At(x,y-3) == 'S') {
                    SetColor(x,y, Color.Lime);
                    SetColor(x,y-1, Color.DarkGreen);
                    SetColor(x,y-2, Color.DarkGreen);
                    SetColor(x,y-3, Color.DarkGreen);
                    _total += 1;
                }
                if(At(x+1,y+1) == 'M' && At(x+2,y+2) == 'A' && At(x+3,y+3) == 'S') {
                    SetColor(x,y, Color.Lime);
                    SetColor(x+1,y+1, Color.DarkGreen);
                    SetColor(x+2,y+2, Color.DarkGreen);
                    SetColor(x+3,y+3, Color.DarkGreen);
                    _total += 1;
                }
                if(At(x+1,y-1) == 'M' && At(x+2,y-2) == 'A' && At(x+3,y-3) == 'S') {
                    SetColor(x,y, Color.Lime);
                    SetColor(x+1,y-1, Color.DarkGreen);
                    SetColor(x+2,y-2, Color.DarkGreen);
                    SetColor(x+3,y-3, Color.DarkGreen);
                    _total += 1;
                }
                if(At(x-1,y+1) == 'M' && At(x-2,y+2) == 'A' && At(x-3,y+3) == 'S') {
                    SetColor(x,y, Color.Lime);
                    SetColor(x-1,y+1, Color.DarkGreen);
                    SetColor(x-2,y+2, Color.DarkGreen);
                    SetColor(x-3,y+3, Color.DarkGreen);
                    _total += 1;
                }
                if(At(x-1,y-1) == 'M' && At(x-2,y-2) == 'A' && At(x-3,y-3) == 'S') {
                    SetColor(x,y, Color.Lime);
                    SetColor(x-1,y-1, Color.DarkGreen);
                    SetColor(x-2,y-2, Color.DarkGreen);
                    SetColor(x-3,y-3, Color.DarkGreen);
                    _total += 1;
                }
            }
            Draw(ctx);
        }
        Success = true;
        if(_slow) await Task.Delay(50);
        for(int x = 0; x < height; x++) {
            for(int y = 0; y < width; y++) {
                Color? z = ColorAt(x,y);
                if(z is not null && z.Equals(Color.Grey)) SetPosition(x,y,'.', Color.Grey);
            }
        }
        Draw(ctx);
    }
    public async Task SolvePt2(LiveDisplayContext ctx) {
        _total = null;
        Success = false;
        Draw(ctx);
        if(_slow) await Task.Delay(50);
        for(int x = 0; x < height; x++) {
            for(int y = 0; y < width; y++) {
                if(At(x,y) == 'A') SetColor(x,y,Color.Aqua);
            }
        }
        Draw(ctx);
        _total = 0;
        for(int x = 0; x < height; x++) {
            for(int y = 0; y < width; y++) {
                char c = At(x,y);
                if(c != 'A') continue;
                SetColor(x,y, Color.Red);
                if(_slow) Draw(ctx);
                if(_slow) await Task.Delay(50);
                SetColor(x,y, Color.Grey);
                
                // check for MAS in cross configurations
                // M.S   M.M   S.M   S.S 
                // .A.   .A.   .A.   .A. 
                // M.S   S.S   S.M   M.M 
                if((At(x-1,y-1) == 'M' && At(x+1,y+1)=='S') ||
                   (At(x-1,y-1) == 'S' && At(x+1,y+1)=='M') ){
                    if((At(x-1,y+1) == 'M' && At(x+1,y-1) == 'S') ||
                       (At(x-1,y+1) == 'S' && At(x+1,y-1) == 'M')){
                        _total +=1;
                        SetColor(x,y,Color.Lime);
                        SetColor(x-1,y-1,Color.DarkGreen);
                        SetColor(x-1,y+1,Color.DarkGreen);
                        SetColor(x+1,y-1,Color.DarkGreen);
                        SetColor(x+1,y+1,Color.DarkGreen);
                    }
                }
            }
            Draw(ctx);
        }
        Success = true;
        if(_slow) await Task.Delay(50);
        for(int x = 0; x < height; x++) {
            for(int y = 0; y < width; y++) {
                Color? z = ColorAt(x,y);
                if(z is not null && z.Equals(Color.Grey)) SetPosition(x,y,'.', Color.Grey);
            }
        }
        Draw(ctx);
    }
    private void Draw(LiveDisplayContext ctx) {
        var _display = new Grid();
        foreach(var spot in letterGrid[0]){
            _display.AddColumn(new GridColumn());
        }
        foreach(var row in letterGrid) {
            _display.AddRow(row.Select(spot => 
            new Text(spot.Item1.ToString(), 
                new Style(spot.Item2))).ToArray());
        }
        var p = new Panel(_display);
        if(_total is not null) p.Header = new PanelHeader($"Found: {_total.Value}", Justify.Center);
        ctx.UpdateTarget(p);
        ctx.Refresh();
    }
}