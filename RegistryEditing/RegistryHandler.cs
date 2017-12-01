using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistryEditing
{
    public static class RegistryHandler
    {
        public static RegistryKeyValue ReadKey(string targetPath)
        {
            RegistryKeyValue keyValuePair = new RegistryKeyValue();

            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            using (RegistryKey registryKey = hklm.OpenSubKey(targetPath))
            {
                string valueName = registryKey.GetValueNames().First();
                string value = registryKey.GetValue(valueName).ToString();
                keyValuePair.KeyName = valueName;
                keyValuePair.KeyValue = value;

                return keyValuePair;  //value;
            }
        }

        public static List<RegistryKeyValue> ReadKeyTree(string targetPath)
        {
            List<RegistryKeyValue> keyList = new List<RegistryKeyValue>();
            try
            {
                using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                using (RegistryKey registryKey = hklm.OpenSubKey(targetPath))  //NOTE: currently hardcoded to CurrentUser. Need LocalMachine!
                {
                    //Console.WriteLine("There are {0} subkeys under {1}.", registryKey.SubKeyCount.ToString(), registryKey.Name);

                    foreach (string subKeyName in registryKey.GetSubKeyNames())
                    {
                        using (RegistryKey tempKey = registryKey.OpenSubKey(subKeyName))
                        {
                            //Console.WriteLine("\nThere are {0} values for {1}.", tempKey.ValueCount.ToString(), tempKey.Name);
                            foreach (string valueName in tempKey.GetValueNames())
                            {
                                //Console.WriteLine("{0, -8}: {1}", valueName, tempKey.GetValue(valueName).ToString());
                                keyList.Add(new RegistryKeyValue { KeyName = valueName, KeyValue = tempKey.GetValue(valueName).ToString() });
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //log.Errror("Error while reading key: " + e.Message;
                return keyList;
            }
            return keyList;
        }

        public static void WriteSingleValue(string targetPath, RegistryKeyValue keyValuePair, RegistryValueKind regType) //RegValueKind regType
        {
            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            using (RegistryKey registryKey = hklm.OpenSubKey(targetPath, true))
            {
                registryKey.SetValue(keyValuePair.KeyName, keyValuePair.KeyValue, regType);
            }

        }

        public static void DeleteSingleKey(string targetPath, string keyName) // Will delete key and content
        {
            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            using (RegistryKey registryKey = hklm.OpenSubKey(targetPath, true))
            {
                registryKey.DeleteSubKey(keyName);
            }
        }

        public static void DeleteAllSubKeys(string targetPath)
        {
            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            using (RegistryKey registryKey = hklm.OpenSubKey(targetPath, true))
            {
                foreach (string subkey in registryKey.GetSubKeyNames())
                {
                    registryKey.DeleteSubKey(subkey);
                }
            }            
        }

        public static void CreateSingleKey(string targetPath) //, string keyName
        {
            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                RegistryKey newKey = hklm.CreateSubKey(targetPath);
            }
        }
    }
}
