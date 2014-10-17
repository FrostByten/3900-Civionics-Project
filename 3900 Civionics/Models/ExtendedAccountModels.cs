using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace _3900_Civionics.Models
{
    public class AccountInfo
    {
        public string Username { get; set; }
        public string Organization { get; set; }
        public string Email { get; set; }
    }

    public class AccountInfoDBContext : DbContext
    {
        public DbSet<AccountInfo> AccountInfoList { get; set; }
    }
}