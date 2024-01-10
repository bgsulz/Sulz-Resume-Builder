using QuestPDF.Helpers;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace BGSulz;

public class PlaceholderResumeDataSource
{
    public ResumeModel ResumeData()
    {
        return new ResumeModel
        {
            Categories = Enumerable.Range(0, 3)
                .Select(_ => ResumeCategory())
                .ToList()
        };
    }

    public ResumeCategory ResumeCategory()
    {
        return new ResumeCategory
        {
            CategoryTitle = Placeholders.Label(),
            Items = Enumerable.Range(0, 5)
                .Select(_ => ResumeItem())
                .ToList()
        };
    }

    public ResumeItem ResumeItem()
    {
        var bulletPointsCount = Random.Shared.Next(3, 5);

        return new ResumeItem
        {
            ItemTitle = Placeholders.Label(),
            ItemDesc = Placeholders.Label(),
            Bullets = Enumerable.Range(0, bulletPointsCount)
                .Select(_ => Placeholders.Sentence())
                .ToList(),
            Timespan = RandomTimeSpan()
        };
    }

    private YearMonthSpan RandomTimeSpan()
    {
        var startDateTime = RandomDateTime();
        var endDateTime = startDateTime.AddDays(Random.Shared.Next(30, 3000));
        var shouldBePresent = endDateTime > DateTime.Today;

        var start = new YearMonth
        {
            Month = startDateTime.Month,
            Year = startDateTime.Year
        };
        YearMonth? end = shouldBePresent ? null : new YearMonth
        {
            Month = endDateTime.Month,
            Year = endDateTime.Year
        };

        return new YearMonthSpan
        {
            Start = start,
            End = end
        };
    }

    private DateTime RandomDateTime()
    {
        var start = new DateTime(2010, 1, 1);
        var range = (DateTime.Today - start).Days;
        return start.AddDays(Random.Shared.Next(range));
    }
    
    private static ResumeModel SerializePlaceholder(string path)
    {
        var model = new PlaceholderResumeDataSource().ResumeData();

        var serializer = new SerializerBuilder()
            .WithTypeConverter(new YearMonthSpanConverter())
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .DisableAliases()
            .Build();
        var yaml = serializer.Serialize(model);
        File.WriteAllText(path, yaml);

        return model;
    }
}