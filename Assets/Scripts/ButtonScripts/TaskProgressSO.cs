using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/TaskProgress")]
public class TaskProgressSO : ScriptableObject
{
    public int task1Progress;
    public int task2Progress;
    public int task3Progress;

    public float task1doneTime;
    public float task2doneTime;
    public float task3doneTime;
    public float timeout = 2400f;

    public void Init()
    {
        task1Progress = 0;
        task2Progress = 0;
        task3Progress = 0;

        task1doneTime = 0f;
        task2doneTime = 0f;
        task3doneTime = 0f;
    }
}
