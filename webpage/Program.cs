namespace BGSulz
{
    public class Program
    {
        private const string Template = @"---
layout: home
hidden: true
onlytag: [{{tags}}]
permalink: /{{link}}/
---

# Hello {{name}}!

Here are some selected game projects of mine. You might recognize some of the titles from my resume. Please enjoy!";
        private const string WebpagesPath = "C:/Users/bgsma/Documents/Personal Wiki/Website/Personal-Website/docs/specials";
        
        public static void Main(string[] args)
        {
            var name = GetNameFromDirectory();
            var tags = string.Join(", ", args);
            var text = Template.Replace("{{name}}", name).Replace("{{tags}}", tags).Replace("{{link}}", name.ToLower());

            var path = Path.Combine(WebpagesPath, $"{name.ToLower()}.markdown");
            File.WriteAllText(path, text);
            
            Console.WriteLine($"Created webpage at {path}.");
        }

        private static string GetNameFromDirectory()
        {
            var directory = Directory.GetCurrentDirectory();
            var directoryName = new DirectoryInfo(directory).Name;
            var directoryNameSplit = directoryName.Split(" ");
            return string.Join(" ", directoryNameSplit.Take(directoryNameSplit.Length - 1));
        }
    }
}