using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Net.Security;

namespace Pop3
{
    public class Pop3Client
    {
        private Pop3Credential m_credential;

        private const int m_pop3port = 110;// 995;//110
        private const int MAX_BUFFER_READ_SIZE = 256;

        private long m_inboxPosition = 0;
        private long m_directPosition = -1;

        private Socket m_socket = null;
        private int ContaMail = 0;
        /// <summary>
        ///  nuovo
        TcpClient m_tcpClient = new TcpClient();
        SslStream m_sslStream;
        int m_mailCount;
        byte[] m_buffer = new byte[8172];
        int port = 995;
        string host = "pop3s.aruba.it";
        /// </summary>
        private Pop3Message m_pop3Message = null;

        public Pop3Credential UserDetails
        {
            set { m_credential = value; }
            get { return m_credential; }
        }

        public string Received
        {
            get { return m_pop3Message.Received; }
        }
        public string MessageID
        {
            get { return m_pop3Message.MessageID; }
        }


        public string Date
        {
            get { return m_pop3Message.Date; }
        }

        public string ID
        {
            get { return m_pop3Message.ID; }
        }
        public string From
        {
            get { return m_pop3Message.From; }
        }

        public string To
        {
            get { return m_pop3Message.To; }
        }

        public string Subject
        {
            get { return m_pop3Message.Subject; }
        }

        public string Body
        {
            get { return m_pop3Message.Body; }
        }

        public IEnumerator MultipartEnumerator
        {
            get { return m_pop3Message.MultipartEnumerator; }
        }

        public bool IsMultipart
        {
            get { return m_pop3Message.IsMultipart; }
        }


        public Pop3Client(string user, string pass, string server)
        {
            m_credential = new Pop3Credential(user, pass, server);
        }

        private Socket GetClientSocket()
        {           
            Socket s = null;            
            try
            {
                m_tcpClient.Connect(host, port);
                s = m_tcpClient.Client;
                m_sslStream = new SslStream(m_tcpClient.GetStream());
                m_sslStream.AuthenticateAsClient(host);
                // Read the stream to make sure we are connected
                int bytes = m_sslStream.Read(m_buffer, 0, m_buffer.Length);
                // line = Encoding.ASCII.GetString(m_buffer, 0, bytes);
                if (!Encoding.ASCII.GetString(m_buffer, 0, bytes).Contains("+OK"))
                {
                    throw new Pop3ConnectException("non connesso");
                }

            }
            catch (Exception e)
            {
                throw new Pop3ConnectException(e.ToString());
            }

            if (s == null)
            {
                throw new Pop3ConnectException("Error : connecting to " + m_credential.Server);
            }

            return s;
        }

        private string GetPop3String(string strSendCommand, int messageNumber)
        {

            if (m_socket == null)
            {
                throw new
                    Pop3MessageException("Connection to POP3 server is closed");
            }

            string ret = "";
            // m_sslStream.Write(Encoding.ASCII.GetBytes("RETR " + messageNumber.ToString() + "\r\n"));
            m_sslStream.Write(Encoding.ASCII.GetBytes(string.Format("{0} {1}\r\n", strSendCommand, messageNumber)));
            if (messageNumber == 0)
            {
                m_sslStream.Write(Encoding.ASCII.GetBytes(string.Format("{0} \r\n", strSendCommand)));
            }
            int bytesRead = -1;
            while (bytesRead != 0 && !ret.Contains("\r\n.\r\n") && !ret.Contains(@"\r\n") && !ret.Contains(@"OK")) //"+OK 1 2791\r\n"
            {
                bytesRead = m_sslStream.Read(m_buffer, 0, m_buffer.Length);
                string tempo = Encoding.ASCII.GetString(m_buffer, 0, bytesRead);
                //Debug.Print(tempo);
                ret += tempo;
                //ret += Encoding.ASCII.GetString(m_buffer, 0, bytesRead);
            }
            if (ret.Contains(@"OK"))
            {
                ret = ret.Remove(ret.IndexOf("\r\n") , 2);
            }
            else
            {
                ret = ret.Remove(0, ret.IndexOf("\r\n") + 2);
            }                
            return ret;  
        }
   


        private void LoginToInbox()
        {
            string returned;

            if (!m_tcpClient.Connected)
            {
                throw new Pop3LoginException("login/password not accepted");
            }
            if (string.IsNullOrEmpty(m_credential.User) || string.IsNullOrEmpty(m_credential.Pass))
                throw new ArgumentException("Username or Password was empty.");
            int bytesRead = -1;
            //Send the users login details
            m_sslStream.Write(Encoding.ASCII.GetBytes(string.Format("USER {0}\r\n", m_credential.User)));
            bytesRead = m_sslStream.Read(m_buffer, 0, m_buffer.Length);
            if (!Encoding.ASCII.GetString(m_buffer, 0, bytesRead).Contains("+OK"))
            {
                throw new Pop3LoginException("login/password not accepted");
            }
            //Send the password                        
            m_sslStream.Write(Encoding.ASCII.GetBytes(string.Format("PASS {0}\r\n", m_credential.Pass)));
            bytesRead = m_sslStream.Read(m_buffer, 0, m_buffer.Length);
            if (!Encoding.ASCII.GetString(m_buffer, 0, bytesRead).Contains("+OK"))
            {
                throw new Pop3LoginException("login/password not accepted");
            }         
           
        }

        public void CloseConnection()
        {
            try
            {
                if (m_tcpClient.Connected)
                    m_sslStream.Write(Encoding.ASCII.GetBytes("QUIT\r\n"));
                m_sslStream.Close();
                m_tcpClient.Close();
                //Send("quit");
                m_socket = null;
                m_pop3Message = null;
            }
            catch {
                throw new Pop3MessageException("Pop3 server non puo essere discomessa");
            }
        }

        public bool DeleteEmail()
        {
            bool ret = false;
            int messageNumber = int.Parse(m_inboxPosition.ToString());
            string StrSend = "dele ";
            string returned = GetPop3String(StrSend, messageNumber);
 
            if (Regex.Match(returned,
                @"^.*\+OK.*$").Success)
            {
                ret = true;
            }

            return ret;
        }

        public bool NextEmail(long directPosition)
        {
            bool ret;

            if (directPosition >= 0)
            {
                m_directPosition = directPosition;
                ret = NextEmail();
            }
            else
            {
                throw new Pop3MessageException("Position less than zero");
            }

            return ret;
        }

        public bool NextEmail()
        {
            if (m_inboxPosition == ContaMail)
                return false;
            string returned;

            long pos;

            if (m_directPosition == -1)
            {
                if (m_inboxPosition == 0)
                {
                    pos = 1;
                }
                else
                {
                    pos = m_inboxPosition + 1;
                }
            }
            else
            {
                pos = m_directPosition + 1;
                m_directPosition = -1;
            }

            long size = 0;
            int messageNumber = int.Parse(pos.ToString());
                string StrSend = "list";
            if (pos == 1)
            {               
                returned = GetPop3String(StrSend, messageNumber);
                // if email does not exist at this position
                // then return false ...
                if (returned.Substring(0, 4).Equals("-ERR"))
                {
                    return false;
                }

                m_inboxPosition = pos;
                // strip out CRLF ...
                string[] noCr = returned.Split(new char[] { '\r' });

                // get size ...
                string[] elements = noCr[0].Split(new char[] { ' ' });

                 size = 0;
                try
                {
                    size = long.Parse(elements[2]);
                }
                catch
                {
                    size = long.Parse(elements[1]);
                }
            }
            m_inboxPosition = pos;
            messageNumber = int.Parse(pos.ToString());
            StrSend = "retr ";
            //string messaggio = GetPop3String(StrSend, messageNumber);
              Console.WriteLine("------------------:" );
            //Console.WriteLine("messaggio:" + messaggio);
              Console.WriteLine("-----------:" );
            // ... else read email data
            m_pop3Message = new Pop3Message(messageNumber, size, m_socket, m_sslStream, m_buffer);

            return true;
        }


        public void OpenInbox()
        {
            // get a socket ...
            m_socket = GetClientSocket();
            // send login details ...
            LoginToInbox();

            ContaMail = GetMailCount();
        }

        public int GetMailCount()
        {
            m_sslStream.Write(Encoding.ASCII.GetBytes("STAT\r\n"));
            int bytesRead = m_sslStream.Read(m_buffer, 0, m_buffer.Length);
            string data = Encoding.ASCII.GetString(m_buffer, 0, bytesRead);
            char[] str = Encoding.ASCII.GetChars(m_buffer);
            string data1 = str.ToString();
            if (data.Contains("+OK"))
            {
                data = data.Remove(0, 4);
                string[] temp = data.Split(' ');
                int r;
                if (Int32.TryParse(temp[0], out r))
                {
                    m_mailCount = r;
                    return r;
                }
            }
            return -1;
        }



    }
}
