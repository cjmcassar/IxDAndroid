using Pixyz.Import;
using System.Collections.Generic;
using UnityEngine;

namespace Pixyz.Samples
{
    public class DisplayMetadataOnMouseOver : MonoBehaviour
    {
        public bool includeParents = true;

        private Transform objectUnderMouse;

        private GUIStyle _boldText;
        private GUIStyle boldText {
            get {
                if (_boldText == null) {
                    _boldText = new GUIStyle(GUI.skin.label);
                    _boldText.fontStyle = FontStyle.Bold;
                }
                return _boldText;
            }
        }

        const float COLUMN_NAMES_WIDTH = 120;
        const float COLUMN_VALUES_WIDTH = 200;

        void Update()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                objectUnderMouse = hit.transform;
            } else {
                objectUnderMouse = null;
            }
        }

        private void OnGUI()
        {
            Rect nrect = new Rect(5, 5, COLUMN_NAMES_WIDTH, 22);
            Rect vrect = new Rect(COLUMN_NAMES_WIDTH + 10, 5, COLUMN_VALUES_WIDTH, 22);

            Transform current = objectUnderMouse;
            while (current) {
                var properties = getProperties(current);

                if (properties != null) {

                    GUI.Label(new Rect(nrect.x, nrect.y, COLUMN_NAMES_WIDTH + 10 + COLUMN_VALUES_WIDTH, nrect.height), current.name, boldText);
                    nrect.y += 16;
                    vrect.y += 16;

                    foreach (KeyValuePair<string, string> property in properties) {
                        GUI.Label(nrect, property.Key);
                        GUI.Label(vrect, property.Value);
                        nrect.y += 16;
                        vrect.y += 16;
                    }

                    nrect.y += 10;
                    vrect.y += 10;
                }

                if (!includeParents)
                    break;

                current = current.parent;
            }
        }

        private Dictionary<string, string> getProperties(Transform transform)
        {

            Metadata metadata = transform.GetComponent<Metadata>();
            if (!metadata)
                return null;

            return metadata.getProperties();
        }
    }
}