﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.ApplicationDomainModels
{
    public class SmUserRoles
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}