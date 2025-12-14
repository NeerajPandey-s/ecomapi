using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcomAPI.Repository.Setup.Dto
{
    [PrimaryKey(nameof(Id))]
    [Table("tbl_business", Schema = "business")]
    public class BusinessDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; init; }

        [Required]
        [MaxLength(200)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public required string DomainName { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Email { get; set; }

        [Required]
        public required string EncryptedPassword { get; set; }


        public static void AddBuilderProperties(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<BusinessDto>()
                .HasIndex(x => x.DomainName)
                .IsUnique();
        }
    }
}
