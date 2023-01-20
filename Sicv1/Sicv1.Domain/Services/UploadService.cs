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
	public class UploadService : IUploadService
	{
		private readonly IUploadsRepository uploadsRepository;
		public UploadService(IUploadsRepository uploadsRepository)
		{
			this.uploadsRepository = uploadsRepository;
		}

		public async Task Insert(Upload upload)
		{
			await uploadsRepository.Insert(upload);
		}
	}
}
