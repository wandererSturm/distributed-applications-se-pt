using PS.Infrastructure.Messaging.Responses.OperatingSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.ApplicationServices.Messaging.Responses.OperatingSystem
{
    /// <summary>
    /// Get platform repsonse object.
    /// </summary>
    public class GetOperatingSystemResponse: ServiceResponseBase
    {

        /// <summary>
        ///  Gets or sets the platforms list.
        /// </summary>
        public List<OperatingSystemModel>? OperatingSystems { get; set; }
    }
}
