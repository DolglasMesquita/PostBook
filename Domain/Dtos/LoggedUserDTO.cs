﻿namespace PostBook.Domain.Dtos
{
    public class LoggedUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
