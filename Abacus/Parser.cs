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
using System.Globalization;

namespace Abacus
{
    public class Parser
    {
        private string text;
        private int index;
     
        public Parser(string s)
        {
            text = s;
            index = 0;
        }
     
        public Expression parse()
        {
            Expression expr = null;
         
            skipws();
            if (text.Contains("=") && Char.IsLetter(current())) {
                expr = parse_assignment();
            } else {
                expr = parse_expression();
            }
         
            if (current() != '\0') {
                throw new ParserException("Not at end yet");
            }
            return expr;
        }
     
        public Expression parse_var()
        {
            string name = "";
            while (Char.IsLetterOrDigit(current()) || current() == '.') {
                name += current();
                nextchar();
            }
            return new VariableExpression(name);
        }
     
        public Expression parse_assignment()
        {
            VariableExpression name = parse_var() as VariableExpression;
            skipws();
            if (current() == '=') {
                match('=');
                try {
                    Expression expr = parse_expression();
                    return new AssignmentExpression(name.Name, expr);
                } catch (ParserException /* unused */) {
                    return new AssignmentExpression(name.Name, null);
                }
            }
            return null;
        }
     
        public Expression parse_expression()
        {
            skipws();
            Expression num1 = parse_term();
            skipws();
            while (current() == '+' || current() == '-') {
                char op = current();
                nextchar();
                skipws();
                Expression num2 = parse_term();
                num1 = new OperatorExpression(op, num1, num2);
            }
            return num1;
        }
     
        public Expression parse_term()
        {
            Expression num1 = parse_factor();                        
            skipws();
            while (current() == '*' || current() == '/') {
                char op = current();
                nextchar();
                skipws();
                Expression num2 = parse_factor();
                num1 = new OperatorExpression(op, num1, num2);
                skipws();
            }
            return num1;
        }
     
        public Expression parse_factor()
        {
            if (current() == '(') {
                match('(');
                Expression expr = parse_expression();
                match(')');
                return expr;
            }
            if (Char.IsLetter(current())) {
                return parse_var();
            } else {
                return parse_number();
            }
        }

        public Expression parse_number()
        {
            if (current() != '-' && !Char.IsDigit(current())) {
                throw new ParserException("Not a number");
            }
         
            string number = "";
                         
            if (current() == '-') {
                number += current();
                match('-');
            }
         
            while (Char.IsDigit(current()) || current() == '_') {
                if (current() == '_') {
                    nextchar();
                    continue;
                }
                number += current();
                nextchar();
            }
         
            if (current() == '.') {
                number += current();
                match('.');
                while (Char.IsDigit(current())) {
                    number += current();
                    nextchar();
                }
            }
            if (current() == 'e' || current() == 'E') {
                number += current();
                nextchar();
             
                if (current() == '-') {
                    number += current();
                    match('-');
                }
             
                while (Char.IsDigit(current())) {
                    number += current();
                    nextchar();
                }
            }
         
            try {
                return new ValueExpression(Double.Parse(number, CultureInfo.GetCultureInfo("en-US")));
            } catch (FormatException ex) {
                throw new ParserException("Can't parse [" + number + "]", ex);
            }
        }
     
        public char current()
        {
            if (index >= text.Length) {
                return '\0';
            } else {
                return text[index];
            }
        }

        public void nextchar()
        {
            index++;
        }
     
        public void skipws()
        {
            while (Char.IsWhiteSpace(current())) {
                nextchar();
            }
        }
 
        public void match(char c)
        {
            if (c == current()) {
                nextchar();
            } else {
                throw new ParserException("char " + c + " not found");
            }
        }
    }
}
