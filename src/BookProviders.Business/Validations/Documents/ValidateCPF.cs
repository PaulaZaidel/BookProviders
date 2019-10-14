using System;
using System.Linq;

namespace BookProviders.Business.Validations.Documents
{
    public class ValidateCPF
    {
        public const int cpfSize = 11;

        public static bool Validate(string cpf)
        {
            var cpfNumbers = UtilsValidate.OnlyNumbers(cpf);

            if (!IsSizeValid(cpfNumbers))
                return false;

            return !InvalidNumbers(cpfNumbers) && IsValid(cpfNumbers);
        }

        private static bool IsValid(string value)
        {
            var number = value.Substring(0, cpfSize - 2);
            var checkerNumbers = new CheckerNumbers(number)
                .WithMultipliers(2, 11)
                .Replacing("0", 10, 11);
            var firstNumber = checkerNumbers.CalculateNumber();
            checkerNumbers.AddNumber(firstNumber);
            var secondNumber = checkerNumbers.CalculateNumber();

            return string.Concat(firstNumber, secondNumber) == value.Substring(cpfSize - 2, 2);
        }

        private static bool IsSizeValid(string value)
        {
            return value.Length == cpfSize;
        }

        private static bool InvalidNumbers(string value)
        {
            string[] invalidNumbers =
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999",
            };

            return invalidNumbers.Contains(value);
        }

    }
}
