using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WalletApi.Models
{
    public class Wallet
    {
        [Key]
        public Guid Id { get; set; }
        [Index("IX_UserId", IsUnique = true)]
        public Guid UserId { get; set; }
        [Column(TypeName = "decimal")]
        [Range(0, Double.MaxValue)]
        [DataType("decimal(18,2)")]
        public Decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}