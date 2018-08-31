using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;

namespace Scanner.Core.Services
{
    public class ScannerService
    {
        private HttpWebResponse _response;

        /// <summary>
        /// Gets response of a Http request and makes sure it has a content.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool GetResponse(string url)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Credentials = CredentialCache.DefaultCredentials;
            _response = (HttpWebResponse)webRequest.GetResponse();
            //-100 is because Dropbox files always return -1
            return _response.ContentLength > -100;
        }

        /// <summary>
        /// Returns the file size by reading content length from Http Request's (GetResponse(string url)) response.
        /// </summary>
        /// <returns></returns>
        public long GetFileSize()
        {
            return _response.ContentLength;
        }


        /* -------------ALTERNATIVE TO GetFileSize()--------- 
         *  public long GetFileSize(string stringUrl)
        {
            
            System.Net.WebClient wc = new System.Net.WebClient();
            wc.OpenRead(stringUrl);
            Int64 size = Convert.ToInt64(wc.ResponseHeaders["Content-Length"]);
            return size;
        } */

        // request.Headers.Range = new RangeHeaderValue(startByte, endByte)
        // https://stackoverflow.com/questions/122853/how-to-get-the-file-size-from-http-headers/122984#122984




        /// <summary>
        /// Reads sha1 of the in memory file by using Stream.
        /// </summary>
        /// <returns>Returns the sha1 string of a file.</returns>
        public string GetCheckSum()
        {
            byte[] shaHash;
            using (var shaForStream = new SHA256Managed())
            {
                var stream = _response.GetResponseStream();

                // Read the sha hash from memory
                using (Stream sourceStream = new CryptoStream(stream, shaForStream, CryptoStreamMode.Read))
                {
                    var buffer = new byte[1024];
                    var read = sourceStream.Read(buffer, 0, buffer.Length);
                    while (read > 0)
                    {
                        read = sourceStream.Read(buffer, 0, buffer.Length);
                    }
                    shaHash = shaForStream.Hash;
                    sourceStream.Flush();                  
                }
            }
            return Convert.ToBase64String(shaHash);
        }

    }
}
