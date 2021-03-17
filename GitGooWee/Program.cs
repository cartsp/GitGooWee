using System;
using System.Linq;
using Terminal.Gui;

namespace GitGooWee
{
    class Program
    {
	    private static string GitRemote =  string.Empty;
        static void Main(string[] args)
        {
	        var statusBar = new StatusBar () {Visible = true};
			
	        statusBar.Items = new StatusItem[]
	        {
		        new (Key.Q | Key.CtrlMask, "~CTRL-Q~ Quit", () =>
		        { Application.RequestStop(); }),
		        new (Key.S | Key.CtrlMask, "~CTRL-S~ Squash", () =>
		        { 
					bool okpressed = false;
					var ok = new Button(3, 14, "Ok");
					ok.Clicked += () =>
					{
            			Application.RequestStop();
            			okpressed = true;
					};
            
					var cancel = new Button(10, 14, "Cancel");
					cancel.Clicked += () => Application.RequestStop();
                 
					var dialog = new Dialog ("Login", 60, 18, ok, cancel);
            
					var entry = new TextField () {
            			X = 1, 
            			Y = 1,
            			Width = Dim.Fill (),
            			Height = 1
					};
					dialog.Add (entry);
					Application.Run (dialog);
					if (okpressed)
            			Console.WriteLine ("The user entered: " + entry.Text);
				})
	        };

	        GitRemote = GitRepo.GetRemote().Trim();
	        var res = GitRepo.GetBranches();
	        var notPushed = GitRepo.GetUnPushedCommits(GitRemote, res.Single(a => a.Current).Name);
			
	        Application.Init();
			var top = Application.Top;
			
			var win = new Window("GitUI")
			{
				X = 0,
				Y = 1, // Leave one row for the toplevel menu

				Width = Dim.Fill(),
				Height = Dim.Fill()
			};

			top.Add(win);
			
			var menu = new MenuBar(new MenuBarItem[] {
							new MenuBarItem ("_Git", new MenuItem [] {
								new MenuItem ("_New", "Creates new file", null),
								new MenuItem ("_Close", "", null),
								new MenuItem ("_Quit", "", () => {Application.Driver.End();Application.RequestStop();})
							})
						});

			top.Add(menu);
			top.Add(statusBar);

			var leftPane = new FrameView("Branches")
			{
				X = 0,
				Y = 1, // for menu
				Width = 25,
				Height = Dim.Fill(),
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
				Width = Dim.Fill(),
				Height = Dim.Fill(),
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
