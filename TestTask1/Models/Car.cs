using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTask1.Models
{
    public class Car
    {
        [Key]
        public long Id { get; set; }
        [Required, MinLength(2), MaxLength(31)]
        public string Brand { get; set; }
        [Required, MinLength(2), MaxLength(63)]
        public string Model { get; set; }
        public Category Category { get; set; }
        //[ForeignKey("CategoryId")]
        public long CategoryId { get; set; }
        [Required, MinLength(6), MaxLength(6)]
        public string Number { get; set; }
        [Range(1900, 2022)]
        public short ReleaseYear { get; set; }


        public Car()
        {

        }

        //public Car(string brand, string model, Category category, string number, ushort releaseYear)
        //{
        //    Brand = brand;
        //    Model = model;
        //    Category = category;
        //    CategoryId = category.Id;
        //    Number = number;
        //    ReleaseYear = releaseYear;
        //}
    }
}
