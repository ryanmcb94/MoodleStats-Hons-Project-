using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;
using MoodleObjects;

namespace MoodleStats
{
	public class Controller
	{
		private static Store store;
        private static Controller control;
		public HonsServiceClient service  { get; set; }
        public MoodleUser user { get; set; }
        private static readonly EndpointAddress EndPoint = new EndpointAddress("http://ryanmcbroom.eu/Service/HonsService.svc");
        private BasicHttpBinding binding = CreateBasicHttp();

        public Controller()
        {
            service = new HonsServiceClient(binding,EndPoint);
			store = new Store();
        }
        
        private static BasicHttpBinding CreateBasicHttp()
        {
            BasicHttpBinding binding = new BasicHttpBinding
            {
                Name="BasicHttpBinding",
                MaxBufferSize=2147483647,
                MaxReceivedMessageSize=214783647
            };
            TimeSpan timeout = new TimeSpan(0, 0,5000);
            binding.SendTimeout = timeout;
            binding.OpenTimeout = timeout;
            binding.ReceiveTimeout = timeout;
            return binding;
        }
        
        public static Controller getController()
        {
            if(control == null)
            {
                control = new Controller();
            }
            return control;
        }

		public Store getStore()
		{
			return store;
		}
    }
}