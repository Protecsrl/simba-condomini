using System;
using System.Diagnostics;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
//using Pop3;
namespace CAMS.Module.Classi
{
    public class ClientPopMail //POPGmail()
    {

        
    TcpClient m_tcpClient = new TcpClient();
    SslStream m_sslStream;
    int m_mailCount;
    byte[] m_buffer = new byte[8172];
    public ClientPopMail()
    {
        m_mailCount = -1;
    }
    /// <summary>
    /// Connect to pop3 server "host" using "port" and auth SSL as client.
    /// </summary>
    /// <param name="host"></param>
    /// <param name="port"></param>
    /// <returns></returns>
    public bool Connect(string host, int port)
    {
        m_tcpClient.Connect(host, port);
        m_sslStream = new SslStream(m_tcpClient.GetStream());
        m_sslStream.AuthenticateAsClient(host);
        // Read the stream to make sure we are connected
        int bytes = m_sslStream.Read(m_buffer, 0, m_buffer.Length);
        return (Encoding.ASCII.GetString(m_buffer, 0, bytes).Contains("+OK"));
    }
    /// <summary>
    /// Closes SSL and TCP connections.
    /// </summary>
    public void Disconnect()
    {
        //if(m_tcpClient.Connected)
        //    m_sslStream.Write(Encoding.ASCII.GetBytes("QUIT\r\n"));
        m_sslStream.Close();
        m_tcpClient.Close();
    }
    /// <summary>
    /// Logs into the pop3 server (when connected.)
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public bool Login(string username, string password)
    {
        if (!m_tcpClient.Connected)
            return false;
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            throw new ArgumentException("Username or Password was empty.");
        int bytesRead = -1;
        //Send the users login details
        m_sslStream.Write(Encoding.ASCII.GetBytes("USER " + username + "\r\n"));
        bytesRead = m_sslStream.Read(m_buffer, 0, m_buffer.Length);
        if (!Encoding.ASCII.GetString(m_buffer, 0, bytesRead).Contains("+OK"))
            return false;
        //Send the password                        
        m_sslStream.Write(Encoding.ASCII.GetBytes("PASS " + password + "\r\n"));
        bytesRead = m_sslStream.Read(m_buffer, 0, m_buffer.Length);
        if (!Encoding.ASCII.GetString(m_buffer, 0, bytesRead).Contains("+OK"))
            return false;
        return true;
    }
    /// <summary>
    /// Returns the number of emails in the inbox.
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// Returns the message data as a string.
    /// </summary>
    /// <param name="messageNumber"></param>
    /// <returns></returns>
    public string GetMessage(int messageNumber)
    {
        string ret = "";
       // m_sslStream.Write(Encoding.ASCII.GetBytes("RETR " + messageNumber.ToString() + "\r\n"));
        m_sslStream.Write(Encoding.ASCII.GetBytes(string.Format("RETR {0}\r\n", messageNumber)));
        int bytesRead = -1;
        while (bytesRead != 0 && !ret.Contains("\r\n.\r\n"))
        {
            bytesRead = m_sslStream.Read(m_buffer, 0, m_buffer.Length);
            string tempo = Encoding.ASCII.GetString(m_buffer, 0, bytesRead);
            //Debug.Print(tempo);
            ret += tempo;
            //ret += Encoding.ASCII.GetString(m_buffer, 0, bytesRead);
        }
        ret = ret.Remove(0, ret.IndexOf("\r\n") + 2);
        return ret;
    }

    }
}

//class POPGmail
//{


//}