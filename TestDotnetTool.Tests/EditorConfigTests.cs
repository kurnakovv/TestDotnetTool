using System.Diagnostics;
using System.Reflection;

namespace TestDotnetTool.Tests;

public class EditorConfigTests
{
    static string ToolProjectPath =>
    typeof(EditorConfigTests).Assembly
        .GetCustomAttributes<AssemblyMetadataAttribute>()
        .Single(x => x.Key == "ToolProjectPath")
        .Value!;

    [Fact]
    public async Task App_Processes_EditorConfig_From_Test_Folder()
    {
        // Arrange
        var testDir = new TestDirectory(nameof(App_Processes_EditorConfig_From_Test_Folder));

        var template = System.IO.Path.Combine(
            AppContext.BaseDirectory,
            "Templates",
            ".editorconfig"
        );

        testDir.CopyFromTemplate(template);

        var appPath = System.IO.Path.Combine(
            AppContext.BaseDirectory,
            "kurnakovv.TestDotnetTool.dll"
        );

        var toolProjectPath = Path.GetFullPath(ToolProjectPath);
        var dllPath = Path.Combine(
            Path.GetDirectoryName(toolProjectPath)!,
            "bin",
            "Debug",
            "net8.0",
            "kurnakovv.TestDotnetTool.dll"
        );

        var psi = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"\"{dllPath}\"",
            WorkingDirectory = testDir.Path,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false
        };

        // Act
        using var process = Process.Start(psi)!;
        var output = await process.StandardOutput.ReadToEndAsync();
        await process.WaitForExitAsync();

        Assert.Equal(0, process.ExitCode);
        Assert.Contains("editorconfig processed", output);
    }
}
