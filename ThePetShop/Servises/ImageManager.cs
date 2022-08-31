using ThePetShopApp.Models;

namespace ThePetShopApp.Servises
{
    public class ImageManager : IImageManager
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        public ImageManager(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        public string CopyImage(Animal animal)
        {
            string wwwrootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(animal.PictureFile!.FileName);
            string extention = Path.GetExtension(animal.PictureFile!.FileName);
             fileName = fileName + DateTime.Now.ToString("yymmddssfff") + extention;
            string path = Path.Combine(wwwrootPath + "/pictures/", fileName);
            CopyToRoot(animal, path);
            return fileName;
        }
        void CopyToRoot(Animal animal, string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                animal.PictureFile!.CopyToAsync(fileStream);
            }
        }

        public bool DeleteImage(string fileName)
        {
            if (ImageExists(fileName, out string picPath))
            {
                System.IO.File.Delete(picPath);
                return true;
            }
            return false;
        }

        public bool ImageExists(string fileName, out string picPath)
        {
            picPath = Path.Combine(_hostEnvironment.WebRootPath, "pictures", fileName);
            return System.IO.File.Exists(picPath);
        }
    }
}
