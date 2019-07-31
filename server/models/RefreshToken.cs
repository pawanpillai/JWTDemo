﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWTDemo
{
    [Table("token")]
    public class RefreshToken
    {
        [Column("id")]
        [Key]
        public string Id { get; set; }

        [Column("client_id")]
        public string ClientId { get; set; }

        [Column("refresh_token")]
        public string Token { get; set; }

        [Column("is_stop")]
        public int IsStop { get; set; }

        [Column("username")]
        public string UserName { get; set; }
    }
}
