using ThePetShopApp.Models;

namespace ThePetShopApp.Servises
{
    public interface IImageManager
    {
        public string CopyImage(Animal animal);
        public bool DeleteImage(string fileName);
        public bool ImageExists(string fileName, out string path);
        void UpdateImage(Animal animal, IFormFile pictureFile);
        
    }
}
