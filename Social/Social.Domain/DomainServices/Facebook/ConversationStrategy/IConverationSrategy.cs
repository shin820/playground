﻿using Social.Domain.Entities;
using Social.Infrastructure.Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Domain.DomainServices.Facebook
{
    public interface IConversationSrategy
    {
        Task Process(SocialAccount socialAccount, FbChange data);
        bool IsMatch(FbChange data);
    }
}