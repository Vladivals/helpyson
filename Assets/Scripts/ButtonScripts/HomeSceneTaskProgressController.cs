using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSceneTaskProgressController : MonoBehaviour
{
    [SerializeField] private TaskProgressSO taskProgress;


    //BoyStrokeScript boyStrokeScript = GetComponent<BoyStrokeScript>();

    public void OnTaskDone()
    {
        /* if (BoyStrokeScript.taskDone == true)
         {
             taskProgress.task1Progress++;
             Debug.Log("task1done");
             ResetProgress(3);

         }*/

        /*if(TextRecognizeScript.taskDone == true)
        {
            taskProgress.task2Progress++;
        }*/

        /*if(PaintScript.taskDone == true)
       {
           taskProgress.task3Progress++;
       }*/
    }

    public int ResetProgress(int progressCount)
    {
        if(taskProgress.task1Progress == progressCount)
        {
            return taskProgress.task1Progress = 0;
        }
        return taskProgress.task1Progress;
    }
}
