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
    
    public partial class vets
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public vets()
        {
            this.appointments = new HashSet<appointments>();
            this.user_pets = new HashSet<user_pets>();
            this.vaccine_calendar = new HashSet<vaccine_calendar>();
            this.vets_adress = new HashSet<vets_adress>();
            this.vets_pictures = new HashSet<vets_pictures>();
        }
    
        public int vets_ID { get; set; }
        public string vets_firstname { get; set; }
        public string vets_lastname { get; set; }
        public string vets_email { get; set; }
        public string vets_telephone { get; set; }
        public Nullable<bool> vets_gender { get; set; }
        public Nullable<int> vets_age { get; set; }
        public string vets_firstpassword { get; set; }
        public string vets_passwordagain { get; set; }
        public string vets_username { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<appointments> appointments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<user_pets> user_pets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<vaccine_calendar> vaccine_calendar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<vets_adress> vets_adress { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<vets_pictures> vets_pictures { get; set; }
    }
}