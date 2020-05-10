﻿using System;
using System.Text.RegularExpressions;

using ScannerUtil.Exceptions;

namespace ScannerUtil
{
    public sealed class Scanner
    {

        // Fields
        
        // Continan the matches that will be returned by a next* method call
        private Match _current_match;

        // Contains the match for the next item to be returned
        private Match _next_match;

        private bool _no_matches_left;

        private string _copy_of_input_string;
        
        // Constructors

        /// <summary>
        /// This constructor takes a single string as a paramitor, by default
        /// the string will the c# regex enging split it by lines. In this
        /// implmentation a new line is represened by a \t, \n, \v or \r. You
        /// can acsess each line in tern by using the 
        /// <code>
        /// nextline()
        /// </code> method.
        /// </summary>
        /// <param name="inputString">This is the string you would like
        /// processed
        /// </param>
        /// <exception
        /// cref="ScannerUtil.Exceptions.InvalidArgumentException">This
        /// exception will be thrown if an empty string is passed to the
        /// constructor
        /// </exception>
        public Scanner(string inputString)
        {
            if (inputString.Equals("")) throw new InvalidArgumentException("Empty string was handed to constructor");
            
            setMatchs(inputString,Patten.NEW_LINE_PATTEN);
        }

        // Methods

        private void setMatchs(string inputString, Patten patten)
        {
            Regex rx = new Regex(patten.ToString(),
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            
            _current_match = rx.Match(inputString);
            _next_match = _current_match.NextMatch();
        }

        /// <summary>
        /// This method will return the next line from the input made in the
        /// constructor. Line are seporated the the \n char.
        /// </summary>
        /// <returns></returns>
        public string nextLine()
        {
            if(_current_match.Success == true)
            {
                string tmp = _current_match.Groups["line"].Value;
                _current_match = _next_match;
                _next_match = _current_match.NextMatch();
                return tmp;
            } else
            {
                throw new NoMoreDataException("There is no more lines left to return");
            }

        }

        /// <summary>
        /// Method checks to see if there is another line to be returned after
        /// the current one. Note: this method dose not shift the possition of
        /// the current line, only the <code>nextLine()</code> can do that.
        /// </summary>
        /// <returns></returns>
        public bool hasNextLine()
        {
            return _next_match.Success;
        }


        public int nextInt()
        {
            return 14;
        }
    }

    public sealed class Patten
    {
        public static readonly Patten NEW_LINE_PATTEN = new Patten(@"(?<line>[^\t\n\v\r$]+)");

        public static readonly Patten INTGER_PATTEN = new Patten(@"[0-9]+");

        private string _patten;

        Patten(string patten) => _patten = patten; 

        public override string ToString()
        {
            return _patten;
        }
    } 
}
