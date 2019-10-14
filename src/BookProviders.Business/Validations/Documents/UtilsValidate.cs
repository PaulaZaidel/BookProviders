namespace BookProviders.Business.Validations.Documents
{
    public class UtilsValidate
    {
        public static string OnlyNumbers(string value)
        {
            var onlyNumbers = "";
            foreach (var s in value)
            {
                if (char.IsDigit(s))
                    onlyNumbers += s;
            }

            return onlyNumbers.Trim();
        }
    }
}
