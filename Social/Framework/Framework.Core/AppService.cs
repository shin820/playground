﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    public abstract class AppService
    {
        public IUserContext UserContext { get; set; }
    }
}
