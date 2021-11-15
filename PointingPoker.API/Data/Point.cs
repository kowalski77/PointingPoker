using PointingPoker.API.Support;

namespace PointingPoker.API.Data;

public class Point : GameEnumeration<Point>
{
    private Point(){}
    
    public static readonly Point Zero = new(0, nameof(Zero), "0 points");
    public static readonly Point One = new(1, nameof(One), "1 point");
    public static readonly Point Two = new(2, nameof(Two), "2 points");
    public static readonly Point Three = new(3, nameof(Three), "3 points");
    public static readonly Point Five = new(5, nameof(Five), "5 points");
    public static readonly Point Eight = new(8, nameof(Eight), "8 points");
    public static readonly Point Ten = new(10, nameof(Ten), "10 points");
    public static readonly Point Thirteen = new(13, nameof(Thirteen), "13 points");
    public static readonly Point Twenty = new(20, nameof(Twenty), "20 points");
    public static readonly Point Forty = new(40, nameof(Forty), "40 points");
    public static readonly Point OneHundred = new(100, nameof(OneHundred), "100 points");

    protected Point(int id, string name, string display) : base(id, name)
    {
        this.Display = display;
    }

    public string? Display { get; }
    
    public static Point? FindById(int id)
    {
        return All.SingleOrDefault(x=>x.Id == id);
    }
}
