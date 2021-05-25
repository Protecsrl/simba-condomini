using System.Linq;
using System;
using System.Net.Mail;

using System.Collections.Generic;
using System.IO;
using System.Reflection;


namespace CAMS.Module.Classi
{
    public static class MailMessageExt
    {
        public static void Save(this MailMessage Message, string FileName)
        {
            var assembly = typeof(SmtpClient).Assembly;
            var _mailWriterType = assembly.GetType("System.Net.Mail.MailWriter");

            using (var _fileStream = new FileStream(FileName, FileMode.Create))
            {
                var _mailWriterContructor =
                    _mailWriterType.GetConstructor(
                        BindingFlags.Instance | BindingFlags.NonPublic,
                        null,
                        new Type[] { typeof(Stream) },
                        null);

                var _mailWriter = _mailWriterContructor.Invoke(new object[] { _fileStream });

                var _sendMethod =
                typeof(MailMessage).GetMethod(
                        "Send",
                        BindingFlags.Instance | BindingFlags.NonPublic);

                _sendMethod.Invoke(
                    Message,
                    BindingFlags.Instance | BindingFlags.NonPublic,
                    null,
                    new object[] { _mailWriter, true },
                    null);

                var _closeMethod =
                    _mailWriter.GetType().GetMethod(
                        "Close",
                        BindingFlags.Instance | BindingFlags.NonPublic);

                _closeMethod.Invoke(
                    _mailWriter,
                    BindingFlags.Instance | BindingFlags.NonPublic,
                    null,
                    new object[] { },
                    null);
            }
        }
    }
}
