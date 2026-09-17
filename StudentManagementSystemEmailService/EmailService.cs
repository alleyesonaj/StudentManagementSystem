using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using StudentManagementSystemModels;

namespace StudentManagementSystemEmailService
{
    /// <summary>
    /// Sends notification emails through Mailtrap's sandbox SMTP server.
    /// Sandbox mail is captured in your Mailtrap inbox and never delivered to real recipients.
    /// </summary>
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration
                ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Sends a notification that a new student record was created.
        /// </summary>
        public void SendStudentAddedEmail(Student student, string recipientEmail)
        {
            string body =
                $"A new student record has been created.\n\n" +
                $"Student ID : {student.StudentID}\n" +
                $"Name       : {student.Name}\n" +
                $"Status     : {student.Status}\n\n" +
                $"Recorded on {DateTime.Now:yyyy-MM-dd HH:mm}.";

            Send("New Student Registered", body, recipientEmail);
        }

        /// <summary>
        /// Sends a notification that a student's status changed.
        /// </summary>
        public void SendStatusUpdateEmail(Student student, string oldStatus, string recipientEmail)
        {
            string body =
                $"A student status has been updated.\n\n" +
                $"Student ID : {student.StudentID}\n" +
                $"Name       : {student.Name}\n" +
                $"Previous   : {oldStatus}\n" +
                $"Current    : {student.Status}\n\n" +
                $"Updated on {DateTime.Now:yyyy-MM-dd HH:mm}.";

            Send("Student Status Updated", body, recipientEmail);
        }

        /// <summary>
        /// Sends a notification that a student record was removed.
        /// </summary>
        public void SendStudentRemovedEmail(int studentId, string studentName, string recipientEmail)
        {
            string body =
                $"A student record has been removed from the system.\n\n" +
                $"Student ID : {studentId}\n" +
                $"Name       : {studentName}\n\n" +
                $"Removed on {DateTime.Now:yyyy-MM-dd HH:mm}.";

            Send("Student Record Removed", body, recipientEmail);
        }

        /// <summary>
        /// Core send routine. All public methods funnel through here.
        /// </summary>
        public void Send(string subject, string bodyText, string recipientEmail)
        {
            if (string.IsNullOrWhiteSpace(recipientEmail))
                throw new ArgumentException("Recipient email cannot be empty.", nameof(recipientEmail));

            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(
                _configuration["EmailSettings:FromName"],
                _configuration["EmailSettings:FromEmail"]
            ));

            message.To.Add(new MailboxAddress("Registrar", recipientEmail));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = bodyText };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(
                        _configuration["EmailSettings:SmtpHost"],
                        int.Parse(_configuration["EmailSettings:SmtpPort"] ?? "2525"),
                        SecureSocketOptions.StartTls
                    );

                    client.Authenticate(
                        _configuration["EmailSettings:Username"],
                        _configuration["EmailSettings:Password"]
                    );

                    client.Send(message);
                }
                catch (Exception ex)
                {
                    // Email failure should not crash the app — log and move on.
                    Console.WriteLine($"[EMAIL] Could not send notification: {ex.Message}");
                }
                finally
                {
                    if (client.IsConnected)
                        client.Disconnect(true);
                }
            }
        }
    }
}
