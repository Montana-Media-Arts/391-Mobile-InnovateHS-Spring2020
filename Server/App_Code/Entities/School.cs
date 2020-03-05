using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnovateServer.App_Code.Entities
{
    public class School
    {

        private int schoolID;
        private string schoolName;
        private bool isOther;

        public int SchoolID { get => schoolID; set => schoolID = value; }
        public string SchoolName { get => schoolName; set => schoolName = value; }
        public bool IsOther { get => isOther; set => isOther = value; }
    }
}