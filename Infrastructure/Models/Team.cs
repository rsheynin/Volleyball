using System;

namespace Volleyball.Models
{
    public class Team : IModel
    {
        public Guid Id { get; set; }
        public string Name;
        public int League;


        public DateTime Date;
    }
}
