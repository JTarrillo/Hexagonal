using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicv1.Domain.Entities
{
	public class Upload : Result
	{
		public int ID { get; set; }
		public int CREATED_USER { get; set; }
		public List<Code> CODES { get; set; } = new List<Code>();
	}
}
