using InnovateServer.App_Code.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InnovateServer.App_Code.Database
{
    public class StudentsTable
    {
        protected DatabaseConnection database;

        public StudentsTable(DatabaseConnection database)
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
            parameters[4] = new SqlParameter("password", student.Password);

            database.uploadCommand(query, parameters);
        }

        //Updates the provided Student in the DB.
        public void updateNPC(Student student)
        {
            string query = "spUpdateStudent";
            SqlParameter[] parameters = new SqlParameter[12];
            parameters[0] = new SqlParameter("studentID", student.StudentID);
            parameters[1] = new SqlParameter("school", student.School);
            parameters[2] = new SqlParameter("firstName", student.FirstName);
            parameters[3] = new SqlParameter("lastName", student.LastName);
            parameters[4] = new SqlParameter("email", student.Email);

            database.uploadCommand(query, parameters);
        }


        //Gets a specific user using the passed parameters
        public Student authenticateStudent(Student student)
        {
            //Make query
            string query = "spAuthenticateStudent";
            //Obtain Parameters
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("email", student.Email);    //Set sql parameter 1 (with sql name of "userID"), with a value of userID
            parameters[1] = new SqlParameter("password", student.Password);    //Set sql parameter 1 (with sql name of "userID"), with a value of userID

            //Retrieve Data
            DataSet data = database.downloadCommand(query, parameters);

            //Return whether or not the db found the user by returning the userID or 0.
            if (data.Tables[0].Rows.Count == 1)
            {
                student.StudentID = (Int32)data.Tables[0].Rows[0]["studentID"];
                student.School = data.Tables[0].Rows[0]["school"].ToString();
                student.FirstName = data.Tables[0].Rows[0]["firstName"].ToString();
                student.LastName = data.Tables[0].Rows[0]["lastName"].ToString();

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