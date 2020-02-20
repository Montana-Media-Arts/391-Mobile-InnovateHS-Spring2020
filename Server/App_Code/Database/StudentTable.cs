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
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("school", student.School);
            parameters[1] = new SqlParameter("firstName", student.FirstName);
            parameters[2] = new SqlParameter("lastName", student.LastName);
            parameters[3] = new SqlParameter("email", student.Email);
            parameters[4] = new SqlParameter("password", student.FirstName);

            database.uploadCommand(query, parameters);
        }
    }
}