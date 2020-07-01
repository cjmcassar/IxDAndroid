
using Pixyz.Import;
using UnityEngine;

public class RuntimeObjectImporter : MonoBehaviour
{

    public void ImportModel()
    {
        string file = @"C:\Users\CJMCassar\Downloads\bugatti-chiron-6.snapshot.4\CHIRON2016.3dm";
        ImportSettings importSettings = ScriptableObject.CreateInstance<ImportSettings>();
        Importer importer = new Importer(file, importSettings);
        importer.progressed += progressed;
        importer.completed += completed;
        importer.run();
        Debug.Log("ImportModel class started");
    }

    private void progressed(float progress, string message)
    {
        Debug.Log("Progress : " + 100f * progress + "%");
    }

    private void completed(GameObject gameObject)
    {
        Debug.Log("Model Imported");
    }

    private void onFailed(ExceptionHandler exception)
    {
        Debug.Log("Failed ! Error : " + exception);
    }
}
