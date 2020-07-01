using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pixyz.Config;
using Pixyz.Interface;
using Pixyz.Plugin4Unity;

namespace Pixyz.Samples
{
    public class OnlineUI : MonoBehaviour
    {
        public RectTransform onlineCreds;
        public RectTransform onlineWin;

        public InputField username;
        public InputField password;

        public Text LicenseTitles;
        public Text LicenseDetails;

        public Dropdown licenseList;
        public Button installBtn;

        public ErrorWinScript errorWindow;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            onlineCreds.gameObject.SetActive(!Configuration.IsConnectedToWebServer());
            onlineWin.gameObject.SetActive(Configuration.IsConnectedToWebServer());
        }

        public void connect()
        {
            if (username.text.Length != 0 && password.text.Length != 0)
                refresh();
        }

        public void refresh()
        {
            try {
                Configuration.ConnectToWebServer(username.text, password.text);
            } catch (Exception e) {
                errorWindow.popWithText(e.Message);
                return;
            }
            populateLicenseList();
        }

        private void populateLicenseList()
        {
            int index = licenseList.value;
            licenseList.ClearOptions();
            List<string> options = new List<string>();

            foreach (var license in NativeInterface.RetrieveAvailableLicenses(username.text, password.text).list) {
                string option = license.product + "  [" + license.validity.ToEndDateRichText() + "]";
                if (license.current)
                    option += "  (current)";
                else if (license.onMachine)
                    option += "  (installed)";
                options.Add(option);
            }
            licenseList.AddOptions(options);
            switchLicense(index);
        }

        public void switchLicense(int index)
        {
            var info = Configuration.Licenses[index];
            var d = Configuration.Licenses[index].validity;
            int daysRemaining = Math.Max(0, (new DateTime(d.year, d.month, d.day) - DateTime.Now).Days + 1);
            string remainingTextColor = daysRemaining > 185 ? "green" : daysRemaining > 92 ? "orange" : "red";
            bool installed = info.onMachine;
            string productName = info.product;
            string validity = info.validity.ToString()
                + "   (<color='" + remainingTextColor + "'><b>" + daysRemaining + "</b> Day" + (daysRemaining > 1 ? "s" : "") + " remaining</color>)";
            string licenseUse = "" + (int)info.inUse + " / " + (int)info.count;
            string currentlyInstalled = installed ? "<color='green'>true</color>" : "false";

            LicenseTitles.text = "Product name: \n";
            LicenseTitles.text += "Validity: \n";
            LicenseTitles.text += "License use: \n";
            LicenseTitles.text += "Currently installed: \n";

            LicenseDetails.text = productName + "\n";
            LicenseDetails.text += validity + "\n";
            LicenseDetails.text += licenseUse + "\n";
            LicenseDetails.text += currentlyInstalled + "\n";

            installBtn.gameObject.SetActive(!installed);
        }

        public void installLicense()
        {
            try {
                Configuration.RequestWebLicense(Configuration.Licenses[licenseList.value]);
            } catch (Exception e) {
                errorWindow.popWithText("An error occured while installing the license: \n" + e.Message);
            }
        }

        public void releaseLicense()
        {
            try {
                Configuration.ReleaseWebLicense(Configuration.Licenses[licenseList.value]);
            } catch (Exception e) {
                errorWindow.popWithText("An error occured while installing the license: \n" + e.Message);
            }
        }
    }
}