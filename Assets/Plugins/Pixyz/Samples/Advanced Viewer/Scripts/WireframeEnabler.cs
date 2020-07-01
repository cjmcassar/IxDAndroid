using System.Collections.Generic;
using UnityEngine;

namespace Pixyz.Samples
{
    public class WireframeEnabler : MonoBehaviour
    {
        public static WireframeEnabler Instance;

        public Shader shader;
        public bool wireframeEnabled = false;

        private void Awake()
        {
            Instance = this;
        }

        public void toggleWireframe()
        {
            wireframeEnabled = !wireframeEnabled;
            refresh();
        }

        public void refresh()
        {

            Stack<Transform> tsms = new Stack<Transform>();
            tsms.Push(Setup.Instance.root.transform);

            while (tsms.Count != 0) {

                Transform current = tsms.Pop();
                Renderer renderer = current.GetComponent<Renderer>();

                if (renderer) {
                    foreach (Material material in renderer.sharedMaterials) {
                        material.SetFloat("_WireframeThickness", wireframeEnabled ? 0.1f : 0f);
                    }
                }

                foreach (Transform child in current) {
                    tsms.Push(child);
                }
            }
        }
    }
}