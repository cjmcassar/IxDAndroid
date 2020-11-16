using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ButtonFunctions : MonoBehaviour
{

    public GameObject ChatWindow;
    public GameObject PopUpWindow;

    public void EnableChat()
    {
        ChatWindow.SetActive(true);
    }

    public void DisableChat()
    {
        ChatWindow.SetActive(false);
    }

    public void DisablePopUp()
    {
        PopUpWindow.SetActive(false);
    }

    public void ShareDesign()
    {
        StartCoroutine(ShareLink());
    }

    private IEnumerator ShareLink()
    {

        yield return new WaitForEndOfFrame();

        //Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        //ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        //ss.Apply();

        //string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        //File.WriteAllBytes(filePath, ss.EncodeToPNG());

        ////To avoid memory leaks
        //Destroy(ss);

        new NativeShare()
            .SetSubject("I want to talk about this design with you! Download Obi to view it on your Smart Device")
            .SetText("I want to talk about this design with you! Download Obi to view it on your Smart Device: " + "iOS: https://testflight.apple.com/join/7NSzciUP"
            + "Android: https://install.appcenter.ms/orgs/obi-vision/apps/ixd-1/distribution_groups/the%20imagination%20factory")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();

        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
    }
}
