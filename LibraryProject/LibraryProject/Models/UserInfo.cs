﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryProject.Models
{
    public class UserInfo
    {
        public int ID { get; set; }
        [Required]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string StudentID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DepartmentName { get; set; }
    }
}