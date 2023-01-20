namespace Sicv1.Domain.Entities
{
    public class Configuration : BaseClass
    {
        public string DATA_PRIVACY_POLICY { get; set; }
        public string TREATMENT_OF_PERSONAL_DATA { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string URL_LOGO { get; set; }
        public string URL_OFICIAL { get; set; }
        public string VERSION { get; set; }
        public Configuration()
        {
            DATA_PRIVACY_POLICY = string.Empty;
            TREATMENT_OF_PERSONAL_DATA = string.Empty;
            NAME = string.Empty;
            DESCRIPTION = string.Empty;
            URL_LOGO = string.Empty;
            URL_OFICIAL = string.Empty;
            VERSION = string.Empty;
        }
    }
    public class ConfigurationResponseBE
    {
        public string MESSAGE { get; set; }
        public bool STATUS { get; set; }
        public Configuration DATA { get; set; }

        public ConfigurationResponseBE()
        {
            MESSAGE = string.Empty;
            STATUS = false;
            DATA = new Configuration();
        }
    }
}
