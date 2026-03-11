using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectManagement.Domain.Entities.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("Email")]
        public required string Email { get; set; }

        [Required]
        [Column("Username")]
        public required string Username { get; set; }

        [Required]
        [Column("PasswordHash")]
        public required string Password { get; set; }

        [Required]
        [Column("FirstName")]
        public required string FirstName { get; set; }

        [Required]
        [Column("LastName")]
        public required string LastName { get; set; }
    }
}
