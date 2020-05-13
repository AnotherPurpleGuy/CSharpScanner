using System;
using System.Text.RegularExpressions;

using Scanners.Exceptions;

namespace Scanners
{
    public sealed class Scanner
    {

        // Fields

        private Patten _current_active_patten;
        
        // Continan the matches that will be returned by a next* method call
        private Match _current_match;

        // Contains the match for the next item to be returned
        private Match _next_match;

        private bool _string_fully_processed = false;

        private string _copy_of_construct_string;

        private string _working_string;
        
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
        /// cref="Scanners.Exceptions.InvalidArgumentException">This
        /// exception will be thrown if an empty string is passed to the
        /// constructor
        /// </exception>
        public Scanner(string inputString)
        {
            if (inputString.Equals("")) throw new InvalidArgumentException("Empty string was handed to constructor");
            _working_string = inputString;
            _copy_of_construct_string = inputString;
            setMatchs(inputString,Patten.NEW_LINE_PATTEN);
        }

        // Methods

        private void setMatchs(string inputString, Patten patten)
        {
            Regex rx = new Regex(patten.ToString(),
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            
            _current_match = rx.Match(inputString);

            if(!_current_match.Success) throw new NoMatchFoundException();

            _next_match = _current_match.NextMatch();
            _current_active_patten = patten;
        }

        private string getMatch(string name)
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

        private void switchMatchs(Patten patten)
        {
            setMatchs(_working_string,patten);
        }

        private string next(Patten patten, string name)
        {
            if(_current_active_patten.Equals(patten))
            {
                if(_current_match.Success == true)
                {
                    return getMatch(name);
                } else
                {
                    throw new NoMoreDataException("There is no more lines left to return");
                }
            } else 
            {
                switchMatchs(patten);
                return next(patten,name);
            }
        }

        /// <summary>
        /// This method will return the next line from the input made in the
        /// constructor. Line are seporated the the \n char.
        /// </summary>
        /// <returns></returns>
        public string nextLine()
        {
            return next(Patten.NEW_LINE_PATTEN,"line");
        }

        public int nextInt()
        {
            try
            {
                return Convert.ToInt32(next(Patten.INTGER_PATTEN,"integer"));
            } catch (NoMatchFoundException)
            {
                throw new NoMatchFoundException("There was no integer found in the remaining string");
            }
        }

        public double nextDouble()
        {
            try
            {
                return Convert.ToDouble(next(Patten.DOUBLE_PATTEN,"double"));
            } catch (NoMatchFoundException)
            {
                throw new NoMatchFoundException("There was no double found in the remaining string");
            }
        }

        private bool hasNext(Patten patten)
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
        /// the current line, only the <code>nextLine()</code> can do that.
        /// </summary>
        /// <returns></returns>
        public bool hasNextLine()
        {
            return hasNext(Patten.NEW_LINE_PATTEN);
        }

        public bool hasNextInt()
        {
            return hasNext(Patten.INTGER_PATTEN);
        }

        public bool hasNextDouble()
        {
            return hasNext(Patten.DOUBLE_PATTEN);
        }

    }

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
