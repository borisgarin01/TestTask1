using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace TestTask1.Models
{
    public class Category
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [MinLength(1), MaxLength(63)]
        public string Name { get; set; }

        public Category()
        {

        }
        public Category(string name)
        {
            Name = name;
        }
    }
}