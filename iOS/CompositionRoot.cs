﻿using System;

namespace HealthKitServer.iOS
{
	public class CompositionRoot
	{
		public CompositionRoot ()
		{
			var container = Container.Instance = new SimpleContainer (); 
			container.RegisterSingleton<IHealthKitAccess> (new HealtKitAccess());
			container.Register<IHealthKitDataWebService, HealthKitDataWebService> ();
			HealthKitDataContext.ActiveHealthKitData = new HealthKitData{Id=3, DistanceReadings = new DistanceReading{}};
		}
	}
}

