﻿using System;
using Nancy;
using Nancy.ModelBinding;
using HealthKitServer.Server;
using System.Collections.Generic;
using Nancy.Routing;

namespace HealthKitServer.Server
{
	public class HealthKitServerModule : NancyModule
	{
		public HealthKitServerModule(IRouteCacheProvider routeCacheProvider) 
		{

			Get["/api/v1"] = parameters =>
			{
				return Negotiate
					.WithModel(routeCacheProvider.GetCache().RetrieveMetadata<HealthKitServerMetadataModule>());

			};


			Post["/api/v1/addPatient"] = parameters =>
			{
				try
				{
					var person = this.Bind<HealthKitData>();	 
					Container.Singleton<IHealthInfoDataStorage>().AddOrUpdatePersonHealthInfoToStorage(person);
					return Response.AsJson(person);
				}
				catch(Exception e)
				{
					return Response.AsText(e.Message);
				}
			};

			Get["/api/v1/getAllPatients"] = parameters => 
			{
				return Response.AsJson (Container.Singleton<IHealthInfoDataStorage> ().GetAllPersons());
			};

			Get["/api/v1/getPatient"] = parameters => 
			{
				var id = this.Request.Query["id"];
				int number; 
				if(int.TryParse(id, out number))
				{
					return Response.AsJson(Container.Singleton<IHealthInfoDataStorage>().GetPatientHealthInfo(number));

				}
				return "Invalid query";

			};
		}
	}
}

