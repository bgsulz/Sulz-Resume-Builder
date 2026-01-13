using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace BGSulz;

public class Program
{
    const string TotalYamlPath = @"C:\Users\B\Documents\Programming\Resume Generator\Sulz-Resume-Builder\resume\TemplateYAML.yaml";

    private static string GetPersonalizedPDFName(string name) => $"Ben Sulzinsky - {name} Resume.pdf";
    private static string GetPersonalizedYamlName(string name) => $"{name} Resume Data.yaml";

    public static void Main(string[] args)
    {
        var result = Arguments.Parse(args);

        switch (result.Type)
        {
            case Arguments.CommandType.Build:
                var resumePath = BuildResume(result.Directory, result.Name);
                Console.WriteLine($"Resume built successfully at {resumePath}.");
                break;
            case Arguments.CommandType.Prepare:
                var yamlPath = PrepareForResume(result.Directory, result.Name);
                Console.WriteLine($"YAML prepared successfully at {yamlPath}.");
                break;
            default: throw new ArgumentOutOfRangeException();
        }
    }

    private static string BuildResume(string directory, string name)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var yamlPath = Path.Combine(directory, GetPersonalizedYamlName(name));
        var savePath = Path.Combine(directory, GetPersonalizedPDFName(name));

        var model = new YamlResumeDataSource(yamlPath).ResumeData();

        var file = new ResumeDocument(model);
        file.GeneratePdf(savePath);

        return savePath;
    }

    private static string PrepareForResume(string directory, string name)
    {
        var yamlPath = Path.Combine(directory, GetPersonalizedYamlName(name));
        File.Copy(TotalYamlPath, yamlPath);
        return yamlPath;
    }
}
