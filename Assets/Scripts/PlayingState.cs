using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayingState : IGameState { 

    public float score;

    public void Enter() {
        score = GameManager.Instance.score;
        Time.timeScale = 1f;
        GUI.Label(new Rect(5,5,200,25), "Empezo:" + ((int)score));        
    }
    public void Update() {         
        score += Time.deltaTime * 10f;
        // if(GameManager.Instance.gameOver) {
            
        // }
    }
    public void FixedUpdate(){ 
    }

    public void DrawUI() {
        // if(gameOver) {
        //     GUI.color = Color.red;
        //     GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200), "Game Over\nYour score is: " + ((int)score) + "\nPress 'Space' to restart");
        // }
        // else { 
        //     if(!gameStarted){ 
        //         GUI.color = Color.red;
        //         GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200), "Press 'Space' to start");
        //     }
        // }

        GUI.color = Color.green; 
        GUI.Label(new Rect(5,5,200,25), "Score:" + ((int)score));
    }
    public void Exit() { 
        return;
    }
}