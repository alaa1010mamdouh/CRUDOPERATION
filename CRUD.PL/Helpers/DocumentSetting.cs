using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace CRUD.PL.Helpers
{
    public static class DocumentSetting
    {
        public static string UploadFile(IFormFile File, string FolderName)
        {
            //Directory.GetCurrentDirectory() + "\\wwwroot\\Files\\" + FolderName;
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);
            string FileName = $"{Guid.NewGuid()} {File.FileName}";
            string FilePath = Path.Combine(FolderPath, FileName);
            using  var Fs = new FileStream( FilePath, FileMode.Create);
            File.CopyTo( Fs );
            return FileName;

        }
        //Delete
        public static void DeleteFile(string FileName,string FolderName)
        {
            //Get File Path
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName,FileName);

            if (File.Exists(FilePath)) 
            { 
                File.Delete(FilePath);
            
            }
        }
    }
}
