﻿namespace RestProjectRouTeam.Infrastructure
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Generate(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        }

        public bool Verify(string password, string hasedPassword) 
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hasedPassword);
        }
    }
}