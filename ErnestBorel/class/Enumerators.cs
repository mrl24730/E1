using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErnestBorel
{
    //staus Enum
    public enum StatusType
    {
        noAuthorization= -1,
        error,              //0
        success            //1
    }

    public enum LocationType
    {
        network,
        distributor
    }

}