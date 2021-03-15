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

        private static IEnumerable<Branch> ParseBranchString(string branchString)
        {
            var branchStrings = branchString.Split('\n');

            foreach(var branch in branchStrings.Skip(1).SkipLast(1))
            {
                if (branch.StartsWith("*"))
                {
                    yield return new Branch() { Name = branch.Substring(2), Current = true };
                }
                else
                {
                    yield return new Branch() { Name = branch };
                }
                
            }
        }

    }
}
