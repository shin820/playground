﻿using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Social.Infrastructure
{
    public static class ClaimsIdentityExtensions
    {
        public static long? GetUserId(this IIdentity identity)
        {
            Checker.NotNull(identity, nameof(identity));

            var claimsIdentity = identity as ClaimsIdentity;

            var userIdOrNull = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdOrNull == null || string.IsNullOrWhiteSpace(userIdOrNull.Value))
            {
                return null;
            }

            return Convert.ToInt64(userIdOrNull.Value);
        }

        public static int? GetSideId(this IIdentity identity)
        {
            Checker.NotNull(identity, nameof(identity));

            var claimsIdentity = identity as ClaimsIdentity;

            var tenantIdOrNull = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == Comm100ClaimTypes.SideId);
            if (tenantIdOrNull == null || string.IsNullOrWhiteSpace(tenantIdOrNull.Value))
            {
                return null;
            }

            return Convert.ToInt32(tenantIdOrNull.Value);
        }
    }
}
