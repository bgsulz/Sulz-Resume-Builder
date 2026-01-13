namespace BGSulz;

public static class Arguments
{
    public enum CommandType
    {
        Build,
        Prepare
    }

    private static string ToInitialCaps(this string self) => char.ToUpperInvariant(self[0]) + self[1..].ToLowerInvariant();

    public static CommandResult Parse(string[] args)
    {
        if (args.Length < 1)
            throw new ArgumentException("Needs command. Use 'prepare' or 'build'.");

        if (!Enum.TryParse<CommandType>(args[0].ToInitialCaps(), out var command))
            throw new ArgumentException("Invalid command. Use 'prepare' or 'build.'");

        var directory = Directory.GetCurrentDirectory();
        var name = new DirectoryInfo(directory).Name;

        var nameSplit = name.Split(" ");
        if (nameSplit.Length != 1)
            name = string.Join(" ", nameSplit.Take(nameSplit.Length - 1));
        
        return new CommandResult(command, directory, name);
    }

    public record CommandResult(CommandType Type, string Directory, string Name);
}