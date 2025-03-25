namespace WebTinTuc.Services
{
    public static class ImageUtil
    {
        public static string SaveImage(IFormFile image, IWebHostEnvironment environment)
        {
            if (image == null || image.Length == 0)
                return null;

            string uploadsFolder = Path.Combine(environment.WebRootPath, "images");
            string fileName = Path.GetFileNameWithoutExtension(image.FileName);
            string extension = Path.GetExtension(image.FileName);
            string uniqueFileName = fileName + extension;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            int counter = 1;
            while (File.Exists(filePath))
            {
                uniqueFileName = $"{fileName}_{counter}{extension}";
                filePath = Path.Combine(uploadsFolder, uniqueFileName);
                counter++;
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }

            return uniqueFileName;
        }
    }
}
