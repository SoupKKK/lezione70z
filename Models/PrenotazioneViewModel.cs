using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace lezione70z.Models
{
    public class PrenotazioneViewModel
    {

        public PrenotazioneViewModel()
        {
            DataPrenotazione = DateTime.Now;
            DataArrivo = DateTime.Now;
            DataPartenza = DateTime.Now;
        }


        [Required]
        [Display(Name = "Data Prenotazione")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "1/1/1753", "12/12/8999")]

        public DateTime? DataPrenotazione { get; set; }
       


        [Required]
        [Display(Name = "Anno")]
        public int Anno { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data Arrivo")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "1/1/1753", "12/12/8999")]

        public DateTime DataArrivo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data Partenza")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "1/1/1753", "12/12/8999")]

        public DateTime DataPartenza { get; set; }

        [Required]
        public decimal Cauzione { get; set; }

        [Required]
        public decimal Tariffa { get; set; }

        [Display(Name = "Mezza Pensione")]
        public bool MezzaPensione { get; set; }

        [Display(Name = "Pensione Completa")]
        public bool PensioneCompleta { get; set; }

        [Display(Name = "Prima Colazione")]
        public bool PrimaColazione { get; set; }

        [Required]
        [Display(Name = "Numero Camera")]
        public int NumeroCamera { get; set; }

        [Required]
        [Display(Name = "Codice Fiscale Cliente")]
        public string CodiceFiscaleCliente { get; set; }

        public List<SelectListItem> CodiciFiscaliList { get; set; }
        public List<SelectListItem> CamereList { get; set; }
    }
}
