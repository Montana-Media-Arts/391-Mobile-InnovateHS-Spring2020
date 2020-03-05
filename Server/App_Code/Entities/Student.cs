using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//comment
namespace InnovateServer.App_Code.Entities
{
    public class Student
    {
        private int studentID;
        private string firstName;
        private string lastName;
        private string email;
        private int schoolID;
        private string password;
        private string fbLiked;
        private string fbBetter;
        private string fbWantOfferings;
        private string fbOther;
        private int[] topicChoices = new int[2];
        private bool registrationComplete = false;

        //Blank Student
        public Student() { }

        //Student with login credentials
        public Student(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

        public int StudentID { get => studentID; set => studentID = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public string FbLiked { get => fbLiked; set => fbLiked = value; }
        public string FbBetter { get => fbBetter; set => fbBetter = value; }
        public string FbWantOfferings { get => fbWantOfferings; set => fbWantOfferings = value; }
        public int[] TopicChoices { get => topicChoices; set => topicChoices = value; }
        public int SchoolID { get => schoolID; set => schoolID = value; }
        public string Password { get => password; set => password = value; }
        public string FbOther { get => fbOther; set => fbOther = value; }
        public bool RegistrationComplete { get => registrationComplete; set => registrationComplete = value; }
    }
}