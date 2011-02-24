/*  Abacus - a calculator that calculates as you type
    Copyright (C) 2011  Peter Stuifzand

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using Gtk;
using Pango;
using Abacus;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		textview1.GrabFocus();
		
		textview1.ModifyFont(FontDescription.FromString("Sans 14"));
		textview2.ModifyFont(FontDescription.FromString("Sans 14"));
		
		quitAction.Activated  += delegate(object sender, EventArgs e) {
			Gtk.Main.Quit();
		};
		
		aboutAction.Activated += delegate(object sender, EventArgs e) {
			Gtk.AboutDialog d = new Gtk.AboutDialog();
			d.ProgramName = "Abacus (Build 1010)";
			d.Authors     = new String[]{"Peter Stuifzand"};
			d.Copyright   = "Copyright (c) 2011 Peter Stuifzand";
			d.Website     = "http://stuifzand.eu/abacus/";
			d.Version     = "0.6.3";
			d.License     = "Abacus\n"+
"    Copyright (C) 2011  Peter Stuifzand\n"+
"\n"+
"    This program is free software: you can redistribute it and/or modify\n"+
"    it under the terms of the GNU General Public License as published by\n"+
"    the Free Software Foundation, either version 3 of the License, or\n"+
"    (at your option) any later version.\n"+
"\n"+
"    This program is distributed in the hope that it will be useful,\n"+
"    but WITHOUT ANY WARRANTY; without even the implied warranty of\n"+
"    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the\n"+
"    GNU General Public License for more details.\n"+
"\n"+
"    You should have received a copy of the GNU General Public License\n"+
"    along with this program.  If not, see <http://www.gnu.org/licenses/>.";
			d.Deletable   = true;
			d.Response += delegate(object o, ResponseArgs args) {
				Gtk.AboutDialog dialog = o as AboutDialog;
				if (args.ResponseId == ResponseType.Cancel) {
					dialog.Destroy();
				}
			};
			d.Run();
		};
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	
	protected virtual void OnTextview1KeyReleaseEvent (object o, Gtk.KeyReleaseEventArgs args)
	{
		switch(args.Event.Key) {
		case Gdk.Key.Escape:
			Gtk.Main.Quit();
			break;
		}

		LineCalculator calc = new LineCalculator();
		string [] lines = textview1.Buffer.Text.Split("\n".ToCharArray());
		ICollection<string> col = calc.CalculateLines(Array.AsReadOnly(lines));
		string s = "";
		foreach (string line in col) {
			s += line + "\n";
		}
		textview2.Buffer.Text = s;
	}
}
