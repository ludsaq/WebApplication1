using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class User
    {
        [Key]
        [Required]
        [Display(Name = "Логин")]
        [DataType(DataType.Text)]
        public string login { get; set; }

        [Required]
        [Display(Name = "Контактная информация")]
        [DataType(DataType.Url)]
        public string referens { get; set; }

        [Required]
        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        public string description { get; set; }
    }
}
