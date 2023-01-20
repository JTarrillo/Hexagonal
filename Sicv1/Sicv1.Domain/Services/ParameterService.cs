using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Services
{
    public class ParameterService : IParameterService
    {
        private readonly IParameterRepository _parameterRepository;
        public ParameterService(IParameterRepository parameterRepository)
        {
            this._parameterRepository = parameterRepository;
        }
        public void Dispose()
        {
            _parameterRepository.Dispose();
            GC.SuppressFinalize(this);
        }


        public async Task<int> Save(Parameter parameter)
        {
            try
            {
                /*-
                if (user.DOCUMENT == await ValidateNumDocument(user.DOCUMENT, user.ID))
                {
                    return -1;
                }
                */
                return await _parameterRepository.Save(parameter);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(Parameter parameter)
        {
            try
            {
                /*
                if (user.DOCUMENT == await ValidateNumDocument(user.DOCUMENT, user.ID)) 
                {
                    return -1;
                }
                */
                return await _parameterRepository.Update(parameter);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Parameter>> GetParameter(Parameter parameter)
        {
            try
            {
                return await _parameterRepository.GetParameter(parameter);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> DeleteParameter(Parameter parameter)
        {
            try
            {
                return await _parameterRepository.DeleteParameter(parameter);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> ActiveParameter(Parameter parameter)
        {
            try
            {
                return await _parameterRepository.ActiveParameter(parameter);
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
                return await _parameterRepository.UpdateStatus(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
