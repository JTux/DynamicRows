using DynamicRows.Web.Controllers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DynamicRows.Web.Models
{
    public class DefaultCreate
    {
        [Required]
        [MinLength(5)]
        public string Name { get; set; }
        public List<ItemCreate> Items { get; set; }
    }

    public class ItemCreate
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, 1150)]
        public int Number { get; set; }
    }

    public class DefaultListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ItemCount { get; set; }
    }

    public class DefaultDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ItemDetail> Items { get; set; }
    }

    public class ItemDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
    }
}