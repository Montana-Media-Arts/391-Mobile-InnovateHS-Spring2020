using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InnovateServer.App_Code.Database
{
    public class MadLibTable
    {
        protected DatabaseConnection database;

        public MadLibTable(DatabaseConnection database)
        {
            this.database = database;
        }

        //Inserts a student's madlib into the database
        public void insertMadLibs(string aResponse1, string aResponse2, string bVerb1, string bVerb2, string bAdverb, int studentID)
        {
            string query = "spInsertMadLibs";
            SqlParameter[] parameters = new SqlParameter[6];
            parameters[0] = new SqlParameter("aResponse1", aResponse1);
            parameters[1] = new SqlParameter("aResponse2", aResponse2);
            parameters[2] = new SqlParameter("bVerb1", bVerb1);
            parameters[3] = new SqlParameter("bVerb2", bVerb2);
            parameters[4] = new SqlParameter("bAdverb", bAdverb);
            parameters[5] = new SqlParameter("studentID", studentID);

            database.uploadCommand(query, parameters);
        }
    }
}