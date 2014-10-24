using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MySql.Data.MySqlClient;
using System.Net.Mail;


namespace Alluminati.Controllers
{
    public class PagoServicioController : Controller
    {
        //
        // GET: /PagoServicio/
        public ActionResult Index()
        {
            return View();
        }

        #region Variables
        MySqlConnection connection = new MySqlConnection("server=localhost; database= web; Uid=root; pwd= javier;");
        #endregion

        public bool PagarLuz(string nombre)
        {
            MySqlCommand actualizar = new MySqlCommand("spServicioLuz", connection);
            actualizar.CommandType = CommandType.StoredProcedure;
            actualizar.Parameters.AddWithValue("@nombreU", nombre);

            try
            {
                connection.Open();
                actualizar.ExecuteNonQuery();

                if (EnviarEmail(nombre, "Pago de Luz por medio del servicio Alluminati ", "Hola " + nombre + "\nUsted pago su servicio de Luz el día " + DateTime.Now.ToString()
                    + " con el código \nde comprobación 03XVCLUZPAGO89 \nGracias por usar nuestro servicio."))
                    return true;
                return true;
            }
            catch (Exception) { return false; }
            finally { connection.Close(); }
        }

        bool EnviarEmail(string nombre, string subject, string body)
        {
            var userTo = "";
            MySqlCommand buscarEmail = new MySqlCommand("spBuscarEmail", connection);
            buscarEmail.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter datos = new MySqlDataAdapter();
            datos.SelectCommand = buscarEmail;
            DataSet conjunto = new DataSet();
            buscarEmail.Parameters.AddWithValue("@nombreU", userTo);
            try
            {
                connection.Open();
                datos.Fill(conjunto, "Datos");
                connection.Close();
                userTo = conjunto.Tables[0].Rows[0][1].ToString(); //Posicion del email del usuario

                MailMessage email = new MailMessage("alluminaticompany@gmail.com", userTo);
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "smtp.google.com";
                email.Subject = subject;
                email.Body = body;
                client.Send(email);

                return true;
            }
            catch (Exception) { return false; }

        }
    }
}