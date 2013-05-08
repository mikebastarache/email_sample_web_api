﻿using System;
using System.Collections.Generic;

namespace EmailWebApi.Models
{
    public class ClientEmail
    {
        public string Email { get; set; }
        public string Language { get; set; }
        public string SiteSource { get; set; }
        public string CustomSubjectLine { get; set; }
        public string CustomBody { get; set; }
        public string CustomHeader { get; set; }
        public string CustomFooter { get; set; }
    }
}