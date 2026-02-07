string? argument = args.FirstOrDefault();
string bot = $"Hello from kurnakovv";
bot += @"";
Console.WriteLine(bot);

if (argument == "setup")
{
    string currentDirectory = Directory.GetCurrentDirectory();
    string editorConfigPath = Path.Combine(currentDirectory, ".editorconfig");

    if (File.Exists(editorConfigPath))
    {
        Console.WriteLine($".editorconfig найден: {editorConfigPath}");
    }
    else
    {
        Console.WriteLine(".editorconfig не найден в текущей директории.");
    }

    string targetDir = ".biak";
    string targetFile = Path.Combine(targetDir, ".editorconfig-main");

    // Проверяем, существует ли .editorconfig
    if (!File.Exists(editorConfigPath))
    {
        Console.WriteLine("Файл .editorconfig не найден.");
        return;
    }

    Directory.CreateDirectory(targetDir);

    using var reader = new StreamReader(editorConfigPath);
    using var writer = new StreamWriter(targetFile, false);

    string? line;
    while ((line = reader.ReadLine()) != null)
    {
        line = line.Trim();

        //if (line.StartsWith("[") && line.EndsWith("]") && line.Length > 2)
        //{
        //    writer.WriteLine($"# ^biak^ {line}");
        //}

        writer.WriteLine(line);
    }

    Console.WriteLine("Файл .editorconfig-main успешно создан.");
}
