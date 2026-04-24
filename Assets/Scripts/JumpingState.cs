using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class  JumpingState : PlayerState 
{       
    public event Action<float> onImpact;
    public new string name = "Jumped";
    public override void EnterState(SC_IRPlayer player){ 
        if(player == null) return;
        player.r.linearVelocity = new Vector3(player.r.linearVelocity.x, player.CalculateJumpVerticalSpeed(), player.r.linearVelocity.z);
        player.jumpTimer = player.jumpTime;

    }


    public override void UpdateState(SC_IRPlayer player){ 
        player.playerCam.mainCam.fieldOfView = Mathf.Lerp(player.playerCam.mainCam.fieldOfView, 85, Time.deltaTime * 7f);
        Debug.Log("Velocidad: " + player.r.linearVelocity.y);
        if(player.r.linearVelocity.y <= 0f) { 
            player.TransitionToState(player.playerFallingState);
        } else if(!player.isGrounded())  { 
            player.jumpTimer -= Time.deltaTime;
        }
    }

    public override void FixedUpdateState(SC_IRPlayer player) { 

    }

    public override void ExitState(SC_IRPlayer player) {
        player.jumpTimer = player.jumpTime;
        player.jumpTimeAction = true;
    } 
    public override void OnCollision(SC_IRPlayer player, Collision collision)
    {

    }

}