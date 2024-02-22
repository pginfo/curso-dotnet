using LabUploadFiles.Models.Domain;
using LabUploadFiles.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabUploadFiles.Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContext _context;

        public ProductRepository(DatabaseContext context)
        {
            this._context = context;
        }

        public async Task<bool> Add(Product model)
        {
            try
            {
                await _context.Products.AddAsync(model);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
