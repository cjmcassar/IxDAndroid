using UnityEngine;
using System.IO;
using System;
using Pixyz.Config;

namespace Pixyz.Samples
{
    public class LicenseManagerScript : MonoBehaviour
    {
        public void generateActivationCode(string activationFile)
        {
            FileSelectionWindow fs = Instantiate(Resources.Load<GameObject>("FileSelectionWindow")).GetComponent<FileSelectionWindow>();
            fs.inputField.text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\activation_code.bin";
            fs.onValidated += generateActivationCodeCallback;
        }

        private void generateActivationCodeCallback(object sender, EventArgs args)
        {
            string activationFile = (sender as FileSelectionWindow).inputField.text;
            if (string.IsNullOrEmpty(activationFile))
                throw new Exception();
            Configuration.GenerateActivationCode(activationFile);
        }

        public void installLicense(string licenseFile)
        {
            FileSelectionWindow fs = Instantiate(Resources.Load<GameObject>("FileSelectionWindow")).GetComponent<FileSelectionWindow>();
            fs.onValidated += installLicenseCallback;
        }

        private void installLicenseCallback(object sender, EventArgs args)
        {
            string licenseFile = (sender as FileSelectionWindow).inputField.text;
            if (string.IsNullOrEmpty(licenseFile) || !File.Exists(licenseFile))
                throw new FileNotFoundException();
            Configuration.InstallActivationCode(licenseFile);
        }

        public void generateReleaseCode(string releaseFile)
        {
            FileSelectionWindow fs = Instantiate(Resources.Load<GameObject>("FileSelectionWindow")).GetComponent<FileSelectionWindow>();
            fs.inputField.text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\release_code.bin";
            fs.onValidated += generateReleaseCodeCallback;
        }

        private void generateReleaseCodeCallback(object sender, EventArgs args)
        {
            string releaseFile = (sender as FileSelectionWindow).inputField.text;
            if (string.IsNullOrEmpty(releaseFile) || !File.Exists(releaseFile))
                throw new FileNotFoundException();
            Configuration.GenerateDeactivationCode(releaseFile);
        }
    }
}