using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WalletApi.Models
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid WalletId {  get; set; }
        [ForeignKey("WalletId")]
        public virtual Wallet Wallet { get; set; }
        [Column(TypeName = "decimal")]
        public decimal Amount{ get; set; }
        [Required]
        [MaxLength(50)]
        public string Type {  get; set; }
        public DateTime CreatedAt { get; set; }
        [Index("IX_ReferenceId", IsUnique =false)]
        [MaxLength(200)]
        public string ReferenceId { get; set; }

    }
}