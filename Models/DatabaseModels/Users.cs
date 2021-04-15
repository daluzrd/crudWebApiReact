using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("users")]
    public class Users
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("bithdate")]
        public DateTime BirthDate { get; set; }

        [Required]
        [MaxLength(11)]
        [Column("cpf")]
        public string CPF { get; set; }

        [MaxLength(10)]
        [Column("user")]
        public string User { get; set; }

        [MaxLength(32)]
        [Column("pwd")]
        public string Pwd { get; set; }
    }
}
