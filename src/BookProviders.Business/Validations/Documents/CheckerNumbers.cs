
using System;
using System.Collections.Generic;

namespace BookProviders.Business.Validations.Documents
{
    public class CheckerNumbers
    {
        private string _number;
        private const int Module = 11;
        private readonly List<int> _multipliers = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9 };
        private readonly IDictionary<int, string> _replacement = new Dictionary<int, string>();
        private bool _complementToModule = true;

        public CheckerNumbers(string number)
        {
            _number = number;
        }

        public CheckerNumbers WithMultipliers(int firstMultiplier, int lastMultiplier)
        {
            _multipliers.Clear();
            for (var i = firstMultiplier; i <= lastMultiplier; i++)
                _multipliers.Add(i);
            
            return this;
        }

        public CheckerNumbers Replacing(string substitute, params int[] numbers)
        {
            foreach (var i in numbers)
                _replacement[i] = substitute;
                
            return this;
        }

        public void AddNumber(string number)
        {
            _number = string.Concat(_number, number);
        }

        public string CalculateNumber()
        {
            return !(_number.Length > 0) ? "" : GetNumberSum(); 
        }

        private string GetNumberSum()
        {
            var sum = 0;
            for (int i = _number.Length - 1, j = 0; i >= 0; i--)
            {
                var product = (int)char.GetNumericValue(_number[i]) * _multipliers[j];
                sum += product;

                if (++j >= _multipliers.Count)
                    j = 0;
            }

            var mod = (sum % Module);
            var result = _complementToModule ? Module - mod : mod;

            return _replacement.ContainsKey(result) ? _replacement[result] : result.ToString();

        }
    }
}
