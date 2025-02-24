using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.ApplicationServices.Messaging.Responses.Platforms
{
    /// <summary>
    /// Get platform repsonse object.
    /// </summary>
    public class GetPlatformResponse: ServiceResponseBase
    {

        /// <summary>
        ///  Gets or sets the platforms list.
        /// </summary>
        public List<PlatformModel>? Platforms { get; set; }
    }
}
