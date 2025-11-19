using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClubRegistration
{
    internal class ClubRegistrationQuery
    {
        private SqlConnection sqlConnect;
        private SqlConnection sqlCommand;
        private SqlDataAdapter sqlDataAdapter;
        private SqlDataAdapter sqlAdapter;
        private SqlDataReader sqlDataReader;

            public DataTable dataTable;
            public BindingSource bindingSource;

            private string connectionString;

            public ClubRegistrationQuery()
        {
            connectionString = @"Data Source=LAB-A-PC00;
            Initial Catalog=ClubRegistration;
            User ID=2000363245;
            Password=12345;";

            sqlConnect = new SqlConnection(connectionString);
            dataTable = new DataTable();
            bindingSource = new BindingSource();
        }
        public bool DisplayList()

        {
            string query = "SELECT ID, FirstName, MiddleName, LastName, Age, Gender, Program FROM ClubMembers";

            sqlAdapter = new SqlDataAdapter(query, sqlConnect);

            dataTable.Clear();
            sqlAdapter.Fill(dataTable);
            bindingSource.DataSource = dataTable;

            return true;
        }

        public bool RegistrationStudent(int ID, long StudentID, string FirstName, String MiddleName, String LastName, int Age, String Gender, String Program )
        {
            sqlCommand = new SqlConnection("INSERT INTO ClubMembers VALUE(@ID, @StudentID, @FirstName, @MiddleName, @LastName, @Age, @Gender, @Program)");
            sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
           sqlCommand.Parameters.Add("@StudentID", SqlDbType.BigInt).Value = StudentID;
           sqlCommand.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = FirstName;
           sqlCommand.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = MiddleName;
           sqlCommand.Parameters.Add("@LastName", SqlDbType.VarChar).Value = LastName;
           sqlCommand.Parameters.Add("@Age", SqlDbType.Int).Value = Age;

            sqlConnect.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnect.Close();

            return true;

        }

        public bool UpdateMember(long StudentID, string FirstName, string MiddleName, string LastName, int Age, string Gender, string Program)
        {
            
        }
    }
}
