using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contract.IService
{
    public interface ICategoryVariationService
    {
        Task<CategoryVariationViewModel> GetCategoryById(string CategoryVariationId);
        Task<IEnumerable<CategoryVariationViewModel>> GetCategoriesByCategoryId(string CategoryId);
        Task<int> CreateCategory(List<CategoryVariationViewModel> categories);
        Task<int> RemoveCategory(string CategoryVariationId);
        Task<IEnumerable<CategoryVariationViewModel>> GetAllCategoryVariation();
    }
}
