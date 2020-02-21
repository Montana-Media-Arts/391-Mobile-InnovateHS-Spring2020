using InnovateServer.App_Code.Database;
using InnovateServer.App_Code.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InnovateServer
{
    public partial class Server : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Inserts a new student into the database with the provided data.
        [WebMethod]
        public static string insertStudent(string firstName, string lastName, string email, string password, string school = null)
        {
            Student newStudent = new Student();
            newStudent.FirstName = firstName;
            newStudent.LastName = lastName;
            newStudent.Email = email;
            newStudent.School = school;

            //Encode password as bytes for varbinary
            UTF8Encoding encoder = new UTF8Encoding();
            Byte[] passwordBytes = encoder.GetBytes(password);
            newStudent.Password = passwordBytes;

            //Let the database begin
            StudentsTable studentsTable = new StudentsTable(new DatabaseConnection());
            if(studentsTable.checkEmail(newStudent.Email)) return "A user with this email already exists.";
            else studentsTable.insertStudent(newStudent);

            //Verify student was entered into database and return a useful message to the frontend guys for testing
            Student verifyStudent = studentsTable.authenticateStudent(newStudent);
            if (verifyStudent != null) return "The student was added to and retrieved from the database successfully.";
            else return "The student's retrieval from the database was unsuccessful.";

        }
    }
}