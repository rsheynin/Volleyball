using System;

namespace Volleyball.Models
{
    public class Match : IModel
    {
        public Guid Id { get; set; }

        public Guid Team1Id;
        public Guid Team2Id;

        //public int Points;
       // public int Sets;
        public DateTime Date;
    }
}
