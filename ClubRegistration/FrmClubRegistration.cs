using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClubRegistration
{
    public partial class FrmClubRegistration : Form
    {
        private ClubRegistrationQuery clubRegistrationQuery;


        int ID, Age, count = 0;
        string FirstName, MiddleName, LastName, Gender, Program;
        long StudentId;





        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshListOfClubMembers();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                FrmUpdateMember updateForm = new FrmUpdateMember();
                updateForm.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening Update Form: " + ex.Message, "System Error");
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            long studentIdValue;

            int ageValue;







            string studentIdText = txtStudentNo.Text.Trim();

            if (!long.TryParse(studentIdText, out studentIdValue))

            {

                MessageBox.Show("Please enter a valid, whole number for Student ID.", "Input Error");

                return;

            }



            string ageText = txtAge.Text.Trim();

            if (!int.TryParse(ageText, out ageValue))
            {
                MessageBox.Show("Please enter a valid, whole number for Age.", "Input Error");
                txtAge.Focus();
                return;
            }


            try

            {

                ID = RegistrationID();

                StudentId = studentIdValue;

                FirstName = txtFirstName.Text;

                MiddleName = txtMiddleInitial.Text;

                LastName = txtLastName.Text;

                Age = int.Parse(txtAge.Text);

                Gender = cbGender.Text;

                Program = cbPrograms.Text;



                bool ok = clubRegistrationQuery.RegisterStudent(ID, StudentId, FirstName, MiddleName, LastName, Age, Gender, Program);



                if (ok)

                {

                    MessageBox.Show("Member Registered Successfully");

                    RefreshListOfClubMembers();

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show("Error: " + ex.Message);

            }
        }

        

        private int RegistrationID()
        {
            count++;
            return count;
        }

        private void RefreshListOfClubMembers()
        {
            clubRegistrationQuery.DisplayList();
            dataGridView1.DataSource = clubRegistrationQuery.bindingSource;
        }

        public FrmClubRegistration()
        {
            InitializeComponent();
    
            clubRegistrationQuery = new ClubRegistrationQuery();

            RefreshListOfClubMembers();
        }
        
        private void cbPrograms_SelectedIndexChanged(object sender, EventArgs e)
        {
      
        }

        private void FrmClubRegistration_Load(object sender, EventArgs e)
        {
          
       
            string[] ListOfProgram = new string[]
       {
            "BS Information Technology",
            "BS Computer Science",
            "BS Information System",
            "BS in Accountancy",
            "BS in Hospitality Management",
            "BS in Tourism Management"
       };

            for (int i = 0; i < 6; i++)
            {
                cbPrograms.Items.Add(ListOfProgram[i].ToString());
            }
            string[] ListOfGender = new string[]
       {
            "Male",
            "Female",

       };

            for (int i = 0; i < ListOfGender.Length; i++)
            {
                cbGender.Items.Add(ListOfGender[i].ToString());
            }

            clubRegistrationQuery = new ClubRegistrationQuery();
            RefreshListOfClubMembers();

        }
       
    }
    }

