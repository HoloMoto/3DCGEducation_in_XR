using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTraditionManager : MonoBehaviour
{
  
  public void SceneTradition(int scene)
  {
      Debug.Log("SceneTradition in"+scene );
    //sceneのシーンへシーン遷移する
    UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    
  }

  public void OpenURLtoNotionWebPage(string url)
  {
    //URLを開く
    Application.OpenURL(url);
  }
}
