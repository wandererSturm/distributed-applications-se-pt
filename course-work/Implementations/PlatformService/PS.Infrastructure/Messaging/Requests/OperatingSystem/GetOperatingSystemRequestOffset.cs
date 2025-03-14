﻿using PS.ApplicationServices.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Infrastructure.Messaging.Requests.OperatingSystem
{
    public class GetOperatingSystemRequestOffset : ServiceRequestBase
    {
        public int Offset { get; set; }
        public int Count { get; set; }
        public GetOperatingSystemRequestOffset(int offset, int count)
        {
            Offset = offset;
            Count = count;
        }
    }
}
