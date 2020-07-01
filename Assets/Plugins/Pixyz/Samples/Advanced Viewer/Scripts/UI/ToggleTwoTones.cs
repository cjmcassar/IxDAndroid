using UnityEngine;
using UnityEngine.UI;

namespace Pixyz.Samples
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleTwoTones : MonoBehaviour
    {
        private Toggle toggle;
        private Color onColor;
        private Color offColor;

        void Start()
        {
            ColorUtility.TryParseHtmlString("#00A4FF", out onColor);
            ColorUtility.TryParseHtmlString("#FFFFFF", out offColor);

            toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(OnToggleValueChanged);

            OnToggleValueChanged(toggle.isOn);
        }

        private void OnToggleValueChanged(bool isOn)
        {
            ColorBlock cb = toggle.colors;
            if (!isOn) {
                cb.normalColor = offColor;
                cb.highlightedColor = offColor;
                cb.selectedColor = offColor;
                cb.pressedColor = offColor;
            } else {
                cb.normalColor = onColor;
                cb.highlightedColor = onColor;
                cb.selectedColor = onColor;
                cb.pressedColor = onColor;
            }
            toggle.colors = cb;
        }
    }
}