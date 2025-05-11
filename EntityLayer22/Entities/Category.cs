using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer22.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Boş Geçilmez")]
        [Display(Name = "Ad")]
        [StringLength(50, ErrorMessage = "Max 50 Karakter Olmalidir.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Boş Geçilmez")]
        [Display(Name = "Açıklama")]
        [StringLength(50, ErrorMessage = "Max 50 Karakter Olmalidir.")]
        public string Description { get; set; }
        [ValidateNever]
        public List<Product> Products { get; set; }
    }
}
