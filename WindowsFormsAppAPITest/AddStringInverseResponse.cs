using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppAPITest
{
    [System.Runtime.Serialization.DataContract]
    public class AddStringInverseResponse
    {
        [System.Runtime.Serialization.DataMember()]
        public string result { get; set; }

        [System.Runtime.Serialization.DataMember()]
        public int resultid { get; set; }
    }
}
