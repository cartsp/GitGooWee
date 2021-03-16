﻿using System;
using System.Linq;
using Terminal.Gui;

namespace GitGooWee
{
    class Program
    {
	    private static string GitRemote =  string.Empty;
        static void Main(string[] args)
        {
	        GitRemote = GitRepo.GetRemote().Trim();
	        var res = GitRepo.GetBranches();
	        var notPushed = GitRepo.GetUnPushedCommits(GitRemote, res.Single(a => a.Current).Name);
			Application.Init();
			var top = Application.Top;
			
			// Creates the top-level window to show
			var win = new Window("GitUI")
			{
				X = 0,
				Y = 1, // Leave one row for the toplevel menu

				Width = Dim.Fill(),
				Height = Dim.Fill()
			};

			top.Add(win);

			// Creates a menubar, the item "New" has a help menu.
			var menu = new MenuBar(new MenuBarItem[] {
			new MenuBarItem ("_Git", new MenuItem [] {
				new MenuItem ("_New", "Creates new file", null),
				new MenuItem ("_Close", "", null),
				new MenuItem ("_Quit", "", () =>
				{
					Application.Driver.End();
					Application.RequestStop();
				})
			}),
			new MenuBarItem ("_Edit", new MenuItem [] {
				new MenuItem ("_Copy", "", null),
				new MenuItem ("C_ut", "", null),
				new MenuItem ("_Paste", "", null)
			})
		});
			top.Add(menu);

			var leftPane = new FrameView("Branches")
			{
				X = 0,
				Y = 1, // for menu
				Width = 25,
				Height = 20,
				CanFocus = true
			};
			leftPane.Title = $"Branches";
			
			var branchList = new ListView();
			branchList.SetSource(res);
			branchList.Width = 23;
			branchList.Height = res.Count;
			leftPane.Add(branchList);
			
			var rightPane = new FrameView("Local Commits")
			{
				X = 26,
				Y = 1, // for menu
				Width = 45,
				Height = 20,
				CanFocus = true
			};
			rightPane.Title = $"Local Commits";
			
			var commitList = new ListView();
			commitList.SetSource(notPushed);
			commitList.Width = 50;
			commitList.Height = notPushed.Count;
			rightPane.Add(commitList);
			
			win.Add(leftPane, rightPane);
			
			Application.Run();
        }
    }
}
