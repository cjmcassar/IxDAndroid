using UnityEngine;

namespace Pixyz.Samples
{
    public class MainCanvas : MonoBehaviour
    {
        private static MainCanvas _Instance;

        void Start()
        {
            _Instance = this;
        }

        public static MainCanvas Instance { get { return _Instance; } }
        public static GameObject GameObject { get { return _Instance.gameObject; } }
        public static Transform Transform { get { return _Instance.transform; } }
    }
}