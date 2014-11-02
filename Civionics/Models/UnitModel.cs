using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Civionics.Models
{
    /// <summary>
    /// Description:
    ///     Represents the Unit table,
    ///     which is essentially a dynamic array,
    ///     that needs to be changed during run time
    /// 
    /// Data Members:
    ///     string UnitID
    /// </summary>
    public class Unit
    {
        //public int ID;
        [Display(Name = "Unit")]
        public string UnitID { get; set; }
    }
}