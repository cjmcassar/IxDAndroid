using UnityEngine;
using UnityEngine.UI;
using Pixyz.Config;
using Pixyz.Plugin4Unity;

namespace Pixyz.Samples
{
    public class LicenseServerScript : MonoBehaviour
    {
        public InputField address;
        public InputField port;
        public Toggle flexLM;
        public ErrorWinScript errorWindow;

        private void OnEnable()
        {
            if (Configuration.CurrentLicenseServer == null)
                return;
            if (string.IsNullOrEmpty(address.text)) {
                address.text = Configuration.CurrentLicenseServer.serverAddress;
                flexLM.isOn = Configuration.CurrentLicenseServer.useFlexLM;
            }
            if (string.IsNullOrEmpty(port.text)) {
                port.text = Configuration.CurrentLicenseServer.serverPort.ToString();
                flexLM.isOn = Configuration.CurrentLicenseServer.useFlexLM;
            }
        }

        public void apply()
        {
            Configuration.ConfigureLicenseServer(address.text, ushort.Parse(port.text), flexLM.isOn);
        }
    }
}