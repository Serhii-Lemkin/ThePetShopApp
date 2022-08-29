namespace ThePetShopApp.Servises
{
    public interface IImageManager
    {
        public string AddImage(IFormFile imageName, out string path);
        public bool DeleteImage(string fileName);
    }
}
