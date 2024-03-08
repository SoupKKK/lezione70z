using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace lezione70z.Models
{
    public class PrenotazioniClienteViewModel
    {
        public string CodiceFiscaleCliente { get; set; }
        public List<PrenotazioneViewModel> Prenotazioni { get; set; }
    }
}

