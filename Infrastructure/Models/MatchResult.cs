using System;
using System.Collections.Generic;

namespace VB.Infrastructure.Models
{
     public class MatchResult: IModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid MatchId;
        public List<int> SetResults;
    }
}
