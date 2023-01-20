namespace Sicv1.Domain.Entities
{
    public class UserCompany : BaseClass
    {
        /// FOREIGN KEYS: ID_COMPANY, ID_USER
        public int ID_COMPANY { get; set; }
        public int ID_USER { get; set; }
    }
}
