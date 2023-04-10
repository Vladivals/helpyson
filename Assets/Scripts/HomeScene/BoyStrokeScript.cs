using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoyStrokeScript : MonoBehaviour
{
    [SerializeField] GameObject boy;
    private bool canClick = true;
    public bool taskDone = false;

    public TaskProgressSO cooldownObject;

    // Start is called before the first frame update
    void Start()
    {
        taskDone = false;
    }


    private void OnMouseDown()
    {
        if (canClick == true)
        {
            canClick = false;
            StartCoroutine(ClickDelay());

            cooldownObject.task1Progress++;
            
            if(cooldownObject.task1Progress > 3) 
            {
                cooldownObject.task1Progress = 0;
                taskDone = true;
            }
           
        }

    }

    

    IEnumerator ClickDelay()
    {
        yield return new WaitForSeconds(5f);
        canClick = true;
    }
}
