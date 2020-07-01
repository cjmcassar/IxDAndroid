using UnityEngine;

namespace Pixyz.Samples
{
    public class Spinner : MonoBehaviour
    {
        float speed = 0;
        public float maxSpeed = 1;

        void FixedUpdate()
        {
            transform.Rotate(-Vector3.forward, speed);
        }

        public void spin()
        {
            speed = maxSpeed;
        }

        public void stop()
        {
            speed = 0;
            transform.localEulerAngles = new Vector3();
        }
    }
}