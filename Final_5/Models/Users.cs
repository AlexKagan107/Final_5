//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Final_5.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Users
    {
        public string userId { get; set; }
        public string password { get; set; }
        public string fileFinger { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string idDoctor { get; set; }
        public string permissions { get; set; }
    
        public virtual Doctor Doctor { get; set; }
    }
}
