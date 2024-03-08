using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace lezione70z.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Il campo Nome è obbligatorio.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il campo Cognome è obbligatorio.")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "Il campo Username è obbligatorio.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Il campo Password è obbligatorio.")]
        public string Password { get; set; }
    }

}