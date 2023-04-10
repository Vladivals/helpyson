using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
  public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
    
}
