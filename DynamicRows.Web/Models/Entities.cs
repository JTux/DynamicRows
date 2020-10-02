using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DynamicRows.Web.Models
{
    public class ItemEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Number { get; set; }

        [ForeignKey(nameof(Default))]
        public int DefaultId { get; set; }
        public virtual DefaultEntity Default { get; set; }
    }

    public class DefaultEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual List<ItemEntity> Items { get; set; }
    }
}