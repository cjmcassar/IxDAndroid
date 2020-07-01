using UnityEngine;
using System.Collections;

namespace Pixyz.Samples
{
    public class MouseOrbitImproved : MonoBehaviour {

        public Material lineMaterial;

        public Transform target;
        public float distance = 5.0f;
        public float xSpeed = 120.0f;
        public float ySpeed = 120.0f;

        const float Y_MIN = -89.9f;
        const float Y_MAX = 89.9f;

        public float distanceMin = .5f;
        public float distanceMax = 15f;

        private float x = 0.0f;
        private float y = 0.0f;

        void Start() {
            Vector3 angles = transform.eulerAngles;
            x = angles.y;
            y = angles.x;

            fit();
        }

        public void fit() {
            Bounds? boundsn = target.GetTreeBounds();
            if (boundsn == null) {
                Debug.Log("cake");
                return;
            }

            Bounds bounds = (Bounds)boundsn;
            var tmp = 0.6f * Vector3.Scale(Vector3.Cross(Vector3.one, this.transform.forward), bounds.size);
            distance = 4f * Mathf.Max(Mathf.Abs(tmp.x), Mathf.Abs(tmp.y), Mathf.Abs(tmp.z));

            distanceMin = (bounds.max - bounds.min).magnitude / 2;
            distanceMax = 10 * distanceMin;
        }

        void OnPostRender() {
            if (lineMaterial && !isMouseOverUI && (Input.GetMouseButton(0) || Input.touches.Length > 0)) {
                GL.PushMatrix();
                GL.Begin(GL.LINES);
                lineMaterial?.SetPass(0);
                GL.Color(Color.red);
                GL.Vertex3(0f, 0f, 0f);
                GL.Vertex3(1f, 0f, 0f);
                GL.Color(Color.green);
                GL.Vertex3(0f, 0f, 0f);
                GL.Vertex3(0f, 1f, 0f);
                GL.Color(Color.blue);
                GL.Vertex3(0f, 0f, 0f);
                GL.Vertex3(0f, 0f, 1f);
                GL.End();
                GL.PopMatrix();
            }
        }

        bool isMouseOverUI = false;

        void LateUpdate() {

            if (target) {

                isMouseOverUI = Extensions.IsMouseDirectlyOverUI();

                float zoom = 0;

                if (!isMouseOverUI) {
                    zoom = Input.GetAxis("Mouse ScrollWheel");

                    if (Input.GetMouseButton(0) || Input.touches.Length > 0) {
                        x += Input.GetAxis("Mouse X") * xSpeed * 1f * 0.02f;
                        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
                    }
                }

                y = ClampAngle(y, Y_MIN, Y_MAX);

                Quaternion rotation = Quaternion.Euler(y, x, 0);

                distance = Mathf.Clamp(distance - zoom * 5, distanceMin, distanceMax);

                Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
                Vector3 position = rotation * negDistance + target.position;

                transform.rotation = rotation;
                transform.position = position;
            }
        }

        public static float ClampAngle(float angle, float min, float max) {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }
    }
}