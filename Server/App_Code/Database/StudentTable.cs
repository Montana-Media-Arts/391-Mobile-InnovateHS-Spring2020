using InnovateServer.App_Code.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace InnovateServer.App_Code.Database
{
    public class StudentTable
    {
        protected DatabaseConnection database;
        protected UTF8Encoding encoder = new UTF8Encoding();

        public StudentTable(DatabaseConnection database)
        {
            this.database = database;
        }

        public void insertStudent(Student student)
        {
            string query = "spInsertStudent";
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("school", student.School);
            parameters[1] = new SqlParameter("firstName", student.FirstName);
            parameters[2] = new SqlParameter("lastName", student.LastName);
            parameters[3] = new SqlParameter("email", student.Email);
            parameters[4] = new SqlParameter("password", encoder.GetBytes(student.Password));

            database.uploadCommand(query, parameters);
        }

        //Enrolls a student in a class
        public void insertStudentClass(Student student, int classID)
        {
            string query = "spInsertStudentClass";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("studentID", student.StudentID);
            parameters[1] = new SqlParameter("classID", classID);

            database.uploadCommand(query, parameters);
        }


        //Updates student's session enrollment
        public void updateStudentClass(Student student, int classID)
        {
            string query = "spUpdateStudentClass";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("studentID", student.StudentID);
            parameters[1] = new SqlParameter("classID", classID);

            database.uploadCommand(query, parameters);
        }

        //Updates the provided Student in the DB.
        public void updateStudentTopics(int studentID, int choiceOneID, int choiceTwoID)
        {
            string query = "spChooseTopics";
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("studentID", studentID);
            parameters[1] = new SqlParameter("topicChoice1", choiceOneID);
            parameters[2] = new SqlParameter("topicChoice2", choiceTwoID);

            database.uploadCommand(query, parameters);
        }


        //Updates the provided Student's feedback info.
        public void updateStudentFeedback(int studentID, string liked, string better, string other, bool wantOfferings)
        {
            string query = "spUpdateStudentFeedback";
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("studentID", studentID);
            parameters[1] = new SqlParameter("fbLiked", liked);
            parameters[2] = new SqlParameter("fbBetter", better);
            parameters[3] = new SqlParameter("fbWantOfferings", wantOfferings);
            parameters[4] = new SqlParameter("fbOther", other);
            database.uploadCommand(query, parameters);
        }


        //Updates the provided Student's feedback info.
        public void updateStudentRegistrationComplete(int studentID)
        {
            string query = "spUpdateStudentRegistrationComplete";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("studentID", studentID);
            database.uploadCommand(query, parameters);
        }


        //Gets a specific user using the passed parameters
        public Student authenticateStudent(Student student)
        {
            //Make query
            string query = "spAuthenticateStudent";
            //Obtain Parameters
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("email", student.Email);
            parameters[1] = new SqlParameter("password", encoder.GetBytes(student.Password));

            //Retrieve Data
            DataSet data = database.downloadCommand(query, parameters);

            //Return whether or not the db found the user by returning the student or null.
            if (data.Tables[0].Rows.Count == 1)
            {
                student.StudentID = (Int32)data.Tables[0].Rows[0]["studentID"];
                student.School = data.Tables[0].Rows[0]["school"].ToString();
                student.FirstName = data.Tables[0].Rows[0]["firstName"].ToString();
                student.LastName = data.Tables[0].Rows[0]["lastName"].ToString();
                student.RegistrationComplete = Convert.ToBoolean(data.Tables[0].Rows[0]["registrationComplete"]);

                return student;
            }
            else return null;
        }


        //Checks if Email exists in the db
        public bool checkEmail(string email)
        {
            string query = "spCheckEmail";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("email", email);

            DataSet data = database.downloadCommand(query, parameters);

            if (data.Tables[0].Rows.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}