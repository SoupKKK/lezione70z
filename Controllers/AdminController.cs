using lezione70z.Models;
using System.Data.SqlClient;
using System;
using System.Configuration;  
using System.Web.Mvc;
using System.Collections.Generic;

namespace lezione70z.Controllers
{
    public class AdminController : Controller
    {
        private static readonly string connectionString = DataAccess.ConnectionString;

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

        private List<SelectListItem> GetCamereList()
        {
            List<SelectListItem> camereList = new List<SelectListItem>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT ID, Numero FROM Camere", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int cameraId = reader.GetInt32(0);
                        int numeroCamera = reader.GetInt32(1);

                        camereList.Add(new SelectListItem { Value = cameraId.ToString(), Text = numeroCamera.ToString() });
                    }
                }
            }

            return camereList;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistraPrenotazione(PrenotazioneViewModel model)
        {
            model.CodiciFiscaliList = GetCodiciFiscaliList();
            model.CamereList = GetCamereList();

            if (ModelState.IsValid)
            {
                // Logica per salvare la prenotazione nel database
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(
                            "INSERT INTO Prenotazioni (DataPrenotazione, Anno, DataArrivo, DataPartenza, Cauzione, Tariffa, MezzaPensione, PensioneCompleta, PrimaColazione, CodiceFiscaleCliente, NumeroCamera) " +
                            "VALUES (@DataPrenotazione,  @Anno, @DataArrivo, @DataPartenza, @Cauzione, @Tariffa, @MezzaPensione, @PensioneCompleta, @PrimaColazione, @CodiceFiscaleCliente, @NumeroCamera)", connection))
                        {
                            command.Parameters.AddWithValue("@DataPrenotazione", model.DataPrenotazione);
                            command.Parameters.AddWithValue("@Anno", model.Anno);
                            command.Parameters.AddWithValue("@DataArrivo", model.DataArrivo);
                            command.Parameters.AddWithValue("@DataPartenza", model.DataPartenza);
                            command.Parameters.AddWithValue("@Cauzione", model.Cauzione);
                            command.Parameters.AddWithValue("@Tariffa", model.Tariffa);
                            command.Parameters.AddWithValue("@MezzaPensione", model.MezzaPensione);
                            command.Parameters.AddWithValue("@PensioneCompleta", model.PensioneCompleta);
                            command.Parameters.AddWithValue("@PrimaColazione", model.PrimaColazione);
                            command.Parameters.AddWithValue("@CodiceFiscaleCliente", model.CodiceFiscaleCliente);
                            command.Parameters.AddWithValue("@NumeroCamera", model.NumeroCamera);

                            command.ExecuteNonQuery();
                        }
                    }

                    return RedirectToAction("Index", "");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Si è verificato un errore durante la registrazione della prenotazione: {ex.Message}");
                    // Puoi gestire l'errore a tuo piacimento (ad esempio, registrare nei log).

                    return View("RegistraPrenotazione", model);
                }
            }

            // Se ci sono errori di validazione o se si verifica un errore durante il salvataggio nel database, mostra nuovamente il form.
            return View(model);
        }


    }

}
