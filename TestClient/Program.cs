using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
	class Program
	{
		static void Main(string[] args)
		{
			string answer = "";

			//string baseUrl = "http://localhost:56539/";
			string baseUrl = "http://169.254.80.80/winauth/";

			string getUrl = baseUrl + "api/Values/1";
			string postUrl = baseUrl + "api/Values";

			try
			{
				// Way 1
				WebRequest req = WebRequest.Create(getUrl);
				req.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
				req.Credentials = CredentialCache.DefaultNetworkCredentials;
				WebResponse resp = req.GetResponse();
				StreamReader reader = new StreamReader(resp.GetResponseStream());
				answer = reader.ReadToEnd().Trim() + System.Environment.NewLine;

				// Way 2
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getUrl);
				request.Credentials = CredentialCache.DefaultNetworkCredentials;
				using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
				using (Stream stream = response.GetResponseStream())
				using (StreamReader reader1 = new StreamReader(stream))
				{
					answer += reader1.ReadToEnd() + System.Environment.NewLine;
				}


				// POST
				var httpWebRequest = (HttpWebRequest)WebRequest.Create(postUrl);
				httpWebRequest.Credentials = CredentialCache.DefaultNetworkCredentials;
				httpWebRequest.ContentType = "application/json";
				httpWebRequest.Method = "POST";

				using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
				{
					var param1 = "Hello";
					var param2 = "World.";
					var param3 = "Nice To";
					var param4 = "meet you.";

					/*NOTE: If the commas are not at the end of each property, it can fail silently (they will default to nulls) */
					string json = "{\"item1\":\"" + param1 + "\"," +
										   "\"item2\":\"" + param2 + "\"," +
										   "\"item3\":\"" + param3 + "\"," +
										   "\"item4\":\"" + param4 + "\"}";

					streamWriter.Write(json);
					streamWriter.Flush();
					streamWriter.Close();
				}

				var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					answer += streamReader.ReadToEnd();					
				}




			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}

			

			Console.WriteLine(answer);
			Console.ReadLine();

		}
	}
}
