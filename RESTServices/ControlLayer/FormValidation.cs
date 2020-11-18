using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTServices.ControlLayer {
    public class FormValidation {

        private readonly int _leastAmountOfCharacters = 4;
        private readonly int _mostAmountOfCharacters = 40;

        public bool UsernameValidation(string input) {
            bool result = false;
            if(LengthValidtion(input)) {
                if(!ContainsSpecialCharactersValidation(input)) {
                    result = true;
                }
            }
            return result;
        }

        public bool PasswordValidation(string input) {
            bool result = false;
            if(LengthValidtion(input)) {
                if(ContainsSpecialCharactersValidation(input)) {
                    if(ContainsNumbersValidation(input)) {
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool EmailValidation(string input) {
            bool result = false;
            if (LengthValidtion(input)) {
                if(input.Contains("@") && input.Contains(".")) {
                    result = true;
                }
            }
            return result;
        }

        public bool LengthValidtion(string input) {
            bool result = false;
            if(input.Length > _leastAmountOfCharacters && input.Length < _mostAmountOfCharacters) {
                result = true;
            }
            return result;
        }

        public bool LengthValidation(string input, int min, int max) {
            bool result = false;
            if (input.Length > min && input.Length < max) {
                result = true;
            }
            return result;
        }

        public bool ContainsSpecialCharactersValidation(string inputString) {
            bool result = false;
            if (inputString.Any(ch => !Char.IsLetterOrDigit(ch))) {
                result = true;
            }
            return result;
        }

        public bool ContainsNumbersValidation(string inputString) {
            bool result = false;
            if (inputString.Any(ch => Char.IsDigit(ch))) {
                result = true;
            }
            return result;
        }

        public bool IsEmailRegistered(string emailString) {
            bool result = false;
            return result;
        }

    }
}