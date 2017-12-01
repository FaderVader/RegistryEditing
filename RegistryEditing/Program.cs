using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistryEditing
{
    class Program
    {
        static void Main(string[] args)
        {
            //string workGroupPath = @"SOFTWARE\JavaSoft\Prefs\avid\workgroups\avid technology incorporated\data\com\avid\workgroup\remoting\setting\groups";

            string workGroupPath = @"SOFTWARE\JavaSoft\Prefs\avid\workgroups\avid technology incorporated\data\com\avid\workgroup\remoting\setting";
            string WorkGroupGPSPath = @"SOFTWARE\JavaSoft\Prefs\avid\workgroups\avid technology incorporated\data\com\avid\workgroup\remoting\setting\grouplocators\isis3wg";

            string WorkGroupGps1 = @"isis3gps01.net.dr.dk:4160";
            string WorkGroupGps2 = @"isis3gps02.net.dr.dk:4160";

            string singleKeyName = "delete";

            RegistryKeyValue registryKeyValue = new RegistryKeyValue() { KeyName = "testKey", KeyValue = "testValue" };


            RegistryHandler.DeleteSingleKey(workGroupPath, singleKeyName); // OK , works as expected
            //RegistryHandler.DeleteAllSubKeys(WorkGroupGPSPath);

            //RegistryHandler.CreateSingleKey(workGroupPath);

        }
    }
}
