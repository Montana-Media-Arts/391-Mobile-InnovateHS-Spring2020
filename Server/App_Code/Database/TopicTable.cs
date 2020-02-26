using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace InnovateServer.App_Code.Database
{
    public class TopicTable
    {
        protected DatabaseConnection database;

        public TopicTable(DatabaseConnection database)
        {
            this.database = database;
        }

        //Returns all of the Topics by id and name.
        public Dictionary<string, int> getTopics()
        {
            //Make query
            string query = "spGetTopics";

            //Retrieve Data
            DataSet data = database.downloadCommand(query);

            //Assemble the List
            Dictionary<string, int> topics = new Dictionary<string, int>();

            //Return useful data
            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                int topicID = (Int32)data.Tables[0].Rows[i]["topicID"];
                string name = data.Tables[0].Rows[i]["topicName"].ToString();

                topics.Add(name, topicID);
            }

            return topics;
        }
    }
}