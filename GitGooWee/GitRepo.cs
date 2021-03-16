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

        private static IEnumerable<Branch> ParseGitBranchOutput(string branchString)
        {
            var branches = branchString.Split('\n');

            IEnumerable<Branch> res;

            if (branches.Count() != 2)
            {
                res = branches
                .Skip(1)
                .SkipLast(1)
                .Select(branch => branch.StartsWith("*")
                ? new Branch() { Name = branch[2..], Current = true }
                : new Branch() { Name = branch.Trim() }).AsEnumerable();
            }
            else
            {
                res = new List<Branch>() { new Branch() { Current = true, Name = branches[0][2..] } };
            }

            return res;
        }

        public static string GetRemote() => 
            Terminal.Send(GitCommands.GetRemote);
        
        public static List<Commit> GetUnPushedCommits(string origin, string branch)
        {
            var commits = Terminal.Send($"{GitCommands.GetUnPushedCommits} {origin}/{branch}");

            return commits
                .Split('\n')
                .SkipLast(1)
                .Select(ParseCommitString).ToList();
        }

        private static Commit ParseCommitString(string commitString)
        {
            var parsed = commitString[2..].Split(' ', 2);
            return new Commit(){Hash = parsed[0], Message = parsed[1]};
        }
    }
}
