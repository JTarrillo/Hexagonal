using System.Configuration;

namespace Sicv1.Infrastructure.Cross
{
    public class Connection
    {
        public string SqlConnectionString { get; private set; }
        public Connection()
        {
            SqlConnectionString = ConfigurationManager.ConnectionStrings["db_connection"].ToString();
        }
    }
}
