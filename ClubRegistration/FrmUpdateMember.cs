using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ClubRegistration
{
    public partial class FrmUpdateMember : Form
    {
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private SqlDataReader sqlReader;
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\CLUBDB.mdf;Integrated Security=True";
      

        public FrmUpdateMember()
        {
            InitializeComponent();

         
            try
            {
                
            }
            catch (Exception ex)
            {
               
                MessageBox.Show("Error during FrmUpdateMember initialization: " + ex.Message, "Initialization Failure");
            }
        }

       
        

        private void LoadStudentIds()
        {
            cbID.Items.Clear();
            sqlConnect.Open();
            sqlCommand = new SqlCommand("SELECT StudentId FROM ClubMembers", sqlConnect);
            sqlReader = sqlCommand.ExecuteReader();
            while (sqlReader.Read())
            {
                cbID.Items.Add(sqlReader["StudentId"].ToString());
            }
            sqlReader.Close();
            sqlConnect.Close();
        }

        private void FrmUpdateMember_Load(object sender, EventArgs e)
        {
            sqlConnect = new SqlConnection(connectionString);
            LoadStudentIds();
            cbGender.Items.AddRange(new string[] { "Male", "Female" });
            cbPrograms.Items.AddRange(new string[] { "BS Information Technology", "BS Computer Science", "BS Information Systems",
            "BS Accountancy", "BS Hospitality Management", "BS Tourism Management"});

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            sqlConnect.Open();
            sqlCommand = new SqlCommand("UPDATE ClubMembers SET FirstName=@FirstName, MiddleName=@MiddleName, LastName=@LastName, Age=@Age, Gender=@Gender, Program=@Program WHERE StudentId=@StudentId", sqlConnect);
            sqlCommand.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
            sqlCommand.Parameters.AddWithValue("@MiddleName", txtMiddleInitial.Text);
            sqlCommand.Parameters.AddWithValue("@LastName", txtLastName.Text);
            sqlCommand.Parameters.AddWithValue("@Age", int.Parse(txtAge.Text));
            sqlCommand.Parameters.AddWithValue("@Gender", cbGender.Text);
            sqlCommand.Parameters.AddWithValue("@Program", cbPrograms.Text);
            sqlCommand.Parameters.AddWithValue("@StudentId", cbID.Text);

            sqlCommand.ExecuteNonQuery();
            sqlConnect.Close();

            MessageBox.Show("Member updated successfully!");
        }
        private void cmbStudentId_SelectedIndexChanged(object sender, EventArgs e)
        {

            sqlConnect.Open();
            sqlCommand = new SqlCommand("SELECT * FROM ClubMembers WHERE StudentId=@StudentId", sqlConnect);
            sqlCommand.Parameters.AddWithValue("@StudentId", cbID.Text);
            sqlReader = sqlCommand.ExecuteReader();
            if (sqlReader.Read())
            {
                txtFirstName.Text = sqlReader["FirstName"].ToString();
                txtMiddleInitial.Text = sqlReader["MiddleName"].ToString();
                txtLastName.Text = sqlReader["LastName"].ToString();
                txtAge.Text = sqlReader["Age"].ToString();
                cbGender.Text = sqlReader["Gender"].ToString();
                cbPrograms.Text = sqlReader["Program"].ToString();
            }
            sqlReader.Close();
            sqlConnect.Close();
        }
    }
}
