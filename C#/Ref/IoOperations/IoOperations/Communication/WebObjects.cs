using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IoOperations.Communication
{
    public class WebObjects
    {
        // class fields
        private WebRequest _request;
        private WebResponse _response;

        /// <summary>
        /// Class constructor, takes in URI
        /// </summary>
        /// <param name="uri"></param>
        public WebObjects(string uri)
        {
            this._request = WebRequest.Create(uri);

            try
            {
                this._response = _request.GetResponse();
            }
            catch (WebException ex)
            {
                Console.WriteLine("Exception thrown: {0}", ex);
                Console.WriteLine("404 error returned!");
            }
        }

        /// <summary>
        /// Display response's HTML to console
        /// </summary>
        public void DisplayHtml()
        {
            try
            {
                StreamReader responseStream = new StreamReader(_response.GetResponseStream());
                string responseText = responseStream.ReadToEnd();

                Console.WriteLine(responseText);

                _response.Close();
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Exception thrown: {0}", ex);
                Console.WriteLine("Null exception!");
            }
        }
    }
}
