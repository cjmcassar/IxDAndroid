using UnityEngine;

namespace Pixyz.Samples
{
    [RequireComponent(typeof(RectTransform))]
    public class DraggableRectTransform : MonoBehaviour {

        public Texture2D cursorMove;
        public RectTransform targetRectTransform;

        private RectTransform rectTransform;
        private Vector2 lastPosition;
        private bool dragging = false;
        private bool wasMouseOver = false;

        private void Awake() {
            rectTransform = GetComponent<RectTransform>();
        }

        void Update() {

            Vector2 currentPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            if (dragging) {
                // Is currently dragging
                if (Input.GetMouseButtonUp(0)) {
                    // Disable dragging
                    dragging = false;
                    Extensions.SetCursor(null);
                } else {
                    // Move
                    Vector2 deltaPositon = currentPosition - lastPosition;
                    targetRectTransform.position += new Vector3(deltaPositon.x, deltaPositon.y, 0f);
                    lastPosition = currentPosition;
                }
            } else {
                // Is currently not dragging

                // Check if mouse is over rect transform
                if (!rectTransform.IsMouseDirectlyOver()) {
                    if (!wasMouseOver) {
                        Extensions.SetCursor(null);
                        wasMouseOver = true;
                    }
                    return;
                }

                wasMouseOver = false;
                Extensions.SetCursor(cursorMove);

                if (Input.GetMouseButtonDown(0)) {
                    // Mouse is clicked
                    // Dragging starts
                    lastPosition = currentPosition;
                    dragging = true;
                }
            }
        }
    }
}