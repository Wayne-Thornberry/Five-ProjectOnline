﻿
using System;

namespace Proline.Engine.Networking
{
    public class NetworkRequest
    {  
        public NetworkHeader Header { get; set; }
        public MethodCall Call { get; set; }
        public Exception Exception { get; set; }
        public int Ticks { get; set; }
    }
}
