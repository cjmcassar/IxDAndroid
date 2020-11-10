using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAndDisableObject : MonoBehaviour
{
    public GameObject ChatWindow;

    public void Enable()
    {
        ChatWindow.SetActive(true);
    }

    public void Disable()
    {
        ChatWindow.SetActive(false);
    }

}
