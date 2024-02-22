using LabUploadFiles.Models.Domain;
using System.Threading.Tasks;

namespace LabUploadFiles.Repository.Abstract
{
    public interface IProductRepository
    {
        Task<bool> Add(Product model);
    }
}
