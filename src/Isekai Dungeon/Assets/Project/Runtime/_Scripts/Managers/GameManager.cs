using System;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Nice, easy to understand enum-based game manager. For larger and more complex games, look into
/// state machines. But this will serve just fine for most games.
/// </summary>
public class GameManager : StaticInstance<GameManager> {
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; set; }

    public Flock PlayerTeam, EnemyTeam;

    // Kick the game off with the first state
    void Start() => ChangeState(GameState.PlayerSetup);

    public void ChangeState(GameState newState) {
        OnBeforeStateChanged?.Invoke(newState);

        State = newState;
        switch (newState) {
            case GameState.PlayerSetup:
                PlayerSetup();
                break;
            case GameState.GameStart:
                GameUpdate();
                break;
            case GameState.Paused:
                Paused();
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);
        
        Debug.Log($"New state: {newState}");
    }

    public void PlayerSetup() {
        // Do some start setup, could be environment, cinematics etc

        // Eventually call ChangeState again with your next state
        
        //ChangeState(GameState.GameStart);
    }

    public void GameStart()
    {
        ChangeState(GameState.GameStart); 
    }

    public void GameUpdate()
    {

    }

    public void GameLose()
    {
        
    }

    public void Paused()
    {
        if (Input.GetKey(KeyCode.P))
        {
            ChangeState(GameState.GameStart);
        }
    }
}

/// <summary>
/// This is obviously an example and I have no idea what kind of game you're making.
/// You can use a similar manager for controlling your menu states or dynamic-cinematics, etc
/// </summary>
[Serializable]
public enum GameState {
    PlayerSetup = 0,
    GameStart = 1,
    Paused = 2,
    Win = 3,
    Lose = 4,
}