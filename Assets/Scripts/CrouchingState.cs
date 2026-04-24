using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class  CrouchingState : PlayerState 
{  
    
    public new string name = "Crouching";

    public override void EnterState(SC_IRPlayer player){ 
        player.transform.localScale = Vector3.Lerp(player.transform.localScale, new Vector3(player.defaultScale.x, player.defaultScale.y * 0.4f, player.defaultScale.z), Time.fixedDeltaTime * 15); 
    }


    public override void UpdateState(SC_IRPlayer player){ 
        if(player.crouchTime > 0f) { 
            
        }
    }

    public override void FixedUpdateState(SC_IRPlayer player) { 

    }

    public override void ExitState(SC_IRPlayer player) {
            }
    public override void OnCollision(SC_IRPlayer player, Collision collision)
    {

    }

}