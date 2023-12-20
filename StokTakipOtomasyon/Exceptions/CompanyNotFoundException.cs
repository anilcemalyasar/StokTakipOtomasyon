using System.Globalization;

namespace StokTakipOtomasyon.Exceptions
{
    public class CompanyNotFoundException : Exception
    {
        public CompanyNotFoundException() : base()
        {
        }

        public CompanyNotFoundException(string? message) : base(message)
        {
        }

        public CompanyNotFoundException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

    }
}
