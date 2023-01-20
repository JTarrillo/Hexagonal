using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Sicv1.Domain.Services
{
    public class CategoryCardService : ICategoryCardService
    {
        private readonly ICategoryCardRepository _categoryCardRepository;
        public CategoryCardService(ICategoryCardRepository categoryCardRepository)
        {
            this._categoryCardRepository = categoryCardRepository;
        }

        public async Task<int> Save(CategoryCard categoryCard)
        {
            try
            {
                return await _categoryCardRepository.Save(categoryCard);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(CategoryCard categoryCard)
        {
            try
            {
                return await _categoryCardRepository.Update(categoryCard);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            _categoryCardRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
