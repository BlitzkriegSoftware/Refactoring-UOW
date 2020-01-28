using CommandLine;

namespace UoW.Demo.Models
{
    public class Options
    {
        [Option(shortName:'f', longName:"folder", Default = @"c:\temp", HelpText ="Root Folder to Test In" )]
        public string RootFolder { get; set; }
    }
}
