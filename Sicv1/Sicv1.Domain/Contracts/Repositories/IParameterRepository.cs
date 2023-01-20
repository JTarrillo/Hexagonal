using Sicv1.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Repositories
{
    public interface IParameterRepository : IRepositoryBase<Parameter>
    {
        Task<int> Save(Parameter parameter);
        Task<int> Update(Parameter parameter);
        Task<IEnumerable<Parameter>> GetParameter(Parameter parameter);
        Task<int> DeleteParameter(Parameter parameter);
        Task<int> ActiveParameter(Parameter parameter);
        Task<int> UpdateStatus(int id);


    }
}
