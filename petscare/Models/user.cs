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
    
    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            this.ownage_notice = new HashSet<ownage_notice>();
            this.user_adress = new HashSet<user_adress>();
            this.user_pets = new HashSet<user_pets>();
            this.user_pictures = new HashSet<user_pictures>();
        }
    
        public int user_ID { get; set; }
        public string user_firstname { get; set; }
        public string user_lastname { get; set; }
        public string user_email { get; set; }
        public string user_telephone { get; set; }
        public Nullable<int> user_age { get; set; }
        public Nullable<bool> user_gender { get; set; }
        public string user_username { get; set; }
        public string user_firstpassword { get; set; }
        public string user_againpassword { get; set; }
        public string user_pictureextension { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ownage_notice> ownage_notice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<user_adress> user_adress { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<user_pets> user_pets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<user_pictures> user_pictures { get; set; }
    }
}
