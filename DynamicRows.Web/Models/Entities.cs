using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DynamicRows.Web.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Number { get; set; }

        [ForeignKey(nameof(Default))]
        public int DefaultId { get; set; }
        public virtual Default Default { get; set; }
    }

    public class Default
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual List<Item> Items { get; set; }
    }
}