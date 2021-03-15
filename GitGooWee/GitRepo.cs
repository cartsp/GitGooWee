using System;
using System.Collections.Generic;
using System.Linq;

namespace GitGooWee
{
    public static class GitRepo
    {
        public static IEnumerable<Branch> GetBranches()
        {
            var res = Terminal.Send(GitCommands.GetBranchs);
            return ParseBranchString(res);
        }

        private static IEnumerable<Branch> ParseBranchString(string branchString) =>
            branchString
                .Split('\n')
                .Skip(1)
                .SkipLast(1)
                .Select(branch => branch.StartsWith("*")
                    ? new Branch() { Name = branch[2..], Current = true }
                    : new Branch() { Name = branch });
    }
}
