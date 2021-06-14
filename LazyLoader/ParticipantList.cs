using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace LazyLoader
{
    [Table("participant_lists")]
    [Index(nameof(Name), IsUnique = true)]
    public partial class ParticipantList
    {
        public ParticipantList()
        {
            Emails = new HashSet<Email>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("name")]
        public string Name { get; set; }

        [InverseProperty(nameof(Email.ParticipantList))]
        public virtual ICollection<Email> Emails { get ; set; }
    }
}
