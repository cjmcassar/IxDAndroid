using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Pixyz.Import;

namespace Pixyz.Samples
{
    public class Properties : MonoBehaviour {

        public RectTransform rectTransform;
        public Text text;

        void Awake() {
            Selection.SelectionChanged += Selection_SelectionChanged;

            text = GetComponent<Text>();
            rectTransform = GetComponent<RectTransform>();
            text.supportRichText = true;
        }

        private void Selection_SelectionChanged() {
            refresh();
        }

        public void refresh() {

            StringBuilder strbldr = new StringBuilder();
                
            if (Selection.Selected) {
                Metadata metadata = Selection.Selected.GetComponent<Metadata>();
                if (metadata) {
                    foreach (KeyValuePair<string, string> pair in metadata.getProperties()) {
                        strbldr.Append(pair.Key + " = " + pair.Value + "\n");
                    }
                }
            }

            text.text = strbldr.ToString();
            //rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, lines * text.fontSize * text.lineSpacing);
        }
    }
}