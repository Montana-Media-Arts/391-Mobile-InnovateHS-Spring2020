using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnovateServer.App_Code.Entities
{
    public class Session
    {

        private int classID;
        private string className;
        private int topicID;
        private string topicName;
        private string description;
        private static readonly int maxStudents = 20;
        private int currentStudents;
        private bool isFull;
        private DateTime? time;          //Times not yet configured, dont use this.
        private string building;        //All buildings are currently null, dont use this.
        private string room;            //All buildings are currently null, dont use this.
        private string speakerName;
        private string speakerAffiliation;

        public int ClassID { get => classID; set => classID = value; }
        public string Name { get => className; set => className = value; }
        public int TopicID { get => topicID; set => topicID = value; }
        public string TopicName { get => topicName; set => topicName = value; }
        public string Description { get => description; set => description = value; }
        public int CurrentStudents { get => currentStudents; set => currentStudents = value; }
        public DateTime? Time { get => time; set => time = value; }
        public string Building { get => building; set => building = value; }
        public string Room { get => room; set => room = value; }
        public bool IsFull { get => isFull; set => isFull = value; }
        public static int MaxStudents { get => maxStudents;}
        public string SpeakerName { get => speakerName; set => speakerName = value; }
        public string SpeakerAffiliation { get => speakerAffiliation; set => speakerAffiliation = value; }
    }   

}