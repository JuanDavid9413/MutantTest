using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BackEnd.Mutants.Entities.DbSet
{
    public class Mutants
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "IsMutant")]
        public bool IsMutant { get; set; }
    }
}
