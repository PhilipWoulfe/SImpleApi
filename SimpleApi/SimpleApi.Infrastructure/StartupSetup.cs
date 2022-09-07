﻿using Microsoft.Extensions.DependencyInjection;
using SimpleApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace SimpleApi.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString) =>
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString)); 
    }
}
