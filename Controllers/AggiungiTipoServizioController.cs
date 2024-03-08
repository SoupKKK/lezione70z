using lezione70z.Models;
using lezione70z;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using System;

public class AggiungiTipoServizioController : Controller
{
    private static readonly string connectionString = DataAccess.ConnectionString;

    public ActionResult AggiungiTipoServizio(int NumeroCamera, string CodiceFiscale)
    {
        var model = new AggiungiTipoServizioViewModel
        {
            NumeroCamera = NumeroCamera,
            CodiceFiscaleCliente = CodiceFiscale,
            TipiServizi = GetTipiServiziList()
        };
        return View(model);
    }

    private IEnumerable<SelectListItem> GetTipiServiziList()
    {
        List<SelectListItem> tipiServiziList = new List<SelectListItem>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand("SELECT ID, NomeServizio FROM TipiServizi", connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int tipoServizioID = reader.GetInt32(reader.GetOrdinal("ID"));
                    string nomeServizio = reader.GetString(reader.GetOrdinal("NomeServizio"));

                    tipiServiziList.Add(new SelectListItem { Value = tipoServizioID.ToString(), Text = nomeServizio });
                }
            }
        }

        return tipiServiziList;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult SalvaTipoServizio(AggiungiTipoServizioViewModel model, int NumeroCamera)
    {
        if (!ModelState.IsValid)
        {

            // Aggiungi messaggi di errore alla ModelState se necessario
            model.TipiServizi = GetTipiServiziList();
            return View("AggiungiTipoServizio", model);
        }

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(
                "INSERT INTO ServiziAggiuntivi (DataServizio, Quantita, CodiceFiscaleCliente, NumeroCamera, TipoServizioID) " +
                "VALUES (@DataServizio, @Quantita, @CodiceFiscaleCliente, @NumeroCamera, @TipoServizioID)", connection))
                {
                    command.Parameters.AddWithValue("@DataServizio", DateTime.Now);
                    command.Parameters.AddWithValue("@Quantita", model.Quantita);
                    command.Parameters.AddWithValue("@CodiceFiscaleCliente", model.CodiceFiscaleCliente);
                    command.Parameters.AddWithValue("@NumeroCamera", model.NumeroCamera);
                    command.Parameters.AddWithValue("@TipoServizioID", model.TipoServizioID);

                    command.ExecuteNonQuery();
                }

            }

            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Si è verificato un errore durante il salvataggio.");
        }

        model.TipiServizi = GetTipiServiziList();
        return View("AggiungiTipoServizio", model);
    }
}
