using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HLCardGame.Infrastructure.Entities
{
    [Table("Deck")]
    public class DeckEntity
    {
        [Key]
        public Guid DeckId { get; set; }

        public int NPlayers { get; set; }

        public int PlayerTurn { get; set; }

        public int LastCardValue { get; set; }

        public string LastCardJson { get; set; }

        public virtual ICollection<CardEntity> Cards { get; set; }
    }
}