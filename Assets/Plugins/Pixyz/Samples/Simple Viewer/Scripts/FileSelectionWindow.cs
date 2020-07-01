using System;
using UnityEngine;
using UnityEngine.UI;

namespace Pixyz.Samples
{
    public class FileSelectionWindow : MonoBehaviour
    {
        public string value { get { return inputField.text; } }
        public EventHandler onValidated;
        public EventHandler onCanceled;

        public InputField inputField;

        void Start()
        {
            transform.SetParent(MainCanvas.Transform, false);
            transform.localPosition = new Vector3(0, 0, 0);
        }

        public void validate()
        {
            if (onValidated != null)
                onValidated.Invoke(this, null);
            Destroy(this.gameObject);
        }

        public void cancel()
        {
            if (onCanceled != null)
                onCanceled.Invoke(this, null);
            Destroy(this.gameObject);
        }
    }
}