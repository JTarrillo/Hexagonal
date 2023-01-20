using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Services
{
    public class NewsNessService : INewsNessService
    {
        private readonly INewsNessRepository _newsNessRepository;
        public NewsNessService(INewsNessRepository newsNessRepository)
        {
            this._newsNessRepository = newsNessRepository;
        }

        public async Task<IEnumerable<NewsNess>> GetNewsNesses()
        {
            try
            {
                return await _newsNessRepository.GetNewsNesses();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<NewsNessType>> GetNewsNessType()
        {
            try
            {
                return await _newsNessRepository.GetNewsNessType();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> SaveNewsness(NewsNess NewsNess)
        {
            try
            {
                return await _newsNessRepository.SaveNewsness(NewsNess);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdateNewsness(NewsNess NewsNess)
        {
            try
            {
                return await _newsNessRepository.UpdateNewsness(NewsNess);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> DeleteNewsness(NewsNess NewsNess)
        {
            try
            {
                return await _newsNessRepository.DeleteNewsness(NewsNess);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<int> SaveUpdate(NewsNess model)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _newsNessRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
