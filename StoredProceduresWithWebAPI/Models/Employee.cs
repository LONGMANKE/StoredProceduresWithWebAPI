﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoredProceduresWithWebAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Age { get; set; }
        public int Active { get; set; }
    }
}