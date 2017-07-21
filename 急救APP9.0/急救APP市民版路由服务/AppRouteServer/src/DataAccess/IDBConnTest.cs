using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZIT.AppRouteServer.Model;

namespace ZIT.AppRouteServer.DataAccess
{
    public interface IDBConnTest
    {
        bool DBIsConnected();
    }
}
