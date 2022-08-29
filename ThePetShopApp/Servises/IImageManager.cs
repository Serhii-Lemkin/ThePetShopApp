namespace ThePetShopApp.Servises
{
    public interface IImageManager
    {
        public string CopyImage(IFormFile imageName, out string path);
        public bool DeleteImage(string fileName);
        public bool ImageExists(string fileName, out string picPath);
    }
}
