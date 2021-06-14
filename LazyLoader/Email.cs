using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace LazyLoader
{
    [Table("emails")]
    public partial class Email
    {
        [Key]
        [Column("id", TypeName = "integer")]
        public long Id { get; set; }
        [Column("participant_list_id", TypeName = "integer")]
        public long? ParticipantListId { get; set; }
        [Required]
        [Column("email_address")]
        public string EmailAddress { get; set; }

        [Required]
        [ForeignKey(nameof(ParticipantListId))]
        [InverseProperty("Emails")]
        public virtual ParticipantList ParticipantList { get; set; }
    }
}
