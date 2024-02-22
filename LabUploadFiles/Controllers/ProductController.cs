using LabUploadFiles.Models.Domain;
using LabUploadFiles.Models.DTO;
using LabUploadFiles.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabUploadFiles.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IProductRepository _productRepo;

        public ProductController(IFileService fs, IProductRepository productRepo)
        {
            _fileService = fs;
            _productRepo = productRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] Product model)
        {
            var status = new Status();
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please pass the valid data";
                return Ok(status);
            }

            if (model.ImageFile != null)
            {
                var fileResult = _fileService.SaveImage(model.ImageFile);
                if (fileResult.Item1 == 1)
                {
                    model.ProductImage = fileResult.Item2; // getting name of image
                }
                var productResult = await _productRepo.Add(model);
                if (productResult)
                {
                    status.StatusCode = 1;
                    status.Message = "Added successfully";
                } 
                else
                {
                    status.StatusCode = 0;
                    status.Message = "Error on adding product";
                }                
            }
            return Ok(status);
        }
    }
}
