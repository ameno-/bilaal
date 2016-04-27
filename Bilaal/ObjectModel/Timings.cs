using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bilaal.ObjectModel
{

	public class TimingsRootobject
	{
		public Timings timings { get; set; }
	}

	public class Timings
	{
		public string Fajr { get; set; }
		public string Sunrise { get; set; }
		public string Dhuhr { get; set; }
		public string Asr { get; set; }
		public string Sunset { get; set; }
		public string Maghrib { get; set; }
		public string Isha { get; set; }
	}	
}