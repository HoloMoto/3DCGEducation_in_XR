using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTraditionManager : MonoBehaviour
{
  public void SceneTradition(int scene)
  {
    //sceneのシーンへシーン遷移する
    UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    
  }
}
