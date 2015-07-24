using System;

namespace VB.Infrastructure.Models
{
    public class Team : IModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public int League;
        public DateTime Date;
    }
}
