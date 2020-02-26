﻿using InnovateServer.App_Code.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace InnovateServer.App_Code.Database
{
    public class SessionTable
    {
        protected DatabaseConnection database;

        public SessionTable(DatabaseConnection database)
        {
            this.database = database;
        }

        //Returns all of the Faculty Sessions with their current student totals.
        public List<Session> getSessions()
        {
            //Make query
            string query = "spGetClasses";

            //Retrieve Data
            DataSet data = database.downloadCommand(query);

            //Assemble the List
            List<Session> sessions = new List<Session>();

            //Return useful data
            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                int classID = (Int32)data.Tables[0].Rows[i]["classID"];
                int topicID = (Int32)data.Tables[0].Rows[i]["topicID"];
                int currentStudents = (Int32)data.Tables[0].Rows[i][8];
                string name = data.Tables[0].Rows[i]["className"].ToString();
                string topicName = data.Tables[0].Rows[i]["topicName"].ToString();
                string description = data.Tables[0].Rows[i]["description"].ToString();
                string room = data.Tables[0].Rows[i]["room"].ToString();
                string building = data.Tables[0].Rows[i]["building"].ToString();

                DateTime? time;
                if (data.Tables[0].Rows[i]["time"].GetType() == typeof(DBNull)) time = null;
                else time = (DateTime?)data.Tables[0].Rows[i]["time"];

                Session session = new Session();
                session.ClassID = classID;
                session.TopicID = topicID;
                session.CurrentStudents = currentStudents;
                session.Name = name;
                session.TopicName = topicName;
                session.Description = description;
                session.Room = room;
                session.Building = building;
                session.Time = time;
                if (session.CurrentStudents >= Session.MaxStudents) session.IsFull = true;
                else session.IsFull = false;

                sessions.Add(session);
            }

            return sessions;
        }



        //Similar to getSessions but designed to be faster.  Only returns the raw counts and ids foreach class that has students.
        public Dictionary<int, int> getSessionStudentCounts()
        {
            //Make query
            string query = "spGetClassStudentCounts";

            //Retrieve Data
            DataSet data = database.downloadCommand(query);

            //Assemble the List
            Dictionary<int, int> sessions = new Dictionary<int, int>();

            //Return useful data
            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                int classID = (Int32)data.Tables[0].Rows[i]["classID"];
                int students = (Int32)data.Tables[0].Rows[i][1];

                sessions.Add(classID, students);
            }

            return sessions;
        }

    }
}