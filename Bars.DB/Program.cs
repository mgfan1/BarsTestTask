using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Bars.DB
{
    internal class Program
    {
        private const string SqlExtension = ".sql";
        private static readonly Regex IncludedFileRegex =
            new Regex(@"-- #include '(\w+.sql)'", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static void Main(string[] args)
        {
            GenerateScripts();
            Console.WriteLine("Success");
        }

        private static void GenerateScripts()
        {
            var mainPath = Directory.GetCurrentDirectory();
            var mainDirectory = new DirectoryInfo(mainPath);
            var updateFilePath = Path.Combine(mainPath, "Update" + SqlExtension);
            File.Create(updateFilePath).Close();
            var rollbackFilePath = Path.Combine(mainPath, "Rollback" + SqlExtension);
            File.Create(rollbackFilePath).Close();
            var projectDirectory = mainDirectory.Parent.Parent.Parent;
            var databasesDirectory = projectDirectory.GetDirectories().First(d => d.Name == "Databases");
            var databases = databasesDirectory.GetDirectories();

            databases.ToList().ForEach(database =>
            {
                AddFiles(updateFilePath, database, false);

                var dbMainFolders = database.GetDirectories();
                var schemasDirectory = dbMainFolders.First(d => d.Name == "Schemas");
                schemasDirectory.GetDirectories().ToList().ForEach(schema =>
                {
                    var dbDataFolders = schema.GetDirectories();
                    var tables = dbDataFolders.First(d => d.Name == "Tables");
                    AddFiles(updateFilePath, tables, true);
                    var init = dbDataFolders.First(d => d.Name == "Init");
                    AddFiles(updateFilePath, init, true);
                    var procedures = dbDataFolders.First(d => d.Name == "Procedures");
                    AddFiles(updateFilePath, procedures, true);
                });

                var rollbackDirectory = dbMainFolders.First(d => d.Name == "Rollback");
                AddFiles(rollbackFilePath, rollbackDirectory, false);
            });
        }

        private static void AddFiles(string targetFilePath, DirectoryInfo sourceDirectory, bool findInIncludedFiles)
        {
            using var sw = new StreamWriter(targetFilePath, true, Encoding.ASCII);
            var files = Directory.GetFiles(sourceDirectory.FullName, "*" + SqlExtension);
            var sortedFilesToAdd = new List<string>();

            void AddFile(string file)
            {
                if (targetFilePath != file)
                {
                    using var sr = new StreamReader(file, Encoding.ASCII);
                    while (sr.Peek() > -1)
                    {
                        sw.WriteLine(sr.ReadLine());
                    }
                }
            };

            if (findInIncludedFiles)
            {
                var includesFile = files.First(f => Path.GetFileNameWithoutExtension(f).Remove(0, 1) == sourceDirectory.Name);
                using (var reader = new StreamReader(includesFile))
                {
                    var includes = IncludedFileRegex.Matches(reader.ReadToEnd());
                    sortedFilesToAdd.AddRange(includes.Select(i => i.Groups[1].Value));
                }

                sortedFilesToAdd.ForEach(fileToAddName =>
                {
                    var fileToAdd = files.First(file => Path.GetFileName(file) == fileToAddName);
                    AddFile(fileToAdd);
                });
            }
            else
            {
                foreach (string strFile in files)
                {
                    AddFile(strFile);
                }
            }
        }
    }
}