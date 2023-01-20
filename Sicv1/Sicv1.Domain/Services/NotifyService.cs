using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicv1.Domain.Services
{
	public class NotifyService : INotifyService
	{
		private readonly INotifyRepository notifyRepository;
		public NotifyService(INotifyRepository notifyRepository)
		{
			this.notifyRepository = notifyRepository;
		}

		public void Dispose()
		{
			notifyRepository.Dispose();
			GC.SuppressFinalize(this);
		}

		public async Task<List<string>> Send(Notification notification)
		{
			return await notifyRepository.Send(notification);
		}

        public async Task<User> getDatafromDocument(User objUser)
        {
            return await notifyRepository.getDatafromDocument(objUser);
        }
    }
}
