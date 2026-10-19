using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data
{
    [Index(nameof(user_name), IsUnique = true)]
    public class User
    {
        
        [Key]
        public int user_id { get; set; }

        
        public string user_name { get; set; } = string.Empty;
        public string first_name { get; set; } = string.Empty;
        public string last_name { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string password_hash { get; set; } = string.Empty;
        public DateTime registered_at { get; set; } = DateTime.UtcNow;
        public string role { get; set; } = "user";
    }
}