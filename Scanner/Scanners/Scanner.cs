using System;
using System.Text.RegularExpressions;

using Scanners.Exceptions;

namespace Scanners
{

    /// <summary>
    /// General use class containing common method used by all scanners
    /// </summary>
    public class Scanner
    {

        // Fields

        /// <summary>
        /// Contains a copy of the current patten being used to match the
        /// working string
        /// </summary>
        private Patten _current_active_patten;
        
        /// <summary>
        /// Continan the matches that will be returned by a next* method call
        /// </summary>
        private Match _current_match;

        /// <summary>
        /// Contains the match for the next item to be returned
        /// </summary>
        private Match _next_match;

        /// <summary>
        /// Boolean value indicated that there is nothing else to be processed
        /// </summary>
        private bool _string_fully_processed = false;

        /// <summary>
        /// This contains a copy of the remaining string that hasn't been
        /// returned in a next call
        /// </summary>
        protected string _working_string;
        
        // Constructors

        // Methods

        /// <summary>
        /// This method is used by the constructor in the set up phase and also
        /// when a next call is made for a different patten that the one
        /// currently in use
        /// </summary>
        /// <param name="inputString">
        /// Ether the inital string or a copy of the
        /// working string
        /// </param>
        /// <param name="patten">The desired patten to be used</param>
        protected void SetMatchs(string inputString, Patten patten)
        {
            Regex rx = new Regex(patten.ToString(),
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            
            _current_match = rx.Match(inputString);

            if(!_current_match.Success) throw new NoMatchFoundException();

            _next_match = _current_match.NextMatch();
            _current_active_patten = patten;
        }

        // TODO: Need to add the name paramiter to the Patten Class to tidy this
        // up.
        /// <summary>
        /// Method is called by a next method to get the current match and then
        /// update the current match and next match feilds to the next values.
        /// </summary>
        /// <param name="name">Paramiter name to be returned</param>
        /// <returns>The current match as a string</returns>
        private string GetMatch(string name)
        {
            try
            {
                _working_string = _working_string.Substring(_current_match.Index + _current_match.Length);
            } catch (ArgumentOutOfRangeException)
            {
                _string_fully_processed = true;
            }
            string tmp = _current_match.Groups[name].Value;
            _current_match = _next_match;
            _next_match = _current_match.NextMatch();
            return tmp;
        }

        /// <summary>
        /// Swaps the current match patten and updates the current match and
        /// next match. Should only be used when nessisary.
        /// </summary>
        /// <param name="patten">Desired patten</param>
        private void SwitchMatchs(Patten patten)
        {
            SetMatchs(_working_string,patten);
        }


        /// <summary>
        /// General perpous next method use to contain repeated code
        /// </summary>
        /// <param name="patten">Patten of what you would like returned</param>
        /// <param name="name">Name of the regex variable</param>
        /// <returns>String version of the match made by the regex</returns>
        private string Next(Patten patten, string name)
        {
            if(_current_active_patten.Equals(patten))
            {
                if(_current_match.Success == true)
                {
                    return GetMatch(name);
                } else
                {
                    throw new NoMoreDataException("There is no more lines left to return");
                }
            } else 
            {
                SwitchMatchs(patten);
                return Next(patten,name);
            }
        }

        /// <summary>
        /// This method will return the next line from the input made in the
        /// constructor. Line are seporated the the \n char.
        /// </summary>
        /// <returns></returns>
        public string NextLine()
        {
            return Next(Patten.NEW_LINE_PATTEN,"line");
        }

        /// <summary>
        /// Returns the next Int in the remaining input
        /// </summary>
        /// <returns></returns>
        public int NextInt()
        {
            try
            {
                return Convert.ToInt32(Next(Patten.INTGER_PATTEN,"integer"));
            } catch (NoMatchFoundException)
            {
                throw new NoMatchFoundException("There was no integer found in the remaining string");
            }
        }

        /// <summary>
        /// Returns the next double in the remaining string
        /// </summary>
        /// <returns></returns>
        public double NextDouble()
        {
            try
            {
                return Convert.ToDouble(Next(Patten.DOUBLE_PATTEN,"double"));
            } catch (NoMatchFoundException)
            {
                throw new NoMatchFoundException("There was no double found in the remaining string");
            }
        }

        /// <summary>
        /// General purpous HasNext method that returns based on the Patten paramiter
        /// </summary>
        /// <param name="patten"></param>
        /// <returns></returns>
        private bool HasNext(Patten patten)
        {
            if(_current_active_patten.Equals(patten))
            {
                return _next_match.Success;
            } else
            {
                return new Regex(patten.ToString(),
                    RegexOptions.Compiled | RegexOptions.IgnoreCase).Match(_working_string).Success;
            }
        }

        /// <summary>
        /// Method checks to see if there is another line to be returned after
        /// the current one. Note: this method dose not shift the possition of
        /// the current line, only a <code>Next*()</code> can do that.
        /// </summary>
        /// <returns></returns>
        public bool HasNextLine()
        {
            return HasNext(Patten.NEW_LINE_PATTEN);
        }

        /// <summary>
        /// Method checks to see if there is another Integer (32-bit) to be returned after
        /// the current one. Note: this method dose not shift the possition of
        /// the current line, only a <code>Next*()</code> can do that.
        /// </summary>
        /// <returns></returns>
        public bool HasNextInt()
        {
            return HasNext(Patten.INTGER_PATTEN);
        }

        /// <summary>
        /// Method checks to see if there is another Double to be returned after
        /// the current one. Note: this method dose not shift the possition of
        /// the current line, only a <code>Next*()</code> can do that.
        /// </summary>
        /// <returns></returns>
        public bool HasNextDouble()
        {
            return HasNext(Patten.DOUBLE_PATTEN);
        }

    }

    /// <summary>
    /// Class contains all the Pattens that are used for the regex.These can be
    /// thought of as advanced ENUMS.
    /// </summary>
    public sealed class Patten
    {
        public static readonly Patten NEW_LINE_PATTEN = new Patten(@"(?<line>[^\t\n\v\r$]+)");

        public static readonly Patten INTGER_PATTEN = new Patten(@"(?<integer>-?[0-9]+)");

        public static readonly Patten DOUBLE_PATTEN = new Patten(@"(?<double>-?[0-9]+.[0-9]+)");

        private string _patten;

        Patten(string patten) => _patten = patten; 

        public override string ToString()
        {
            return _patten;
        }

        public bool Equals(Patten other)
        {
            return other !=null && _patten == other.ToString();
        }

    } 
}
