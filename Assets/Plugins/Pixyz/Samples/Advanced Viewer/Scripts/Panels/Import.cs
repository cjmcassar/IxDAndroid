using UnityEngine;
using UnityEngine.UI;
using Pixyz.Import;

namespace Pixyz.Samples
{
    public class Import : MonoBehaviour {

        public InputField filePath;
        public InputField scaling;
        public Toggle isZup;

        public void import() {
            ImportSettings importSettings = ScriptableObject.CreateInstance<ImportSettings>();
            importSettings.isZUp = isZup.isOn;
            importSettings.scaleFactor = float.Parse(scaling.text);
            importSettings.shader = WireframeEnabler.Instance.shader;
            Importer importer = new Importer(filePath.text, importSettings);
            importer.completed += importCompleted;
            importer.run();
        }

        private void importCompleted(GameObject gameObject) {

            // Clear previous model
            foreach (Transform child in Setup.Instance.root.transform) {
                GameObject.Destroy(child.gameObject);
            }

            // Put newly imported model under root
            gameObject.transform.SetParent(Setup.Instance.root.transform);

            // Refreshes wireframes
            WireframeEnabler.Instance.refresh();
        }
    }
}