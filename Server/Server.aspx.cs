﻿using InnovateServer.App_Code.Database;
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
            //insertStudentSession("bobD@gmail.com", "moo1234", 15, 17, 22);
        }

        //Inserts a new student into the database with the provided data.
        [WebMethod]
        public static DataPackage insertStudent(string firstName, string lastName, string email, string password, string school = null)
        {
            DataPackage package = new DataPackage();

            try
            {
                Student newStudent = new Student();
                newStudent.FirstName = firstName;
                newStudent.LastName = lastName;
                newStudent.Email = email;
                newStudent.School = school;
                newStudent.Password = password;

                //Let the database begin
                StudentTable studentsTable = new StudentTable(new DatabaseConnection());
                if (studentsTable.checkEmail(newStudent.Email))
                {
                    package.Message = "A user with this email already exists.";
                    package.WasSuccessful = false;
                    return package;
                }  
                else studentsTable.insertStudent(newStudent);

                //Verify student was entered into database and return a useful message to the frontend guys for testing
                Student verifyStudent = studentsTable.authenticateStudent(newStudent);
                if (studentsTable.authenticateStudent(newStudent) != null) package.Message = "The student was added to and retrieved from the database successfully.";
                else
                {
                    package.Message = "The student's retrieval from the database was unsuccessful.";
                    package.WasSuccessful = false;
                }

                return package;
            }
            catch (Exception e)
            {
                package.WasSuccessful = false;
                package.Message = e.Message;
                return package;
            }
        }



        //Gets all of the Faculty Sessions and whether or not they are full.
        [WebMethod]
        public static DataPackage getClasses()
        {
            DataPackage package = new DataPackage();

            try
            {
                //Let the database begin
                SessionTable sessionTable = new SessionTable(new DatabaseConnection());
                List<Session> sessions = sessionTable.getSessions();
                package.Data = sessions;
            }
            catch (Exception e)
            {
                package.WasSuccessful = false;
                package.Message = e.Message;
            }
            return package;
        }



        //Inserts a student's session choice into the database.
        //more performance tweaks desired.
        [WebMethod]
        public static DataPackage insertStudentSession(string email, string password, int choiceOneID, int choiceTwoID, int choiceThreeID)
        {
            DataPackage package = new DataPackage();
            
            try
            {
                DatabaseConnection connection = new DatabaseConnection();

                //Check if student authenticates, and if so proceed.  Look into caching these or other faster alternatives to calling the database each time.
                StudentTable studentTable = new StudentTable(connection);
                Student student = studentTable.authenticateStudent(new Student(email, password));
                if (student == null)    //Not found
                {
                    package.WasSuccessful = false;
                    package.Message = "Student credentials not found.";
                    return package;
                }

                //Check which classes are still open, and decide which is best to enroll in.
                SessionTable sessionTable = new SessionTable(connection);
                Dictionary<int, int> sessions = sessionTable.getSessionStudentCounts();

                int chosenID;
                if (!sessions.ContainsKey(choiceOneID) || sessions[choiceOneID] < App_Code.Entities.Session.MaxStudents) chosenID = choiceOneID;
                else if (!sessions.ContainsKey(choiceTwoID) || sessions[choiceTwoID] < App_Code.Entities.Session.MaxStudents) chosenID = choiceTwoID;
                else if (!sessions.ContainsKey(choiceThreeID) || sessions[choiceThreeID] < App_Code.Entities.Session.MaxStudents) chosenID = choiceThreeID;
                else    //No Openings found in selected sessions, kickback to the front-end. :(
                {
                    package.WasSuccessful = false;
                    package.Message = "All selected sessions are full.";
                    return package;
                }

                //Insert Student's choice
                studentTable.insertStudentClass(student, chosenID);
                package.Message = "Session chosen successfully!";
                return package;
            }

            catch (Exception e)
            {
                package.WasSuccessful = false;
                package.Message = e.Message;
                return package;
            }
        }


        //Updates the Student with their two preferred topic choices.
        [WebMethod]
        public static DataPackage chooseTopics(string email, string password, string choiceOneName, string choiceTwoName)
        {
            DataPackage package = new DataPackage();

            try
            {
                //Let the database begin
                DatabaseConnection connection = new DatabaseConnection();
                TopicTable topicTable = new TopicTable(connection);
                Dictionary<string, int> topics = topicTable.getTopics();

                //Make sure our dictionary has these two names
                if (topics.ContainsKey(choiceOneName) && topics.ContainsKey(choiceTwoName))
                {
                    //Update the student with their topic choices
                    StudentTable studentTable = new StudentTable(connection);
                    studentTable.updateStudentTopics(topics[choiceOneName], topics[choiceTwoName]);
                    package.Message = "Topic Preferences updated!";
                    return package;
                }
                else     //Bad things have happened
                {
                    package.WasSuccessful = false;
                    package.Message = "Topic Preferences updated!";
                    return package;
                }
            }
            catch (Exception e)
            {
                package.WasSuccessful = false;
                package.Message = e.Message;
                return package;
            }
            
        }
    }
}