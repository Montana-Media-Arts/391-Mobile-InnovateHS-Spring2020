using InnovateServer.App_Code.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InnovateServer.App_Code.Database
{
    public class SchoolTable
    {
        protected DatabaseConnection database;

        public SchoolTable(DatabaseConnection database)
        {
            this.database = database;
        }

        //Returns all of the Topics by id and name.
        public List<School> getOriginalSchools()
        {
            //Make query
            string query = "spGetOriginalSchools";

            //Retrieve Data
            DataSet data = database.downloadCommand(query);

            //Assemble the List
            List<School> schools = new List<School>();

            //Return useful data
            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                int schoolID = (Int32)data.Tables[0].Rows[i]["schoolID"];
                string schoolName = data.Tables[0].Rows[i]["schoolName"].ToString();
                bool isOther = Convert.ToBoolean(data.Tables[0].Rows[0]["isOther"]);

                School school = new School();
                school.SchoolID = schoolID;
                school.SchoolName = schoolName;
                school.IsOther = isOther;
                schools.Add(school);
            }

            return schools;
        }


        //Inserts a new school into the database
        public int insertSchool(School school)
        {
            string query = "spInsertSchool";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("schoolName", school.SchoolName);
            parameters[1] = new SqlParameter("isOther", school.IsOther);

            SqlParameter outputVal = new SqlParameter("@outputID", SqlDbType.Int);
            outputVal.Direction = ParameterDirection.Output;

            return database.uploadAndReturnCommand(query, outputVal, parameters);
        }
    }
}