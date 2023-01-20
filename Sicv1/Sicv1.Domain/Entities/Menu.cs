using System.Collections.Generic;

namespace Sicv1.Domain.Entities
{
    public class Menu : BaseClass
    {
        public int? ID_PARENT { get; set; }
        public string NAME { get; set; } = string.Empty;
        public string DESCRIPTION { get; set; } = string.Empty;
        public string CONTROLLER { get; set; } = string.Empty;
        public string ACTION { get; set; } = string.Empty;
        public string CLASS_AHREF { get; set; } = string.Empty;
        public string CLASS_I { get; set; } = string.Empty;
        public string RAWURL { get; set; } = string.Empty;
    }
}