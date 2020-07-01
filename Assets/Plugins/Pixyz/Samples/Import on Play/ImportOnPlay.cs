using UnityEngine;
using Pixyz.Import;

namespace Pixyz.Samples
{
    public class ImportOnPlay : MonoBehaviour
    {
        public ImportSettings importSettings;
        public string filePath;

        private void Start()
        {
            import();
        }

        void import()
        {
            string fullPath = Application.dataPath + "/" + filePath;
            Importer importer = new Importer(fullPath, importSettings);
            importer.progressed += onProgressChanged;
            importer.completed += onImportEnded;
            importer.run();
        }

        void onImportEnded(GameObject gameObject)
        {
            Debug.Log("Model Imported");
        }

        void onProgressChanged(float progress, string message)
        {
            Debug.Log("Progress : " + 100f * progress + "%");
        }
    }
}