using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager Singleton { get; internal set; }

    private void Awake() 
    {
        Singleton = this;
    }

    public void StartGame() 
    {

    }
}
