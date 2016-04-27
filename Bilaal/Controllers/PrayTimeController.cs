using System;
using System.Net.Http;
using System.Threading.Tasks;
using Bilaal.ObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bilaal.Controllers
{
	public static class PrayTimeController
	{
		public static async Task<Timings> GetTimings(string location, string date)
		{
			using (var client = new HttpClient())
			{

			string url = $"http://api.aladhan.com/timings/{FormatDate(date)}?{FormatLocation(location)}&method=2";

				HttpResponseMessage message = await client.GetAsync(url);

				if (message.IsSuccessStatusCode)
				{
					var jsonResponse = await message.Content.ReadAsStringAsync();
					var json = JObject.Parse(jsonResponse);

					var timings = JsonConvert.DeserializeObject<Timings>(json["data"]["timings"].ToString());

					return timings;
				}

				return null;
				//return $"Failed! with status code: {message.StatusCode}";
			}
		}

		private static object FormatLocation(string location)
		{
			return "latitude=34.052234&longitude=-118.243685&timezonestring=America/Los_Angeles";
		}

		private static long FormatDate(string date)
		{
			return DateTimeOffset.Now.ToUnixTimeSeconds();
		}
	}
}