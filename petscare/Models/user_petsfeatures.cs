//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace petscare.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class user_petsfeatures
    {
        public Nullable<int> pet_ID { get; set; }
        public int pet_featuresID { get; set; }
        public string pet_colors { get; set; }
        public string pet_eyescolors { get; set; }
        public string pet_kilo { get; set; }
        public Nullable<int> pet_height { get; set; }
        public Nullable<bool> pet_gender { get; set; }
        public Nullable<int> pet_age { get; set; }
        public Nullable<System.DateTime> pet_birthday { get; set; }
        public string pet_birthplace { get; set; }
    
        public virtual user_pets user_pets { get; set; }
    }
}
