using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
public class FallingState : PlayerState {
    public new string name ="Falling"; 


    public override void EnterState(SC_IRPlayer player){
       
    }

    public override void UpdateState(SC_IRPlayer player){
        if(player.isFalling() && !player.isGrounded()) {
            Debug.Log("Sigue en el aire");
        } else if(player.isGrounded()) { 
            player.TransitionToState(player.playerWalkingState);
        }
    }

    public override void FixedUpdateState(SC_IRPlayer player){ 

    }

    public override void ExitState(SC_IRPlayer player){ 
        fell = true;
    }

    public override void OnCollision(SC_IRPlayer player, Collision collision) {
    }



}