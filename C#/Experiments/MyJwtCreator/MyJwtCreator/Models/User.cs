﻿namespace MyJwtCreator.Models
{
    /// <summary>
    /// Sample User Model
    /// </summary>
    public class User
    {
        public int UserId { get; set; }
        public string EmailAddress { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}