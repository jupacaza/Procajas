using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Test.Procajas.Service
{
    public class TestBase
    {
        public static void AzureStorageEmulatorStart()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(Constants.AzureStorageEmulatorPath, @"start")
            {
                CreateNoWindow = true
            };

            Process.Start(startInfo);
        }

        public static void AzureStorageEmulatorClearAll()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(Constants.AzureStorageEmulatorPath, @"clear all")
            {
                CreateNoWindow = true
            };

            Process.Start(startInfo);
        }
    }
}
