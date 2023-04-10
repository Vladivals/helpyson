using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeLogic : MonoBehaviour
{
    [SerializeField] private TaskProgressSO taskProgress;

    private static RuntimeLogic _instance;

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }

        _instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        taskProgress.Init();
    }
}