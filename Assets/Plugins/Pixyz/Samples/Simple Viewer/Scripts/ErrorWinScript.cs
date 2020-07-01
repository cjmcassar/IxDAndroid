using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pixyz.Samples
{
    public class ErrorWinScript : MonoBehaviour
    {
        public Text errorString;

        public void popWithText(string text)
        {
            errorString.text = text;
            gameObject.SetActive(true);
        }
    }
}