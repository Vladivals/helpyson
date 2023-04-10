using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskProgressView : MonoBehaviour
{
    [SerializeField] private Image filledImage;

    [SerializeField] private TaskProgressSO taskProgress;

    [SerializeField] private float timeout = 2400f;

    [SerializeField] private float doneTime = 7200f;

    public Image homeImage;
    public Image classroomImage;
    public Image galleryImage;

    public BoyStrokeScript taskDone;

    private const float UPDATE_DELAY = 0.5f;
    private float lastUpdateTime = Mathf.NegativeInfinity;

    void Start()
    {
        bool firstTaskDone = taskDone.taskDone;

    }

    void Update()
    {
        //    if (Time.time < lastUpdateTime + UPDATE_DELAY)
        //        return;

        //    lastUpdateTime = Time.time;

        //    filledImage.fillAmount = taskProgress.task1Progress / 4f;
        FillAmountControllerTask();

    }

    public void FillAmountControllerTask()
    {
        float fillAmount = taskProgress.task1Progress - (Time.time - taskProgress.task1doneTime) / taskProgress.timeout;

        homeImage.fillAmount = fillAmount;
        classroomImage.fillAmount = fillAmount;
        galleryImage.fillAmount = fillAmount;

        if (Time.time > taskProgress.task1doneTime + taskProgress.timeout && taskProgress.task1Progress > 0)
        {
            taskProgress.task1Progress--;
            taskProgress.task1doneTime = Time.time;
        }

        if (Time.time > taskProgress.task2doneTime + taskProgress.timeout)
        {
            taskProgress.task2Progress--;
            taskProgress.task2doneTime = Time.time;
        }
       
        if (Time.time > taskProgress.task3doneTime + taskProgress.timeout)
        {
            taskProgress.task3Progress--;
            taskProgress.task3doneTime = Time.time;
        }
    }

    void TaskDone()
    {
        if (taskDone.taskDone == true)
        {
            taskProgress.task1Progress++;
            Debug.Log("task1done");
            ResetProgress(3);
        }
    }

    public int ResetProgress(int progressCount)
    {
        if (taskProgress.task1Progress == progressCount)
        {
            taskProgress.task1Progress = 0;
            return taskProgress.task1Progress;
        }
        return taskProgress.task1Progress;
    }
}
