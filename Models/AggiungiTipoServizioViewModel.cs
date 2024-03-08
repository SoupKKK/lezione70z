using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace lezione70z.Models
{
    public class AggiungiTipoServizioViewModel
    {

        [Required]
        [Display(Name = "Data Servizio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "1/1/1753", "12/12/8999")]

        public DateTime? DataServizio { get; set; }

        [Required(ErrorMessage = "Il campo Quantità è obbligatorio.")]
        [Display(Name = "Quantità")]
        public int Quantita { get; set; }

        [Required(ErrorMessage = "Il campo Codice Fiscale Cliente è obbligatorio.")]
        [Display(Name = "Codice Fiscale Cliente")]
        public string CodiceFiscaleCliente { get; set; }

        [Required(ErrorMessage = "Il campo Numero Camera è obbligatorio.")]
        [Display(Name = "Numero Camera")]
        public int NumeroCamera { get; set; }

        [Required(ErrorMessage = "Il campo Tipo Servizio è obbligatorio.")]
        [Display(Name = "Tipo Servizio")]
        public int TipoServizioID { get; set; }
        public IEnumerable<SelectListItem> TipiServizi { get; set; }
    }
}
