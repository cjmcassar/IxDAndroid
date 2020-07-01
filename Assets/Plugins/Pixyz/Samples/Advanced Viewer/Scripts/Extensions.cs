using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Pixyz.Samples
{
    public static class Extensions
    {
        public static void SetCursor(Texture2D texture) {
            if (texture) {
                Cursor.SetCursor(texture, new Vector2(0.5f * texture.width, 0.5f * texture.height), CursorMode.Auto);
            } else {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
        }

        public static Sides2D GetSideUnderMouse(this RectTransform rectTransform, int margin) {

            Vector2 wh = rectTransform.GetWidthHeight();
            Vector2 mousePosRelative = GetMousePosRelative(rectTransform);

            if (mousePosRelative.y > wh.y
                || mousePosRelative.y < 0
                || mousePosRelative.x > wh.x
                || mousePosRelative.x < 0) {
                return Sides2D.Out;
            }

            if (mousePosRelative.x < margin) {
                if (mousePosRelative.y < margin) {
                    return Sides2D.BottomLeft;
                } else if (mousePosRelative.y > wh.y - margin) {
                    return Sides2D.TopLeft;
                } else {
                    return Sides2D.Left;
                }
            } else if (mousePosRelative.x > wh.x - margin) {
                if (mousePosRelative.y < margin) {
                    return Sides2D.BottomRight;
                } else if (mousePosRelative.y > wh.x - margin) {
                    return Sides2D.TopRight;
                } else {
                    return Sides2D.Right;
                }
            } else {
                if (mousePosRelative.y < margin) {
                    return Sides2D.Bottom;
                } else if (mousePosRelative.y > wh.y - margin) {
                    return Sides2D.Top;
                } else {
                    return Sides2D.In;
                }
            }
        }

        public static Vector2 GetMousePosRelative(this RectTransform rectTransform, bool invertY = false) {
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);
            if (invertY)
                return new Vector2(Input.mousePosition.x - corners[0].x, corners[2].y - Input.mousePosition.y);
            else
                return new Vector2(Input.mousePosition.x - corners[0].x, Input.mousePosition.y - corners[0].y);
        }

        public static Vector2 GetWidthHeight(this RectTransform rectTransform) {
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);
            return new Vector2(corners[2].x - corners[0].x, corners[2].y - corners[0].y);
        }

        public static bool IsMouseDirectlyOver(this RectTransform rectTransform) {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            if  (results.Count > 0 && results[0].gameObject == rectTransform.gameObject) {
                return true;
            }
            return false;
        }

        public static bool IsMouseDirectlyOverUI() {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            if (results.Count > 0) {
                return true;
            }
            return false;
        }

        public static float GetLineHeight(this UnityEngine.UI.Text text) {
            var extents = text.cachedTextGenerator.rectExtents.size * 0.5f;
            var lineHeight = text.cachedTextGeneratorForLayout.GetPreferredHeight("A", text.GetGenerationSettings(extents));

            return lineHeight;
        }

        public static Bounds? GetTreeBounds(this Transform transform) {

            bool first = true;
            Bounds bounds = new Bounds();

            Stack<Transform> tsms = new Stack<Transform>();
            tsms.Push(transform);

            Renderer renderer;

            while (tsms.Count != 0) {

                Transform current = tsms.Pop();
                renderer = current.GetComponent<Renderer>();

                if (renderer != null) {
                    if (first) {
                        first = false;
                        bounds = renderer.bounds;
                    } else {
                        bounds.Encapsulate(renderer.bounds);
                    }
                }

                foreach (Transform child in current) {
                    tsms.Push(child);
                }
            }

            if (first)
                return null;

            return bounds;
        }
    }
}