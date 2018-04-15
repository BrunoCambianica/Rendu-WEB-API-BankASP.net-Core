using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BankModels
{
    public class Movement : StandardModel
    {
        [Display(Name = "amount", ResourceType = typeof(RESX.Resource))]
        [DataType(DataType.Currency)]
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(RESX.Resource))]
        public double Amount { get; set; }

        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(RESX.Resource))]
        public Guid CreditID { get; set; }
        //public User User { get; set; }

        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(RESX.Resource))]
        public Guid DebitID { get; set; }

        [Display(Name = "note", ResourceType = typeof(RESX.Resource))]
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(RESX.Resource))]
        [StringLength(50, ErrorMessage = "Le message ne peut contenir plus de {0} caractères")]
        public string Message { get; set; }
    }
}
