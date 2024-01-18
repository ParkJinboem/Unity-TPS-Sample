using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvasPlayerInputs : MonoBehaviour
{

    [Header("Output")]
    public TPSPlayerInput tpsPlayerInput;

    public void VirtualMoveInput(Vector2 virtualMoveDirection)
    {
        tpsPlayerInput.BtnMove(virtualMoveDirection);
    }
}
