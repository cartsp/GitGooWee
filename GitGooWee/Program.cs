using System;
using Terminal.Gui;

namespace GitGooWee
{
    class Program
    {
        static void Main(string[] args)
        {
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
				new MenuItem ("_Quit", "", () => Application.RequestStop())
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
				Height = Dim.Fill(1),
				CanFocus = true
			};
			leftPane.Title = $"Branches";

			win.Add(leftPane,
				new Label(3, 18, "Press F9 or ESC plus 9 to activate the menubar")
			);

			var branches = GitRepo.GetBranches();

			Application.Run();
        }
    }
}
