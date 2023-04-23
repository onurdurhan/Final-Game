using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    private AssetBundle myLoadedAssetBundle;
    private string[] scenePaths;

    // Use this for initialization
    void Start()
    {
       // myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/AssetBundles/scenes");
        //scenePaths = myLoadedAssetBundle.GetAllScenePaths();
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "Scene 1 "))
        {
            SceneManager.LoadScene("Assets/Scenes/SampleScene.unity", LoadSceneMode.Single);
        }

        if (GUI.Button(new Rect(100,10,100,100),"Scene 2" ))
        {
            SceneManager.LoadScene("Assets/Scenes/planetary.unity", LoadSceneMode.Single);
        }
    }
}
