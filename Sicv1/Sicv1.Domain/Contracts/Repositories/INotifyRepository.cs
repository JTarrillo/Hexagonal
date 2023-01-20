using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Repositories
{
	public interface INotifyRepository : IDisposable
	{
		Task<List<string>> Send(Notification notification);//se añade segments como variable de array de segmentos
        Task<User> getDatafromDocument(User objUser);
    }
}
