using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        GameData.LoadFlags(); // Load before anything else
        DontDestroyOnLoad(gameObject); // Optional
    }

    void OnApplicationQuit()
    {
        GameData.SaveFlags();
    }
}
