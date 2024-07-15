using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contract.IRepository
{
    public interface ICategoryVariationRepository
    {
        Task<CategoryVariation> GetCategoryById(string CategoryVariationId);
        Task<int> CreateCategory(CategoryVariation categories);
        Task<int> RemoveCategory(string CategoryVariationId);
        Task<IEnumerable<CategoryVariation>> GetAllCategoryVariation();
        Task<IEnumerable<CategoryVariation>> GetCategoryVarirationByCategoryId(string CategoryId);
    }
}
