﻿namespace ApiBTG.Domain.Dtos.Tokens
{
    public class CreateToken
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string TokenValue { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool Status { get; set; }
    }
}
