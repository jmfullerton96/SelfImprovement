using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfImprovement.Models
{
    class SqlBridge
    {
        readonly SqlConnection connection;

        public SqlBridge()
        {
            string connetionString = ConfigurationManager.ConnectionStrings["SelfImprovement.Properties.Settings.SelfImprovementConnectionString"].ConnectionString;
            this.connection = new SqlConnection(connetionString);
            this.connection.Open();
        }

        public SqlConnection GetConnection()
        {
            return this.connection;
        }

        ~SqlBridge()
        {
            //this.connection.Close(); // TODO - why does this throw an exception every time?
        }
    }
}
