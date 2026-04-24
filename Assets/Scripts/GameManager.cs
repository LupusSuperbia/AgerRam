using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public interface IGameState { 
    void Enter();
    void Update();
    void FixedUpdate();
    void Exit();
    void DrawUI();
    // void State_Playing();
    // void State_Menu();
    // void State_Paused();
    // void State_GameOver();
    // // void State_Shop();
    // // void State_MapSelection();
    

}

public class GameManager : MonoBehaviour 
{   

    public static GameManager Instance { get; private set;}
    [Header("Estado del juego")]
    public int score ; 
    public float money; 
    public bool gameStarted = false;
    public bool gameOver = false; 
    public IGameState _currentState;

    // [Ser]
    
    private void Awake() { 
        if(Instance != null && Instance != this) { 
            Destroy(this);
        }
        else { 
            Instance = this;
            _currentState =(new PlayingState());
            Debug.Log(_currentState);
        }
    }

    public void Update()
    {   
        if(_currentState != null) { 
            _currentState.Update();
        }
    }

    public void ChangeState(IGameState newState) {
        if(newState != null){
            _currentState = newState;
        }
    }
    public void StartGame() => gameStarted = true;
    public void EndGame() => gameStarted = true;
    // public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    public void OnGUI(){ 
        if(_currentState != null){  
            _currentState.DrawUI();

        }
    }
}