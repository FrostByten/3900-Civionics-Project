using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace _3900_Civionics.Models
{
    /// <summary>
    /// Description:
    ///     Represents the AccountInfo class,
    ///     which holds additional information on users
    /// 
    /// Data Members:
    ///     string Username
    ///     string Organization
    ///     string Email
    /// </summary>
    public class AccountInfo
    {
        public string Username { get; set; }
        public string Organization { get; set; }
        public string Email { get; set; }
    }

    /// <summary>
    /// Description:
    ///     The database context for the AccountInfo class
    /// 
    /// Data Members:
    ///     DbSet AccountInfoList
    /// </summary>
    public class AccountInfoDBContext : DbContext
    {
        public DbSet<AccountInfo> AccountInfoList { get; set; }
    }
}