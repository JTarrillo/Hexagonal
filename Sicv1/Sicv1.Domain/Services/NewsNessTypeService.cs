using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Services
{
    public class NewsNessTypeService : INewsNessTypeService
    {
        private readonly INewsNessTypeRepository _newsNessTypeRepository;
        public NewsNessTypeService(INewsNessTypeRepository newsNessTypeRepository)
        {
            this._newsNessTypeRepository = newsNessTypeRepository;
        }

        public async Task<IEnumerable<NewsNessType>> GetNewsNessesType()
        {
            try
            {
                return await _newsNessTypeRepository.GetNewsNessesType();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            _newsNessTypeRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
