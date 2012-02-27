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

namespace Abacus
{
	public abstract class Expression
	{
		public Expression()
		{
		}
			
		public static Expression parse(string s) {
			Parser p = new Parser(s);
			return p.parse();
		}
				
		public override string ToString() {
			return "";
		}
		
		public abstract ReturnValue Value(Binding b);
	}
}
