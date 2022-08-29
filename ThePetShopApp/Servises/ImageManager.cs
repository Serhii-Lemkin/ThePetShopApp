namespace ThePetShopApp.Servises
{
    public class ImageManager : IImageManager
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        public ImageManager(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        public string CopyImage(IFormFile image, out string path)
        {
            string wwwrootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(image.FileName);
            string extention = Path.GetExtension(image.FileName);
             fileName = fileName + DateTime.Now.ToString("yymmddssfff") + extention;
            path = Path.Combine(wwwrootPath + "/pictures/", fileName);
            return fileName;
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
