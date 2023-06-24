﻿namespace TaskManager.Api.Models
{
    public class Project : CommonObject
    {
        public List<User> AllUsers { get; set; }
        public List<Desk> AllDescs { get; set; }
    }
}
