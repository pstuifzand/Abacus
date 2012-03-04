/*  Abacus - a calculator that calculates as you type
    Copyright (C) 2012  Peter Stuifzand

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
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Gtk;
using Pango;
using Abacus;

public partial class MainWindow: Gtk.Window
{
    public MainWindow(): base (Gtk.WindowType.Toplevel)
    {
        Build();
        textview1.GrabFocus();

        textview1.ModifyFont(FontDescription.FromString("Sans 14"));
        textview2.ModifyFont(FontDescription.FromString("Sans 14"));
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected virtual void OnTextview1KeyReleaseEvent(object o, Gtk.KeyReleaseEventArgs args)
    {
        switch (args.Event.Key) {
        case Gdk.Key.Escape:
            Gtk.Main.Quit();
            break;
        }
        Recalculate();
    }

    public void Recalculate()
    {
        LineCalculator calc = new LineCalculator();
        string [] lines = textview1.Buffer.Text.Split("\n".ToCharArray());
        ICollection<string > col = calc.CalculateLines(Array.AsReadOnly(lines));

        string s = "";
     
        foreach (string line in col) {
            s += line + "\n";
        }

        textview2.Buffer.Text = s;
    }
}
