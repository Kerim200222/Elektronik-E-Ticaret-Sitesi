using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer22.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Boş Geçilemez")]
        [Display(Name = "Ad")]
        [StringLength(50, ErrorMessage = "Maksimum 50 karakter olmalıdır!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Boş Geçilmez")]
        [Display(Name = "Aciklama")]
        [StringLength(50, ErrorMessage = "Max 50 Karakter Olmalidir.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Boş Geçilmez")]
        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Boş Geçilmez")]
        [Display(Name = "Stock")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Boş Geçilmez")]
        [Display(Name = "Popular")]
        public bool Popular { get; set; }

        [Required(ErrorMessage = "Boş Geçilmez")]
        [Display(Name = "Onay")]
        public bool IsApproved { get; set; }

        [Required(ErrorMessage = "Boş Geçilmez")]
        [Display(Name = "Resim")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Boş Geçilmez")]
        [Display(Name = "Adet")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Boş Geçilmez")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual List<Cart> Cart { get; set; }

        public virtual List<Sales> Sales { get; set; }
    }
}
