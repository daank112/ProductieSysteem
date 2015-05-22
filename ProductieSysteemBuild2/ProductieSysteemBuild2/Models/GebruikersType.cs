namespace ProductieSysteemBuild2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("[dbo].[aspnet_Roles]")]
    public partial class GebruikersType
    {
        public GebruikersType()
        {
            GebruikersTypeRol = new HashSet<GebruikersTypeRol>();
        }

        public string Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public virtual ICollection<GebruikersTypeRol> GebruikersTypeRol { get; set; }
    }
}
