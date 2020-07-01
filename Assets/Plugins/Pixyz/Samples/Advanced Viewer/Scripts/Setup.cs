using UnityEngine;

namespace Pixyz.Samples
{
    public class Setup : MonoBehaviour
    {
        public static Setup Instance;

        public GameObject root;

        void Awake()
        {
            Instance = this;
        }
    }
}