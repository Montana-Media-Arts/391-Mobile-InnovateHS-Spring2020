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
        private string school;
        private Byte[] password;
        private string fbLiked;
        private string fbBetter;
        private string fbWantOfferings;
        private string fbOther;
        private int[] topicChoices = new int[2];
        private int[] classChoices = new int[3];

        public int StudentID { get => studentID; set => studentID = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public string FbLiked { get => fbLiked; set => fbLiked = value; }
        public string FbBetter { get => fbBetter; set => fbBetter = value; }
        public string FbWantOfferings { get => fbWantOfferings; set => fbWantOfferings = value; }
        public int[] TopicChoices { get => topicChoices; set => topicChoices = value; }
        public int[] ClassChoices { get => classChoices; set => classChoices = value; }
        public string School { get => school; set => school = value; }
        public byte[] Password { get => password; set => password = value; }
        public string FbOther { get => fbOther; set => fbOther = value; }
    }
}