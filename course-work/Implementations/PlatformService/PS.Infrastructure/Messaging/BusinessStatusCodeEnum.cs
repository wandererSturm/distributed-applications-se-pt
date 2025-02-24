using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.ApplicationServices.Messaging
{
    public enum BusinessStatusCodeEnum
    {
        None,
        Success,
        MissingObject,
        InternalServerError,
        InvalidUserName,
        InvalidPassword,
    }
}
