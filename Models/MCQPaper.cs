﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPortal.Models
{
    public class MCQPaper : Paper
    {
        public MCQPaper()
        {
            Questions = new List<MCQQuestion>();
        }
        public List<MCQQuestion> Questions { get; set; }
    }
}
