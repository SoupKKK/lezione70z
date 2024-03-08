using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using lezione70z.Models;

namespace lezione70z.Controllers
{
    public class PrenotazioniClienteController : Controller
    {
        private static readonly string connectionString = DataAccess.ConnectionString;

        public ActionResult Index()
        {
            List<SelectListItem> codiciFiscaliList = GetCodiciFiscaliList();
            ViewBag.CodiciFiscaliList = codiciFiscaliList;

            return View();
        }

        private List<PrenotazioneViewModel> GetPrenotazioniCliente(string codiceFiscaleCliente)
        {
            List<PrenotazioneViewModel> prenotazioni = new List<PrenotazioneViewModel>();

            if (string.IsNullOrEmpty(codiceFiscaleCliente))
            {
                // Gestisci il caso in cui il codice fiscale sia nullo o vuoto
                return prenotazioni;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(
                    "SELECT * FROM Prenotazioni WHERE CodiceFiscaleCliente = @CodiceFiscaleCliente", connection))
                {
                    command.Parameters.AddWithValue("@CodiceFiscaleCliente", codiceFiscaleCliente);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PrenotazioneViewModel prenotazione = new PrenotazioneViewModel
                            {
                                DataPrenotazione = reader.GetDateTime(reader.GetOrdinal("DataPrenotazione")),
                                Anno = reader.GetInt32(reader.GetOrdinal("Anno")),
                                DataArrivo = reader.GetDateTime(reader.GetOrdinal("DataArrivo")),
                                DataPartenza = reader.GetDateTime(reader.GetOrdinal("DataPartenza")),
                                Cauzione = reader.GetDecimal(reader.GetOrdinal("Cauzione")),
                                Tariffa = reader.GetDecimal(reader.GetOrdinal("Tariffa")),
                                MezzaPensione = reader.GetBoolean(reader.GetOrdinal("MezzaPensione")),
                                PensioneCompleta = reader.GetBoolean(reader.GetOrdinal("PensioneCompleta")),
                                PrimaColazione = reader.GetBoolean(reader.GetOrdinal("PrimaColazione")),
                                CodiceFiscaleCliente = reader.GetString(reader.GetOrdinal("CodiceFiscaleCliente")),
                                NumeroCamera = reader.GetInt32(reader.GetOrdinal("NumeroCamera"))
                            };

                            prenotazioni.Add(prenotazione);
                        }
                    }
                }
            }

            return prenotazioni;
        }

        private List<SelectListItem> GetCodiciFiscaliList()
        {
            List<SelectListItem> codiciFiscaliList = new List<SelectListItem>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT CodiceFiscale FROM Clienti", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string codiceFiscale = reader.GetString(0);

                        codiciFiscaliList.Add(new SelectListItem { Value = codiceFiscale, Text = codiceFiscale });
                    }
                }
            }

            return codiciFiscaliList;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PrenotazioniCliente(PrenotazioniClienteViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Prenotazioni = GetPrenotazioniCliente(model.CodiceFiscaleCliente);
            }

            List<SelectListItem> codiciFiscaliList = GetCodiciFiscaliList();
            ViewBag.CodiciFiscaliList = codiciFiscaliList;

            return View(model);
        }
    }
}
