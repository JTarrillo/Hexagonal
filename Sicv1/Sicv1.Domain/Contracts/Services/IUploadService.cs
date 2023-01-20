using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Services
{
	public interface IUploadService
	{
		Task Insert(Upload upload);
	}
}
