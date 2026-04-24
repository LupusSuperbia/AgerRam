using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class  PlayerState { 
    // Todos los estados heredan el player
    protected SC_IRPlayer player; 
    public string name;
    public bool fell; 

    public void Initialize(SC_IRPlayer _player) { 
        player = _player;
    }

    public abstract void EnterState(SC_IRPlayer player);
    public abstract void UpdateState(SC_IRPlayer player);
    public abstract void FixedUpdateState(SC_IRPlayer player);
    public abstract void ExitState(SC_IRPlayer player);
    public abstract void OnCollision(SC_IRPlayer player, Collision collision);
}