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
    
    public partial class ownage_notice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ownage_notice()
        {
            this.ownagenotices_pictures = new HashSet<ownagenotices_pictures>();
        }
    
        public int notice_ID { get; set; }
        public string notice_title { get; set; }
        public string notice_description { get; set; }
        public Nullable<int> user_ID { get; set; }
        public string notice_picturesextension { get; set; }
        public Nullable<System.DateTime> notice_date { get; set; }
        public string notice_petname { get; set; }
        public Nullable<int> notice_petage { get; set; }
        public string notice_petgender { get; set; }
        public string notice_province { get; set; }
        public Nullable<int> notice_pictureID { get; set; }
        public Nullable<int> notice_petgenderID { get; set; }
        public Nullable<int> notice_petspeciesID { get; set; }
        public Nullable<int> notice_petbreedID { get; set; }
        public string notice_petbreedname { get; set; }
    
        public virtual pet_gender pet_gender { get; set; }
        public virtual pets_breed pets_breed { get; set; }
        public virtual pets_species pets_species { get; set; }
        public virtual user user { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ownagenotices_pictures> ownagenotices_pictures { get; set; }
    }
}
