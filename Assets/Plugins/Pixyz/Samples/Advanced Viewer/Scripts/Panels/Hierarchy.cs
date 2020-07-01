using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Pixyz.Samples
{
    public class Hierarchy : MonoBehaviour {

        public RectTransform rectTransform;
        public Text text;

        private int lines;
        private HashSet<int> selected = new HashSet<int>();

        private Dictionary<Transform, int> transformToY = new Dictionary<Transform, int>();
        private Dictionary<int, Transform> yToTransform = new Dictionary<int, Transform>();

        void Awake() {

            Selection.SelectionChanged += Selection_SelectionChanged;

            text = GetComponent<Text>();
            rectTransform = GetComponent<RectTransform>();
            text.supportRichText = true;

            refresh();
        }

        private void Selection_SelectionChanged() {
            
        }

        private void Update() {
            if (Input.GetMouseButtonDown(0) && rectTransform.IsMouseDirectlyOver()) {
                int s = Mathf.RoundToInt(text.rectTransform.GetMousePosRelative(true).y / (1f * text.rectTransform.GetWidthHeight().y  / lines));

                refresh();
            }
        }

        public void refresh() {

            lines = 1;
            transformToY.Clear();
            yToTransform.Clear();
            StringBuilder strbldr = new StringBuilder();
            addNode(Setup.Instance.root.transform, strbldr, 0);
            text.text = strbldr.ToString();
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, lines * text.fontSize * text.lineSpacing);
        }

        private void addNode(Transform transform, StringBuilder strbldr, int level) {
            foreach (Transform child in transform.transform) {
                strbldr.Append(new string('\t', level) + child.name + '\n');
                transformToY.Add(child, lines);
                yToTransform.Add(lines, child);
                lines++;
                addNode(child, strbldr, level + 1);
            }
        }
    }
}