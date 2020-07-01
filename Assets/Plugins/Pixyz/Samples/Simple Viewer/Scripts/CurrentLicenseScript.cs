using UnityEngine;
using UnityEngine.UI;
using Pixyz.Config;
using Pixyz.Interface;

namespace Pixyz.Samples
{
    public class CurrentLicenseScript : MonoBehaviour
    {
        public Text infoText;
        public Text detailText;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (infoText != null) {
                if (Configuration.CheckLicense()) {
                    detailText.text = "";
                    infoText.text = "";
                    infoText.color = Color.black;
                    infoText.fontSize = 16;
                    infoText.alignment = TextAnchor.MiddleLeft;
                    string[] names;
                    string[] values;
                    if (Configuration.IsFloatingLicense()) {
                        Configuration.RetrieveFloatingLicensingSetup();
                        names = new string[]{
                        "License",
                        "",
                        "Server address",
                        "Port"
                    };
                        values = new string[] {
                        "Floating",
                        "",
                        Configuration.CurrentLicenseServer.serverAddress,
                        Configuration.CurrentLicenseServer.serverPort.ToString()
                    };
                    } else {
                        names = new string[]{
                        "Start date",
                        "End date",
                        "Company name",
                        "Name",
                        "E-mail"
                    };
                        Configuration.RetrieveCurrentLicense();
                        values = new string[] {
                        Configuration.CurrentLicense.startDate.ToUnityObject().ToString("yyyy-MM-dd"),
                        Configuration.CurrentLicense.endDate.ToUnityObject().ToString("yyyy-MM-dd"),
                        Configuration.CurrentLicense.customerCompany,
                        Configuration.CurrentLicense.customerName,
                        Configuration.CurrentLicense.customerEmail,
                    };
                    }

                    for (int i = 0; i < names.Length; ++i) {
                        infoText.text += names[i] + (names[i].Length > 0 ? ":\n" : "\n");
                        detailText.text += values[i] + "\n";
                    }
                } else {
                    infoText.text = "Your license is inexistant or invalid.";
                    infoText.color = Color.red;
                    infoText.fontSize = 18;
                    infoText.alignment = TextAnchor.MiddleCenter;
                }
            }
        }
    }
}