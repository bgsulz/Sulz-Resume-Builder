using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace BGSulz;

public class YearMonthSpanConverter : IYamlTypeConverter
{
    public bool Accepts(Type type)
    {
        return type == typeof(YearMonthSpan?);
    }

    public object? ReadYaml(IParser parser, Type type)
    {
        var value = (parser.Current as Scalar)?.Value;
        if (value == null) throw new YamlException("Unable to deserialize YearMonthSpan because parser wasn't found.");

        parser.MoveNext();

        if (string.IsNullOrWhiteSpace(value))
            return null;

        var parts = value.Split(" - ");

        YearMonth? start, end;

        if (parts.Length < 2)
        {
            Console.WriteLine($"Doing short YM : {value}");
            start = parts[0].ToYearMonth();
            end = start;
        }
        else
        {
            end = parts[1].ToYearMonth();
            start = parts[0].ToYearMonth(end);
        }

        return new YearMonthSpan
        {
            Start = start ?? throw new YamlException("Invalid start date."),
            End = end
        };
    }

    public void WriteYaml(IEmitter emitter, object? value, Type type)
    {
        var yearMonthSpan = value is YearMonthSpan span ? span : default;
        var yamlValue = yearMonthSpan.ToString().Replace("\n", string.Empty);

        emitter.Emit(new Scalar(null, null, yamlValue, ScalarStyle.Any, true, false));
    }
}

public static class YearMonthHelpers
{
    public static YearMonth? ToYearMonth(this string self, YearMonth? other = null)
    {
        var parts = self.Split(" ");

        var month = parts[0].ToMonth();
        if (month == Months.Invalid) return null;

        var year = parts.Length > 1 ? int.Parse(parts[1]) : other?.Year ?? 2000;

        return new YearMonth
        {
            Year = year,
            Month = (int)month
        };
    }
}