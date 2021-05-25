using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CAMS.Module.ClassiAsinc
{
  public class InviaAsinc
    {
        public static async Task<int> GetPageLength(string url)
        {
            string text = await new System.Net.WebClient().DownloadStringTaskAsync(url);
            return text.Length;
        }
        private async Task HandleClickAsync()
        {
            // Can use ConfigureAwait here.
            await Task.Delay(1000).ConfigureAwait(continueOnCapturedContext: false);
        }

        //async Task<string> GetHtml(string url)
        //{
        //    System.Uri aa = new Uri("http://pa-eams.engie.it");
        //        if (string.IsNullOrEmpty(url))
        //    {
        //        return null;
        //    }
        //    //string html = await new System.Net.WebClient().(url);
        //     HttpClient client = new System.Net.WebClient();
        //    Task<string> myTask = client.GetHtml("http://...");
        //    await myTask;
        //    string html = myTask.Result;
        //    return html;
        //}
        //void DoOperation()
        //{
        //    SomeOperation();
        //}

        byte[] GetData(String s)
        {
            return GetData("/path/to/data");
        }
        async Task DoOperation()
        {
            await Task.Run(() => ShowThreadInfo("Task"));
        }

        async Task<byte[]> GetData()
        {
            return await Task.Run(() => GetData("/path/to/data"));
        }

        static void ShowThreadInfo(String s)
        {
            Console.WriteLine("{0} Thread ID: {1}",
                              s, Thread.CurrentThread.ManagedThreadId);
        }
    }
}
