using System;
using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;

namespace GitGooWee
{
    public static class GitRepo
    {
        public static List<Branch> GetBranches()
        {
            var gitBranchOutput = Terminal.Send(GitCommands.GetBranchs);
            
            return ParseGitBranchOutput(gitBranchOutput).ToList();
        }

        private static IEnumerable<Branch> ParseGitBranchOutput(string branchString) =>
            branchString
                .Split('\n')
                .Skip(1)
                .SkipLast(1)
                .Select(branch => branch.StartsWith("*")
                    ? new Branch() { Name = branch[2..], Current = true }
                    : new Branch() { Name = branch.Trim() });

        public static string GetRemote() => 
            Terminal.Send(GitCommands.GetRemote);
        
        public static string GetUnPushedCommits(string origin, string branch) => 
            Terminal.Send($"({GitCommands.GetUnPushedCommits} {origin}/{branch}");
        
    }
}
