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
using System.Collections.Generic;

namespace Abacus
{
	public class Binding
	{
		private Dictionary<string, Expression> locals;
		private Dictionary<string, ReturnValue> cache;

		private ISet<string> consts;

		public Binding ()
		{
			locals = new Dictionary<string, Expression>();
			cache = new Dictionary<string, ReturnValue>();

			consts = new SortedSet<string>();
		}

		public bool Set(string name, Expression expr, bool is_const = false) {
			if (!consts.Contains(name)) {
				locals[name] = expr;
				if (is_const) {
					consts.Add(name);
					return true;
				}
				return true;
			}
			return false;
		}

		public ReturnValue ValueFor(string name) {
			if (!cache.ContainsKey(name)) {
				cache[name] = new ReturnValue();
				cache[name] = locals[name].Value(this);
			}
			return cache[name];
		}
	}
}

