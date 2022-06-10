using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static int mine { get; set; }

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
