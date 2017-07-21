using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZIT.AppRouteServer.Model;
using ZIT.LOG;

namespace ZIT.AppRouteServer.AppServerAPI
{
    public class AppServer
    {
        private APICallError ace;
        private APICarInfo aci;
        private APICarLocation acl;
        private APICarStep acs;
        private APIQualityInfo aqi;

        public AppServer()
        {
            ace = new APICallError();
            aci = new APICarInfo();
            acl = new APICarLocation();
            acs = new APICarStep();
            aqi = new APIQualityInfo();
        }
        public void SendCallError(HandleCallError hce)
        {
            ace.HandleCallError(hce);
        }

        public void SendCarInfo(SendCarInfo sci)
        {
            aci.HandleCarInfo(sci);
        }

        public void SendCarLocation(CarLocation cl)
        {
            acl.HandleCarLocation(cl);
        }

        public void SendCarStep(CarState cs)
        {
            acs.HandleCarStep(cs);
        }

        public void SendQualityInfo(ServiceQualityInfo sqi)
        {
            aqi.HandleQualityInfo(sqi);
        }

    }
}
