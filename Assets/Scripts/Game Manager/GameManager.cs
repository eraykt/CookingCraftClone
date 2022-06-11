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
