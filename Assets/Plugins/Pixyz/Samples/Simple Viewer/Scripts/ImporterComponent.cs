using UnityEngine;
using Pixyz.Import;
using System;

namespace Pixyz.Samples
{
    public class ImporterComponent : MonoBehaviour
    {

        public ImportSettings importSettings;
        public GameObject container;
        public Spinner spinner;

        public void importClick()
        {

            FileSelectionWindow fs = Instantiate(Resources.Load<GameObject>("FileSelectionWindow")).GetComponent<FileSelectionWindow>();
            fs.onValidated += import;

        }

        private void import(object sender, EventArgs args)
        {

            string path = (sender as FileSelectionWindow).inputField.text;

            if (!Formats.IsFileSupported(path))
                return;

            spinner.spin();

            Importer importer = new Importer(path, importSettings);
            importer.progressed += progressCallback;
            importer.completed += callback;
            importer.run();
        }

        private void callback(GameObject gameObject)
        {

            spinner.stop();

            Debug.Log("Model loaded.");
            if (container != null) {
                foreach (Transform child in container.transform) {
                    Destroy(child.gameObject);
                }
                gameObject.transform.parent = container.transform;
            }
        }

        private void progressCallback(float progress, string message)
        {
            //Debug.Log(message + " " + progress * 100);
            //if (spinner == null)
            //    return;
            //if (progress >= 0 && progress < 1f)
            //    spinner.spin();
            //else
            //    spinner.stop();
        }
    }
}