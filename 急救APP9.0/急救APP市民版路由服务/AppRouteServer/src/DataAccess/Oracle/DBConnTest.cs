using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.OracleClient;
using ZIT.AppRouteServer.DataAccess;
using ZIT.AppRouteServer.Model;
using ZIT.AppRouteServer.Utility;

namespace ZIT.AppRouteServer.DataAccess.Oracle
{
    class DBConnTest : IDBConnTest
    {
        public bool DBIsConnected()
        {
            bool bIsConnected = DbHelperOra.IsConnected(SysParameters.DBConnectString);
            return bIsConnected;
        }
        
    }
}
