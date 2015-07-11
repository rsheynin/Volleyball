using System;

namespace VB.Infrastructure.Models
{
    public class Team : IModel
    {
        public Guid Id { get; set; }
        public string Name;
        public int League;


        public DateTime Date;
    }
}
