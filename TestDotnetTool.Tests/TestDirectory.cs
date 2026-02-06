using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDotnetTool.Tests;

public class TestDirectory
{
    public string Path { get; }

    public TestDirectory(string testName)
    {
        //Path = System.IO.Path.Combine(
        //    System.IO.Path.GetTempPath(),
        //    Guid.NewGuid().ToString("N"));
        Path = System.IO.Path.Combine(
            AppContext.BaseDirectory,
            "_tests",
            testName,
            Guid.NewGuid().ToString("N")
        );

        Directory.CreateDirectory(Path);
    }

    public void CopyFromTemplate(string templatePath)
    {
        File.Copy(
            templatePath,
            System.IO.Path.Combine(Path, ".editorconfig"),
            overwrite: true
        );
    }
}
