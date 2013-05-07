// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VendRequest.cs" company="Chip Willman">
//   Chip Willman
// </copyright>
// <summary>
//   Defines the VendRequest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VendAPI
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;

    /// <summary>
    /// The vend request.
    /// </summary>
    public class VendRequest
    {
        public VendRequest(string url, string username, string password)
        {
            this.Url = url;
            this.Username = username;
            this.Password = password;
        }

        public string Url { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }

        public string Get(string path)
        {
            using (var request = new WebClient())
            {
                request.Credentials = new NetworkCredential(this.Username, this.Password);
                var response = request.DownloadString(string.Format("{0}{1}", this.Url, path));
                return response;
            }
        }

        public string PostRequest(string path, string data)
        {
            try
            {
                using (var request = new WebClient())
                {
                    request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(
                                                           Encoding.ASCII.GetBytes(this.Username + ":" + this.Password));
                    request.Headers["Content-Type"] = "application/json";
                    
                    var response = request.UploadData(this.Url + path, "POST", Encoding.Default.GetBytes(data));
                    return Encoding.Default.GetString(response);
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string Post(string path, string data)
        {
            try
            {
                var request = WebRequest.Create(this.Url + path);
                request.Method = "POST";
                string authInfo = this.Username + ":" + this.Password;
                request.Credentials = new NetworkCredential(this.Username, this.Password);

                request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(authInfo));
                request.ContentType = "application/json";
                var postString = data;
                request.ContentLength = postString.Length;

                StreamWriter writer = new StreamWriter(request.GetRequestStream());
                writer.Write(postString);
                writer.Close();

                string postResponse;

                var response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    postResponse = reader.ReadToEnd();
                    reader.Close();
                }

                return postResponse;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }



        public object PostWebRequest(string path)
        {
            WebRequest request = WebRequest.Create(this.Url + path);

            request.ContentType = "Content-type: text/xml";
            request.Method = "POST";
            string authInfo = this.Username + ":" + this.Password;
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(authInfo));

            byte[] buffer = Encoding.GetEncoding("UTF-8").GetBytes("<workspace><name>my_workspace</name></workspace>");
            Stream reqstr = request.GetRequestStream();
            reqstr.Write(buffer, 0, buffer.Length);
            reqstr.Close();

            WebResponse response = request.GetResponse();
            return response;
        }
    }
}
