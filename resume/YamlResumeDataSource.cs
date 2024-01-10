using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace BGSulz;

public class YamlResumeDataSource
{
    private readonly string _path;
    public YamlResumeDataSource(string path) => _path = path;

    public ResumeModel ResumeData()
    {
        var deserializer = new DeserializerBuilder()
            .WithTypeConverter(new YearMonthSpanConverter())
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        return deserializer.Deserialize<ResumeModel>(File.ReadAllText(_path));
    }
}