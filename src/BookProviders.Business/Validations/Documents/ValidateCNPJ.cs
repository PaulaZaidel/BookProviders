using System.Linq;

namespace BookProviders.Business.Validations.Documents
{
    public class ValidateCNPJ
    {
        public const int cnpjSize = 14;

        public static bool Validate(string cnpj)
        {
            var cnpjNumbers = UtilsValidate.OnlyNumbers(cnpj);

            if (!IsSizeValid(cnpjNumbers))
                return false;

            return !InvalidNumbers(cnpjNumbers) && IsValid(cnpjNumbers);
        }

        private static bool IsSizeValid(string value)
        {
            return value.Length == cnpjSize;
        }

        private static bool InvalidNumbers(string value)
        {
            string[] invalidNumbers =
            {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999",
            };

            return invalidNumbers.Contains(value);
        }

        private static bool IsValid(string value)
        {
            var number = value.Substring(0, cnpjSize - 2);
            var checkerNumbers = new CheckerNumbers(number)
                .WithMultipliers(2, 9)
                .Replacing("0", 10, 11);
            var firstNumber = checkerNumbers.CalculateNumber();
            checkerNumbers.AddNumber(firstNumber);
            var secondNumber = checkerNumbers.CalculateNumber();

            return string.Concat(firstNumber, secondNumber) == value.Substring(cnpjSize - 2, 2);
        }
    }
}
