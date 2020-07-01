using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriLib;

public class LoadObject : MonoBehaviour
{
    // Start is called before the first frame update
    //protected void Start()
    //{
    //    using (var assetLoader = new AssetLoader())
    //    {
    //        try
    //        {
    //            var assetLoaderOptions = AssetLoaderOptions.CreateInstance();
    //            assetLoaderOptions.RotationAngles = new Vector3(90f, 180f, 0f);
    //            assetLoaderOptions.AutoPlayAnimations = true;
    //            assetLoaderOptions.UseOriginalPositionRotationAndScale = true;
    //            var loadedGameObject = assetLoader.LoadFromFile(string.Format("{0}/Bouncing.fbx", TriLibProjectUtils.FindPathRelativeToProject("Models", "t:Model Bouncing")), assetLoaderOptions);
    //            loadedGameObject.transform.position = new Vector3(128f, 0f, 0f);
    //        }
    //        //catch (Exception e)
    //        //{
    //        //    Debug.LogError(e.ToString());
    //        //}
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
