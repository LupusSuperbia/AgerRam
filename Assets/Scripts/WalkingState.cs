using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WalkingState : PlayerState
{
    public new string name = "Walking";
    public int fovStandarWalk = 100;
    public bool standEnterState = false;
    public float startPosY;
    public float startPosX;
    public float startPosZ;
    public Vector3 restPosCamera;

    public float timer;
    public override void EnterState(SC_IRPlayer player)
    {
        player.OnJumpedEvent += jumpAction;
        player.OnCrouchedEvent += crouchAction;
        if (standEnterState) standEnterState = false;
        else player.playerCam.mainCam.fieldOfView = 100;
        if (restPosCamera != player.weaponPlayer.transform.localPosition && startPosY != null)
        {
            restPosCamera = player.weaponPlayer.transform.localPosition;
            startPosY = restPosCamera.y;
            startPosX = restPosCamera.x;
            startPosZ = restPosCamera.z;
        }

    }

    void OnEnable()
    {

    }
    public override void UpdateState(SC_IRPlayer player)
    {

        if ((player.jumpTimeAction || player.isGrounded()) && player.playerCam.mainCam.fieldOfView != fovStandarWalk)
        {
            player.playerCam.mainCam.fieldOfView = Mathf.Lerp(player.playerCam.mainCam.fieldOfView, fovStandarWalk, player.acceleration * Time.deltaTime);
            player.jumpTimeAction = false;
        }
        timer += Time.deltaTime * player.bobSpeed;
        float newY = startPosY + Mathf.Sin(timer) * player.bobAmount;
        player.weaponPlayer.transform.localPosition = new Vector3(startPosX, newY, startPosZ);
    }

    public void jumpAction(SC_IRPlayer player)
    {
        if (player.isGrounded() || player._currentCoyoteTime > 0f)
        {
            player.r.linearVelocity = new Vector3(player.r.linearVelocity.x, player.CalculateJumpVerticalSpeed(), player.r.linearVelocity.z);
            player.jumpTimer = player.jumpTime;
            player.TransitionToState(player.playerJumpingState);
        }
    }
    public void crouchAction(SC_IRPlayer player)
    {

    }
    public override void FixedUpdateState(SC_IRPlayer player)
    {

    }

    public override void ExitState(SC_IRPlayer player)
    {
        // player.currentSpeed =  0;
        player.OnJumpedEvent -= jumpAction;
        restPosCamera = player.weaponPlayer.transform.localPosition;
        standEnterState = true;

    }
    public override void OnCollision(SC_IRPlayer player, Collision collision)
    {

    }

}
