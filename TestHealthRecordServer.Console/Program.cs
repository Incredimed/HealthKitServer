﻿using System;
using System.Net;
using Newtonsoft.Json;
using HealthKitServer;

namespace TestHealthKitServer.Console
{
	public class Program
	{
		public static void Main()
		{
			using(var wc = new WebClient())
			{
				wc.Headers[HttpRequestHeader.ContentType] = "application/json"; 

				var jsonString = JsonConvert.SerializeObject(new HealthKitData { PersonId = 9,
					BloodType = "A+",  DateOfBirth = "08.01.2015", DistanceReadings = new DistanceReading {
						TotalDistance = "40", TotalSteps = "500", TotalStepsOfLastRecording = 200, TotalFlightsClimed = "30", TotalDistanceOfLastRecording = 10, 
					}
				});

				var response = wc.UploadString("http://apollo.amosti.net:5002/api/v1/addpatient", jsonString);

			}
		}
	}
}
