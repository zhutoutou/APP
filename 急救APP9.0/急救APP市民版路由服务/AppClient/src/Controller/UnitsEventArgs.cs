using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZIT.AppClient.Model;

namespace ZIT.AppClient.Controller
{
    /// <summary>
    /// Stores message to be used by an event.
    /// </summary>
    public class UnitsEventArgs : EventArgs
    {
        /// <summary>
        /// NetStatus object that is associated with this event.
        /// </summary>
        public List<OnlineUnit> Units { get; private set; }

        /// <summary>
        /// Creates a new StatusEventArgs object.
        /// </summary>
        /// <param name="status">NetStatus object that is associated with this event</param>
        public UnitsEventArgs(List<OnlineUnit> units)
        {
            Units = units;
        }
    }
}
