using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    #region Player Stack Variables
    //----------------------
    public int PlayerStack { get; set; }
    public int PlayerStackLimit { get; set; } = 4;
    //----------------------
    #endregion

    public float generatingSpeed = 0.5f;
    public float collectingSpeed = 0.2f;
    public float puttingSpeed = 0.2f;


    private void Awake()
    {
        Singleton();
    }

    private void Singleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else
            Destroy(this.gameObject);        
    }
}
