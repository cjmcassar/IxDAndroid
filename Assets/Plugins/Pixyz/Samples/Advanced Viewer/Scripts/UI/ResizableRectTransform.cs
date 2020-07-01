using UnityEngine;

namespace Pixyz.Samples
{
    public enum Sides2D {
        In,
        Out,
        Move,
        Left,
        Right,
        Top,
        Bottom,
        BottomRight,
        BottomLeft,
        TopRight,
        TopLeft,
    }

    [RequireComponent(typeof(RectTransform))]
    public class ResizableRectTransform : MonoBehaviour {

        [HideInInspector]
        public Texture2D cursorResizeH;

        [HideInInspector]
        public Texture2D cursorResizeV;

        [HideInInspector]
        public Texture2D cursorResizeCR;

        [HideInInspector]
        public Texture2D cursorResizeCL;

        [HideInInspector]
        public Texture2D cursorMove;

        private const int MARGIN = 10;

        private Sides2D currentDrag;
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
                    // Resize
                    Vector2 deltaPositon = currentPosition - lastPosition;
                    switch (currentDrag) {
                        case Sides2D.Left:
                            rectTransform.offsetMin += deltaPositon * Vector2.right;
                            break;
                        case Sides2D.Right:
                            rectTransform.offsetMax += deltaPositon * Vector2.right;
                            break;
                        case Sides2D.Top:
                            rectTransform.offsetMax += deltaPositon * Vector2.up;
                            break;
                        case Sides2D.Bottom:
                            rectTransform.offsetMin += deltaPositon * Vector2.up;
                            break;
                        case Sides2D.BottomLeft:
                            rectTransform.offsetMin += deltaPositon * Vector2.one;
                            break;
                        case Sides2D.BottomRight:
                            rectTransform.offsetMin += deltaPositon * Vector2.up;
                            rectTransform.offsetMax += deltaPositon * Vector2.right;
                            break;
                        case Sides2D.TopLeft:
                            rectTransform.offsetMax += deltaPositon * Vector2.up;
                            rectTransform.offsetMin += deltaPositon * Vector2.right;
                            break;
                        case Sides2D.TopRight:
                            rectTransform.offsetMax += deltaPositon * Vector2.one;
                            break;
                    }
                    lastPosition = currentPosition;
                }
            } else {
                // Is currently not dragging
                // Check if mouse is over rect transform
                if (!rectTransform.IsMouseDirectlyOver()) {
                    if (wasMouseOver) {
                        Extensions.SetCursor(null);
                        wasMouseOver = false;
                    }
                    return;
                }

                wasMouseOver = true;

                // Check for possible drag with mouse over
                Sides2D suggestedDrag = rectTransform.GetSideUnderMouse(MARGIN);

                switch (suggestedDrag) {
                    case Sides2D.BottomRight:
                    case Sides2D.TopLeft:
                        Extensions.SetCursor(cursorResizeCL);
                        break;
                    case Sides2D.Right:
                    case Sides2D.Left:
                        Extensions.SetCursor(cursorResizeH);
                        break;
                    case Sides2D.BottomLeft:
                    case Sides2D.TopRight:
                        Extensions.SetCursor(cursorResizeCR);
                        break;
                    case Sides2D.Bottom:
                    case Sides2D.Top:
                        Extensions.SetCursor(cursorResizeV);
                        break;
                    default:
                        Extensions.SetCursor(null);
                        break;
                }

                if (Input.GetMouseButtonDown(0)) {
                    // Mouse is clicked
                    // Dragging starts
                    lastPosition = currentPosition;
                    currentDrag = suggestedDrag;
                    dragging = true;
                }
            }
        }
    }
}