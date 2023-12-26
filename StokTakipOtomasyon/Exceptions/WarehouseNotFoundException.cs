using System.Globalization;

namespace StokTakipOtomasyon.Exceptions
{
    public class WarehouseNotFoundException : Exception
    {
        public WarehouseNotFoundException() : base()
        {
        }

        public WarehouseNotFoundException(string? message) : base(message)
        {
        }

        public WarehouseNotFoundException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
