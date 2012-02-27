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

using NUnit.Framework;

namespace Abacus
{
	[TestFixture()]
	public class LineCalculatorTest
	{

		[Test()]
		public void NoLines ()
		{
			LineCalculator lc = new LineCalculator();
			ICollection<string> result = lc.CalculateLines(Array.AsReadOnly(new string[]{}));
			Assert.AreEqual(0, result.Count);
		}
		
		[Test()]
		public void OneLine() {
			LineCalculator lc = new LineCalculator();
			
			ICollection<string> result = lc.CalculateLines(Array.AsReadOnly(new string[]{"1"}));
			Assert.AreEqual(1, result.Count);
			
			ICollection<string> expected = Array.AsReadOnly(new string[]{"1"});
			Assert.AreEqual(expected, result);
		}
		
		[Test()]
		public void CalculateOneLine() {
			LineCalculator lc = new LineCalculator();
			
			ICollection<string> result = lc.CalculateLines(Array.AsReadOnly(new string[]{"1+1"}));
			Assert.AreEqual(1, result.Count);
			
			ICollection<string> expected = Array.AsReadOnly(new string[]{"2"});
			Assert.AreEqual(expected, result);
		}
		
		[Test()]
		public void CalculateTwoLines() {
			LineCalculator lc = new LineCalculator();
			
			ICollection<string> result = lc.CalculateLines(Array.AsReadOnly(new string[]{"1+1", "2+1"}));
			Assert.AreEqual(2, result.Count);
			
			ICollection<string> expected = Array.AsReadOnly(new string[]{"2", "3"});
			Assert.AreEqual(expected, result);
		}
		
		[Test()]
		public void CalculateTwoLinesSecondIncomplete() {
			LineCalculator lc = new LineCalculator();
			
			ICollection<string> result = lc.CalculateLines(Array.AsReadOnly(new string[]{"1+1", "2+"}));
			Assert.AreEqual(2, result.Count);
			
			ICollection<string> expected = Array.AsReadOnly(new string[]{"2", "2+"});
			Assert.AreEqual(expected, result);
		}
		
		[Test()]
		public void CalculateThreeLinesSecondIncomplete() {
			LineCalculator lc = new LineCalculator();
			
			ICollection<string> result = lc.CalculateLines(Array.AsReadOnly(new string[]{"1+1", "2+", "5+5"}));
			Assert.AreEqual(3, result.Count);
			
			ICollection<string> expected = Array.AsReadOnly(new string[]{"2", "2+", "10"});
			Assert.AreEqual(expected, result);
		}
		
		[Test()]		
		public void CalculateFourLinesLastIncomplete() {
			LineCalculator lc = new LineCalculator();
			
			ICollection<string> result = lc.CalculateLines(Array.AsReadOnly(new string[]{"1+1", "2+2", "5+5", "2+2+"}));
			Assert.AreEqual(4, result.Count);
			
			ICollection<string> expected = Array.AsReadOnly(new string[]{"2", "4", "10", "4+"});
			Assert.AreEqual(expected, result);
		}
		/*
		[Test()]
		public void TestVariables() {
			string[] tests = {
				"x=1",   "1",
				"1+x",   "2",
				"benzine=60", "60",
				"ritten=8", "8",
				"kosten=benzine/ritten", "7.5",
				//"x=1", "x = 1",
			};
			TestCalculations(tests);
		}*/
	}
}
