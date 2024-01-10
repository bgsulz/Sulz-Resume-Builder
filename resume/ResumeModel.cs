using System.Globalization;
using System.Text;

namespace BGSulz;

public class ResumeModel
{
    public List<ResumeCategory>? Categories { get; set; }
}

public class ResumeCategory
{
    public string? CategoryTitle { get; set; }
    public List<ResumeItem>? Items { get; set; }
}

public class ResumeItem
{
    public string? ItemID { get; set; }
    public string? ItemTitle { get; set; }
    public string? ItemDesc { get; set; }
    public List<string>? Bullets { get; set; }

    public YearMonthSpan? Timespan { get; set; }
}

public struct YearMonth : IEquatable<YearMonth>, IEquatable<YearMonth?>
{
    public int Year { get; set; }
    public int? Month { get; set; }

    public bool Equals(YearMonth other) => Year == other.Year && Month == other.Month;
    public bool Equals(YearMonth? other) => other != null && Equals(other);
    public override bool Equals(object? obj) => obj is YearMonth other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(Year, Month);

    public static bool operator ==(YearMonth a, YearMonth b) => a.Equals(b);
    public static bool operator !=(YearMonth a, YearMonth b) => !(a == b);

    public static bool operator ==(YearMonth? a, YearMonth b) => a.Equals(b);
    public static bool operator !=(YearMonth? a, YearMonth b) => !(a == b);
}

public struct YearMonthSpan
{
    private static readonly DateTimeFormatInfo DateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;

    public YearMonth Start { get; set; }
    public YearMonth? End { get; set; }

    public override string ToString()
    {
        Func<int, string> transformer = x => $"{x}/";
        
        var startMonth = Start.Month.DoOrFallback(transformer, "");
        var endMonth = End?.Month.DoOrFallback(transformer, "") ?? "present";

        var builder = new StringBuilder();

        builder.Append(startMonth);

        if (Start == End)
        {
            builder.Append($"{Start.Year}");
            return builder.ToString();
        }

        //if (Start.Year != End?.Year)
            builder.Append($"{Start.Year}");

        builder.Append(" - ");
        builder.Append(endMonth);
        if (End.HasValue) builder.Append($"{End.Value.Year}");

        return builder.ToString();
    }
}