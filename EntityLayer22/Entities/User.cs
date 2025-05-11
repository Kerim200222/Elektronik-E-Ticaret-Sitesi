using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer22.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Boş Geçilmez")]
        [Display(Name = "Ad")]
        [StringLength(50, ErrorMessage = "Max 50 Karakter Olmalidir.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Boş Geçilmez")]
        [Display(Name = "Soyad")]
        [StringLength(50, ErrorMessage = "Max 50 Karakter Olmalidir.")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Boş Geçilmez")]
        [Display(Name = "E-Posta")]
        [StringLength(50, ErrorMessage = "Max 50 Karakter Olmalidir.")]
        [EmailAddress(ErrorMessage = "E-mail Formatı şeklinde Giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Boş Geçilmez")]
        [Display(Name = "Kullanıcı Adı")]
        [StringLength(50, ErrorMessage = "Max 50 Karakter Olmalidir.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Boş Geçilmez")]
        [Display(Name = "Şifre")]
        [StringLength(10, ErrorMessage = "Max 10 Karakter Olmalidir.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Boş Geçilmez")]
        [Display(Name = "Şifre")]
        [StringLength(10, ErrorMessage = "Max 10 Karakter Olmalidir.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Sifre Uyusmuyor")]
        public string RePassword { get; set; }

        [StringLength(10, ErrorMessage = "Max 10 karakter Olmalidir.")]
        public string Role { get; set; }
    }
}
