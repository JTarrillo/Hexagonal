using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyService(ICompanyRepository companyRepository)
        {
            this._companyRepository = companyRepository;
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            try
            {
                return await _companyRepository.GetCompanies();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Company>> GetCompaniesByUserId(int Id)
        {
            try
            {
                return await _companyRepository.GetCompaniesByUserId(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<Company>> GetCompaniesAll()
        {
            try
            {
                return await _companyRepository.GetCompaniesAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Company>> listCompanies()
        {
            try
            {
                return await _companyRepository.listCompanies();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Remove(int Id)
        {
            try
            {
                return await _companyRepository.Remove(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Save(Company company)
        {
            try
            {
                return await _companyRepository.Save(company);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(Company company)
        {
            try
            {
                return await _companyRepository.Update(company);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdateStatus(int id)
        {
            try
            {
                return await _companyRepository.UpdateStatus(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Dispose()
        {
            _companyRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 20191127 215400
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public async Task<int> CheckIfHaveLifeMiles(int CompanyId)
        {
            try
            {
                return await _companyRepository.CheckIfHaveLifeMiles(CompanyId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}