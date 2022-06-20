using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


/* Laut.FM News Downloader
 * Copyright (c) 2022 FriesenRadio.de
 * 
 * 
 * Permission is hereby granted, free of charge,
 * to any person obtaining a copy of this software and
 * associated documentation files (the "Software"),
 * to deal in the Software without restriction,
 * including without limitation the rights to use, copy,
 * modify, merge, publish, distribute, sublicense,
 * and/or sell copies of the Software, and to permit
 * persons to whom the Software is furnished to do so,
 * subject to the following conditions:
 * 
 * The above copyright notice and this permission notice
 * shall be included in all copies or substantial portions
 * of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 */

namespace Laut_News
{
    class Program
    {

        public static string Download_URL="";
        public static string Download_To="";
        public static string StationName="";
        public static string Password="";
        static void Main(string[] args)
        {
            IniFile ini = new IniFile(Path.Combine( Directory.GetCurrentDirectory()+"\\") + "config.ini");
            Download_URL = ini.IniReadValue("Global", "Download_URL");
            Download_To = ini.IniReadValue("Global", "Download_to");
            StationName = ini.IniReadValue("Global", "StationName");
            Password = ini.IniReadValue("Global", "LivePassword");

            if (Download_URL=="" || Download_To =="" || StationName =="" || Password =="" ){
                Trace.Msg("Configuration Error");
                Environment.Exit(-1);

            }


            string currentDirectory = Path.GetDirectoryName(Download_To);
            Directory.CreateDirectory(currentDirectory);

            if (File.Exists(Download_To))
            {
                // If file found, delete it    
                File.Delete(Download_To);
                Trace.Msg("Old File deleted.");
            }

            using (var webClient = new WebClient())
            {
                var token = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(StationName + ":" + Password));
                webClient.Headers.Add("Authorization", "Basic " + token);
                webClient.Headers[HttpRequestHeader.ContentType] = "application/octet-stream";
                webClient.Headers[HttpRequestHeader.UserAgent] = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";

              

                try
                {
                    webClient.DownloadFile(Download_URL, Download_To);
                    Trace.Msg("Donload OK");
                }
                catch (Exception e)
                {
                    Trace.Msg("ERROR : " + e.Message );
                }

            }

          

        }
    }
}
