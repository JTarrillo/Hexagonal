using System.Collections.Generic;

namespace Sicv1.Domain.Entities
{
    public class PersonRewards : BaseEntity
    {
        public string DOCUMENT{ get; set; }
        public int ID_CHARGE { get; set; }
        public string NAME_CHARGE { get; set; }
        public string NAME { get; set; }
        public string LASTNAME_P{ get; set; }
        public string LASTNAME_M { get; set; }
        public bool USED { get; set; }
        public string USED_CREATED_AT { get; set; }

        public List<CompanyReward> COMPANIES { get; set; }
    }


    public class CompanyReward
    {
        public int ID{ get; set; }
        public string NAME { get; set; }
        public bool USED { get; set; }
        public string CREATED_AT { get; set; }
    }

    public class PersonRewardsAction
    {
        public string DOCUMENTS { get; set; }
        public string COMPANIES{ get; set; }
        public string NAME_CHARGE { get; set; }
        public int UPDATED_USER { get; set; }
    }
}
