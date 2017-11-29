using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaGestionCEM.Negocio
{
    public class Email
    {
        public static IRestResponse ResultadoPostulacion(string nombre, string correo, string nombrePrograma, bool resultado)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
            new HttpBasicAuthenticator("api",
                                      "key-bf6538e45b46f9eb5c7672db4491ed33");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "sandbox9a827558ef0943dba832f79f2bfb6f15.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Centro de Estudios Montreal <auto-respuesta@bot.cem.org>");
            request.AddParameter("to", nombre + " <" + correo + ">");
            request.AddParameter("subject", "Resultado de su postulación al programa de intercambio");
            request.AddParameter("text", mensajeRespuesta(nombrePrograma, resultado));
            request.Method = Method.POST;
            return client.Execute(request);
        }

        public static IRestResponse RegistroExitoso(string nombre, string correo, string nombreUsuario)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
            new HttpBasicAuthenticator("api",
                                      "key-bf6538e45b46f9eb5c7672db4491ed33");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "sandbox9a827558ef0943dba832f79f2bfb6f15.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Centro de Estudios Montreal <auto-respuesta@bot.cem.org>");
            request.AddParameter("to", nombre + " <" + correo + ">");
            request.AddParameter("subject", "Registro de su nueva cuenta");
            request.AddParameter("text", MensajeRegistroExitoso(nombreUsuario));
            request.Method = Method.POST;
            return client.Execute(request);
        }



        private static string MensajeRegistroExitoso(string nombreUsuario)
        {
            string message = "Su nueva cuenta ha sido creada! "+
                    "Su nombre de usuario es: '"+ nombreUsuario + "'";
           
            return message;

        }

        private static string mensajeRespuesta(string nombrePrograma, bool resultado)
        {
            string mensaje;
            if (resultado)
                mensaje = "Felicitaciones su postulacion al programa de estudios '" 
                    + nombrePrograma + "' ha sido aprobada";
            else
                mensaje = "Lamentablemente su postulacion al programa de estudios '"
                    + nombrePrograma + "' ha sido rechazada";
            return mensaje;
        }
    }
}
