using UnityEngine;

namespace Pixyz.Samples
{
    public delegate void VoidHandler();

    public static class Selection
    {
        public static event VoidHandler SelectionChanged;

        public static Transform _Selected;
        public static Transform Selected {
            get {
                return _Selected;
            }
            set {
                if (_Selected != value) {
                    _Selected = value;
                    SelectionChanged?.Invoke();
                }
            }
        }
    }
}