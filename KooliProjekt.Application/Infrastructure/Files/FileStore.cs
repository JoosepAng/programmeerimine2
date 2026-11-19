using System.IO;

namespace KooliProjekt.Application.Infrastructure.Files
{
    public static class FileStore
    {
        public const string Photos = "photos";
        public const string Invoices = "invoices";

        public static string CarPhotos(int carId)
        {
            return Path.Combine(Photos, "cars", carId.ToString());
        }
    }
}