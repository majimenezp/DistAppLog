using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistALMessages
{
    [Serializable]
    public enum MessageTypes
    {
        Fatal=100, //Fatal error that compromises the app
        Error=50, // Error catched or compromises a module or process
        Warning=25, //Warning of failure 
        Info=10, //General information
        Debug=5, //Debug data for developer
        Hit=1 //Indicating that a module has hit or touched
    }
}
