using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Bilaal.Controllers;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Utilities;
using Bilaal.ObjectModel;

namespace Bilaal
{
	[BotAuthentication]
	public class MessagesController : ApiController
	{
		/// <summary>
		/// POST: api/Messages
		/// Receive a message from a user and reply to it
		/// </summary>
		public async Task<Message> Post([FromBody]Message message)
		{
			if (message.Type == "Message")
			{
				// return our reply to the user

				Luis luisObject = await LuisController.ParseUserInput(message.Text);

				string bilaalSays = string.Empty;
				string enteredMessage = message.Text;

				if (luisObject.intents.Count() > 0)
				{
					switch (luisObject.intents[0].intent)
					{
					case "GetPrayerTime":
						//lastLocation = message.GetBotUserData<string>("LastLocation");
						//lastQuery = message.GetBotUserData<string>("LastQuery");

						string timeFrame = "¯\\_(ツ)_/¯";
						string prayerType = "¯\\_(ツ)_/¯";
						string location = "¯\\_(ツ)_/¯";
						string time = "¯\\_(ツ)_/¯";

						foreach (var item in luisObject.entities)
						{
							var type = item.type.Split(':');

							if (type[0] == "TimeFrame")
							{
								timeFrame = type[2].ToString();
							}

							if (type[0] == "PrayerTime")
							{
								prayerType = type[2].ToString();
							}
						}

						Timings timings = await PrayTimeController.GetTimings(location, timeFrame);

						switch (prayerType.ToLower())
						{
						case "fajr":
							time = timings.Fajr;
							break;
						case "dhuhr":
							time = timings.Dhuhr;
							break;
						case "asr":
							time = timings.Asr;
							break;
						case "maghrib":
							time = timings.Maghrib;
							break;
						case "isha":
							time = timings.Isha;
							break;
						}
						
						bilaalSays = $"The time for {prayerType} is {time}";
						break;
						//add cases for more complex queries
					default:
						bilaalSays = "Sorry, I don't understand...";
						break;
					}
				}
				else
				{
					bilaalSays = "Sorry, I don't understand...";
				}

				Message ReplyMessage = message.CreateReplyMessage(bilaalSays);

				return ReplyMessage;
			}
			else
			{
				return HandleSystemMessage(message);
			}
		}

		private Message HandleSystemMessage(Message message)
		{
			if (message.Type == "Ping")
			{
				Message reply = message.CreateReplyMessage();
				reply.Type = "Pong";
				return reply;
			}
			else if (message.Type == "DeleteUserData")
			{
				// Implement user deletion here
				// If we handle user deletion, return a real message
			}
			else if (message.Type == "BotAddedToConversation")
			{
			}
			else if (message.Type == "BotRemovedFromConversation")
			{
			}
			else if (message.Type == "UserAddedToConversation")
			{
				Message reply = message.CreateReplyMessage("Salaams, I'm Bilaal. AMA");
				reply.Type = "UserAddedToConversation";
				return reply;
			}
			else if (message.Type == "UserRemovedFromConversation")
			{
			}
			else if (message.Type == "EndOfConversation")
			{
			}

			return null;
		}
	}
}