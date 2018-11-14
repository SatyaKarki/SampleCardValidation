namespace Card.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CardNumber")]
    public partial class CardNumber
    {
        public long Id { get; set; }

        public long CardTypeId { get; set; }

        [StringLength(50)]
        public string CNumber { get; set; }
        [StringLength(6)]
        public string Expiry { get; set; }
        public bool Active { get; set; }

        public DateTime Created { get; set; }

        public long Author { get; set; }

        public DateTime Modified { get; set; }

        public long Editor { get; set; }
        [JsonIgnore]
        public virtual CardType CardType { get; set; }
    }
}
