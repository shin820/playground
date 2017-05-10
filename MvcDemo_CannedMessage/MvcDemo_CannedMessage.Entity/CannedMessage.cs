﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo_CannedMessage.Entity
{
    public class CannedMessage: EFEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(500)]
        public string Message { get; set; }
        [MaxLength(100)]
        public string Shortcuts { get; set; }
    }
}
