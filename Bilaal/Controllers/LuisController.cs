using Bilaal.ObjectModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Bilaal.Controllers
{
	public class LuisController
	{
		public static async Task<Luis> ParseUserInput(string query)
		{

			using (var client = new HttpClient())
			{
				string url = $"https://api.projectoxford.ai/luis/v1/application?id=e52e48c0-8a7a-4254-8119-8f00568cd2c4&subscription-key=29bac826a01146acaeb98702ffdae385&q={Uri.EscapeDataString(query)}";

				HttpResponseMessage message = await client.GetAsync(url);

				if (message.IsSuccessStatusCode)
				{
					var jsonResponse = await message.Content.ReadAsStringAsync();

					var data = JsonConvert.DeserializeObject<Luis>(jsonResponse);

					return data;
				}

				return null;
			}
		}
	}
}