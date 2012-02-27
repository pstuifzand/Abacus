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
using System.Collections.Generic;

namespace Abacus
{


	public class LineCalculator
	{
		public LineCalculator ()
		{
		}
		
		public ICollection<string> CalculateLines(ICollection<string> lines) {
			
			Binding binding = new Binding();
			Calculator calc = new Calculator(binding);
			
			List<string> list = new List<string>();
			
			foreach (string line in lines) {
				int len;
				for (len = line.Length; len >= 1; len--) {
					string s = line.Substring(0, len);
					try {
						Expression expr = Expression.parse(s);
						list.Add(calc.calculate(expr).ToString() + line.Substring(len));
						break;
					}
					catch (ParserException /* unused */) {}
				}
				if (len == 0) {
					list.Add(line);
				}
			}
			return list;
		}
	}
}
