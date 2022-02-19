﻿using ItServiceApp.Business.Repository.Abstracts;
using ItServiceApp.Core.Entities;
using ItServiceApp.Data;
using System;

namespace ItServiceApp.Business.Repository
{
    public class SubscriptionRepo : BaseRepository<Subscription, Guid>
    {
        public SubscriptionRepo(MyContext context) : base(context)
        {

        }
    }
}
