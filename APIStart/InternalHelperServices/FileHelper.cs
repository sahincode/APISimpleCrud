

namespace APIStart.InternalHelperServices
{
    public class FileHelper
    {
         public async static Task<string> SaveImage(string rootPath ,string passPath ,IFormFile image)
        {
            var folderImage = Path.Combine( rootPath, passPath);
           
            if (!Directory.Exists(folderImage) )
            {
                Directory.CreateDirectory(folderImage);
              
            }
            var imageName = Guid.NewGuid().ToString() + image.FileName;
            
            var fileFullPath = Path.Combine(folderImage, imageName);
           
            using (var FileStream = new FileStream(fileFullPath, FileMode.Create))
            {
                await image.CopyToAsync(FileStream);
            }
          
            return imageName;
        }
        public async  static Task<string> SaveVideo(string filePath, string passPath, IFormFile video)
        {
           
            var FolderVideo = Path.Combine(filePath ,passPath);
            if ( !Directory.Exists(FolderVideo))
            {
                
                Directory.CreateDirectory(FolderVideo);
            }
            var videoName = Guid.NewGuid().ToString() + video.FileName;
           
            var fileFullPath = Path.Combine(FolderVideo, videoName);
        
            using (var FileStream = new FileStream(fileFullPath, FileMode.Create))
            {
                await video.CopyToAsync(FileStream);
            }
           
            return videoName;
        }
    }
}
