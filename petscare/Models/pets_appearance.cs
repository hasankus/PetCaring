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
    
    public partial class pets_appearance
    {
        public int pets_appearanceID { get; set; }
        public Nullable<int> pets_breedID { get; set; }
        public string pets_colors { get; set; }
        public string pets_breeddescription { get; set; }
        public string pets_bodysize { get; set; }
        public string pets_origin { get; set; }
        public string pets_characterdescription { get; set; }
        public string pets_careinstruction { get; set; }
        public string pets_hairfigure { get; set; }
        public Nullable<int> pets_ID { get; set; }
    
        public virtual pets pets { get; set; }
        public virtual pets_breed pets_breed { get; set; }
    }
}
