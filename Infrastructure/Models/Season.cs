using System;

namespace VB.Infrastructure.Models
{
    class Season: IModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TeamCount;
        public string CompetitionSestem;
        public int GameCount;

    }
}
