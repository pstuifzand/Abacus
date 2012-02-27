//     Abacus - a calculator that calculates as you type
//     Copyright (C) 2012  Peter Stuifzand
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Globalization;

namespace Abacus
{
	public class ReturnValue
	{
		private bool defined;
		private double val;
		
		public ReturnValue ()
		{
			defined = false;
		}
		
		public ReturnValue(double val)
		{
			defined = true;
			this.val = val;
		}
		
		public bool Defined() {
			return defined;
		}
		
		public double Value() {
			return val;
		}
		
		public override string ToString() {
			if (Defined()) {
				return String.Format(CultureInfo.GetCultureInfo("en-US"), "{0}", val);
			}
			return "undefined";
		}
	}
}

