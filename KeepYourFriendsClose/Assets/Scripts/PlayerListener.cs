using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListener : MonoBehaviour
{
    private PlayerMovement PM;
    public PlayerRender PR;

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
        if (!PR.getFacingRight() && PM.getHorizontalInput() > 0 || PR.getFacingRight() && PM.getHorizontalInput() < 0)      //HORIZONTALLY
        {
            if (_playerFlippedHoriz != null)
                _playerFlippedHoriz();
        }
        if (!PR.getFacingDown() && PM.getVerticalInput() < 0 || PR.getFacingDown() && PM.getVerticalInput() > 0)         //VERTICALLY
        {
            if (_playerFlippedVert != null)
                _playerFlippedVert();
        }
    }
}
