using System.Globalization;

namespace StokTakipOtomasyon.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException() : base()
        {
        }

        public ProductNotFoundException(string? message) : base(message)
        {
        }

        public ProductNotFoundException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }


    }
}
