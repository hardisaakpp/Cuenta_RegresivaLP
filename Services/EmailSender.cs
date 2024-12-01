using System.Net;
using System.Net.Mail;

namespace LandingServer.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        // Constructor que recibe la configuración de la aplicación
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Método para enviar un correo electrónico
        public async Task SendEmailAsync(string name, string email, string phone)
        {
            // Obtener la configuración de SMTP
            var smtpSettings = _configuration.GetSection("SmtpSettings");
            string smtpHost = smtpSettings["Host"];
            int smtpPort = int.Parse(smtpSettings["Port"]);
            bool enableSsl = bool.Parse(smtpSettings["EnableSsl"]);
            string smtpUser = smtpSettings["UserName"];
            string smtpPass = smtpSettings["Password"];
            string smtpFrom = smtpSettings["From"];

            // Crear el mensaje de correo para Milena
            var mailMessageToMilena = new MailMessage
            {
                From = new MailAddress(smtpUser, "Eventos Dharma"), // smtpFrom es el correo electrónico del remitente
                Subject = "Formulario de contacto",
                Body = $"Hola Milena,\n\nHas recibido un nuevo formulario de contacto:\n\nNombre: {name}\nCorreo: {email}\nTeléfono: {phone}\n\nSaludos,\nEquipo de Eventos Dharma",
                IsBodyHtml = false
            };
            mailMessageToMilena.To.Add("milena.murillo@grupodharma.org");

            // Crear el mensaje de correo de agradecimiento para el usuario
            var mailMessageToUser = new MailMessage
            {
                From = new MailAddress(smtpUser, "Eventos Dharma"),
                Subject = "¡Gracias por registrarte!",
                Body = $"Hola {name},\n\n ✨ ¡Gracias por unirte a nuestra comunidad! \n\n 📞 Muy pronto, uno de nuestros especialistas se pondrá en contacto contigo.\n\nSi necesitas algo mientras tanto, contáctanos por 📧milena.murillo@grupodharma.org o 📱 0961347748.\n\n🚀 ¡Estamos aquí para ayudarte a lograr grandes cosas!\n\nSaludos,\nEquipo de Eventos Dharma",
                IsBodyHtml = false
            };
            mailMessageToUser.To.Add(email);
            
            // Enviar el correo electrónico
            using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(smtpUser, smtpPass);
                smtpClient.EnableSsl = enableSsl;

                await smtpClient.SendMailAsync(mailMessageToMilena);
                await smtpClient.SendMailAsync(mailMessageToUser);
            }
        }
    }
}
