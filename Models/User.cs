using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankModels
{
    public class User : StandardModel
    {
        [Display(Name = "name", ResourceType = typeof(RESX.Resource))]
        [Required(ErrorMessageResourceName = "required_name", ErrorMessageResourceType = typeof(RESX.Resource))]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Le nom doit contenir entre {1} et {0} caractères")]
        public string Name { get; set; }

        [Display(Name = "firstname", ResourceType = typeof(RESX.Resource))]
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(RESX.Resource))]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Le nom doit contenir entre {1} et {0} caractères")]
        public string Firstname { get; set; }

        [Display(Name = "mail", ResourceType = typeof(RESX.Resource))]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(RESX.Resource))]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                           @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                           @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                           ErrorMessage = "Le format du mail est incorrect.")]
        public string Mail { get; set; }

        [Display(Name = "Date de naissance")]
        [DataType(DataType.Date)]
        [MajorAttribute(18, ErrorMessage = "La personne doit avoir plus de {0} ans.")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "password", ResourceType = typeof(RESX.Resource))]
        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(RESX.Resource))]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Le mot de passe doit contenir entre {1} et {0} caractères.")]
        public string Password { get; set; }

        [Display(Name = "balance", ResourceType = typeof(RESX.Resource))]
        [DataType(DataType.Currency)]
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(RESX.Resource))]
        public double Balance { get; set; }

        [Display(Name = "rib", ResourceType = typeof(RESX.Resource))]
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(RESX.Resource))]
        [StringLength(15, MinimumLength = 15, ErrorMessage = "Le numéro de compte doit contenir {0} caractères")]
        public string RIB { get; set; }

        //[Display(Name = "movements", ResourceType = typeof(RESX.Resource))]
        //public ICollection<Movement> Movements { get; set; } 

    }
}