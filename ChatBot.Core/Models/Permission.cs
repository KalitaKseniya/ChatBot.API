﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ChatBot.Core.Models
{
    public class Permission
    {
        public int Id { get; set; }
        [Required]
        //equivalent to ClaimVaue in table AspNetRoleClaims
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}