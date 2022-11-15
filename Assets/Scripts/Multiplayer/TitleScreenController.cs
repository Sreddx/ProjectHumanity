using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fusion;

public class TitleScreenController : MonoBehaviour
{
    [SerializeField] private StartGameSettings _startGameSettings;
    public void SetGameModeAsHost()
    {
        _startGameSettings.GameMode = GameMode.Host;
        //Change scene to multiplayer
        SceneManager.LoadScene("Multiplayer");
    }
    
    public void SetGameModeAsClient()
    {
        _startGameSettings.GameMode = GameMode.Client;
        SceneManager.LoadScene("Multiplayer");
    }
}
