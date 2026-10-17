using System;
using System.Collections.Generic;
using System.Text;

namespace KooliProjekt.Application.Data
{
    public class User
    {
        public int user_id { get; set; }

        public string first_name { get; set; } = string.Empty;

        public string last_name { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;

        public string password_hash { get; set; } = string.Empty;

        public DateTime registered_at { get; set; } = DateTime.UtcNow;

        public string role { get; set; } = "user";

    }

    
} 
