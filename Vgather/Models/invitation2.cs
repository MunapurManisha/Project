//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vgather.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class invitation2
    {
        public string ContactNumber { get; set; }
        public string ContactName { get; set; }
        public string Venue_Name { get; set; }
    
        public virtual ContactList2 ContactList2 { get; set; }
    }
}
