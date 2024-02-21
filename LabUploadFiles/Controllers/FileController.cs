using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LabUploadFiles.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] ICollection<IFormFile> files, CancellationToken cancellationToken)
        {
            return await WriteFile(files);
        }

        [HttpGet]
        [Route("download")]
        public async Task<IActionResult> Download(string filename)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Storage\Files", filename);

            FileExtensionContentTypeProvider provider = new ();

            if (!provider.TryGetContentType(filePath, out string contenttype))
                contenttype = "application/octet-stream";

            byte[] bytes = await System.IO.File.ReadAllBytesAsync(filePath);

            return File(bytes, contenttype, Path.GetFileName(filePath));
        }

        private async Task<IActionResult> WriteFile(ICollection<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return BadRequest("Nenhum arquivo foi enviado.");

            string storageFolder = Path.Combine(Directory.GetCurrentDirectory(), @"Storage\Files");            

            if (!Directory.Exists(storageFolder))
                Directory.CreateDirectory(storageFolder);

            foreach (var file in files)
            {
                using FileStream stream = new(Path.Combine(storageFolder, file.FileName), FileMode.Create);
                await file.CopyToAsync(stream);
            }

            return Ok("Arquivos foram gravados com sucesso.");
        }
    }
}
