using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Services
{
    public class BranchOfficesService : IBranchOfficesService
    {
        private readonly IBranchOfficesRepository _branchOfficesRepository;

        public BranchOfficesService(IBranchOfficesRepository branchOfficesRepository)
        {
            this._branchOfficesRepository = branchOfficesRepository;
        }

        public async Task<IEnumerable<BranchOffices>> GetBranchOffices()
        {
            try
            {
                return await _branchOfficesRepository.GetBranchOffices();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            _branchOfficesRepository.Dispose();
            GC.SuppressFinalize(this);
        }


        public async Task<int> SaveBranchOffices(BranchOffices branchOffices)
        {
            try
            {
                return await _branchOfficesRepository.SaveBranchOffices(branchOffices);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdateBranchOffices(BranchOffices branchOffices)
        {
            try
            {
                return await _branchOfficesRepository.UpdateBranchOffices(branchOffices);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> DeleteBranchOffices(BranchOffices branchOffices)
        {
            try
            {
                return await _branchOfficesRepository.DeleteBranchOffices(branchOffices);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
