﻿using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RequestRepository : BaseRepo<Request>
    {
        public RequestRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public RequestRepository(AppDbContext dbContext, ILogger<BaseRepo<Request>> logger) : base(dbContext, logger)
        {
        }


    }
}