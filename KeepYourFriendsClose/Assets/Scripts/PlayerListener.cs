using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListener : MonoBehaviour
{
    private PlayerMovement PM;
    private PlayerRender PR;
    private PlayerAim PA;

    public delegate void PlayerWalking();
    public static event PlayerWalking _playerWalking;
    public delegate void PlayerStopWalking();
    public static event PlayerStopWalking _playerStopWalking;

    public delegate void PlayerFlippedHoriz();
    public static event PlayerFlippedHoriz _playerFlippedHoriz;
    public delegate void PlayerFlippedVert();
    public static event PlayerFlippedVert _playerFlippedVert;

    private void Awake()
    {
        PM = GetComponent<PlayerMovement>();
        PA = GetComponentInChildren<PlayerAim>();
        PR = GetComponentInChildren<PlayerRender>();
    }

    private void Update()
    {
        //LISTEN FOR WALK
        if ((PM.getHorizontalInput() != 0 || PM.getVerticalInput() != 0))
        {
            if (_playerWalking != null)
                _playerWalking();
        }
        else
        {
            if (_playerStopWalking != null)
                _playerStopWalking();
        }

        //LISTEN FOR SPRITE FLIP
        if (!PR.getFacingRight() && (PA.GetQuadrant() == PlayerAim.AimQuadrant.TopRight || PA.GetQuadrant() == PlayerAim.AimQuadrant.BottomRight)  //HORIZONTALLY
            || PR.getFacingRight() && (PA.GetQuadrant() == PlayerAim.AimQuadrant.TopLeft || PA.GetQuadrant() == PlayerAim.AimQuadrant.BottomLeft))     
        {
            if (_playerFlippedHoriz != null)
                _playerFlippedHoriz();
        }
        if (!PR.getFacingDown() && (PA.GetQuadrant() == PlayerAim.AimQuadrant.BottomRight || PA.GetQuadrant() == PlayerAim.AimQuadrant.BottomLeft)  //VERTICALLY
            || PR.getFacingDown() && (PA.GetQuadrant() == PlayerAim.AimQuadrant.TopRight || PA.GetQuadrant() == PlayerAim.AimQuadrant.TopLeft))        
        {
            if (_playerFlippedVert != null)
                _playerFlippedVert();
        }
    }
}
