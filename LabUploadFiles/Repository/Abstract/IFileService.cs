using Microsoft.AspNetCore.Http;
using System;

namespace LabUploadFiles.Repository.Abstract
{
    public interface IFileService
    {
        public Tuple<int, string> SabeImage(IFormFile imageFile);
        public bool DeleteImage(string imageFileName);
    }
}
