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
        [Key]
        [Column("Email")]
        public string Email { get; set; }
        [Key]
        [Column("Username")]
        public string Username { get; set; }
        [Key]
        [Column("PasswordHash")]
        public string Password { get; set; }
        [Key]
        [Column("FirstName")]
        public string FirstName { get; set; }
        [Key]
        [Column("LastName")]
        public string LastName { get; set; }
    }
}
