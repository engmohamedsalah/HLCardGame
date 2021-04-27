using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HLCardGame.Infrastructure.Entities
{
    [Table("Cards")]
    public class CardEntity
    {
        [Key]
        public Guid CardId { get; set; }

        public CardColor Color { get; set; }

        public Suit Suit { get; set; }

        public int Value { get; set; }

        public string DisplayName { get; set; }
    }
}