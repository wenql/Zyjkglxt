﻿using System.Data.Common;
using Abp.Zero.EntityFramework;
using TcmHMS.Authorization.Roles;
using TcmHMS.Authorization.Users;
using TcmHMS.MultiTenancy;

namespace TcmHMS.EntityFramework
{
    public class TcmHMSDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public TcmHMSDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in TcmHMSDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of TcmHMSDbContext since ABP automatically handles it.
         */
        public TcmHMSDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public TcmHMSDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public TcmHMSDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
