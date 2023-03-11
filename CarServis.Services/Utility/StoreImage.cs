using CarServis.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServis.Services.Utility
{
    // This static class contains static method for saving uploaded image to wwwroot folder
    public static class StoreImage
    {
        public static string SaveImage(BaseViewModel model,string imagesFolder)
        {
            string imageFileName = null;

            if (model.Image != null)
            {
                StringBuilder sb = new();

                sb.Append(Guid.NewGuid().ToString().Substring(0, 10));
                sb.Append("_");
                sb.Append(model.Image.FileName);
                imageFileName = sb.ToString();
                string filePath=Path.Combine(imagesFolder,imageFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                model.Image.CopyTo(fileStream);
            }
            return imageFileName;
        }
    }
}
