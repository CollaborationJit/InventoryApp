using Microsoft.Extensions.DependencyInjection;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryHelper
{
    public static class IocContainer
    {
        public static ServiceProvider Provider { get; set; }
    }

    public static class IoC
    {
        public static ApplicationDbContext Context => IocContainer.Provider.GetService<ApplicationDbContext>();
    }
}
