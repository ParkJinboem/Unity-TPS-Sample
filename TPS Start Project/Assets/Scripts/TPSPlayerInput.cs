using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System.Collections;
using UnityEngine.InputSystem;

public class TPSPlayerInput : MonoBehaviour
{
    public CinemachineInputProvider cinemachineInputProvider;
    public string fireButtonName = "Fire1";
    public string jumpButtonName = "Jump";
    public string moveHorizontalAxisName = "Horizontal";
    public string moveVerticalAxisName = "Vertical";
    public string reloadButtonName = "Reload";

    public Vector2 moveInput { get; private set; }
    public bool fire { get; set; }
    public bool reload { get; set; }
    public bool jump { get; private set; }

    [SerializeField] private InputActionReference screenPress;
    //[SerializeField] private LayerMask passLayerMask;
    private bool isScreenPressed;
    
    private void Awake()
    {
        screenPress.action.Enable();
    }
    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameover && !GameManager.Instance.isGamestart)
        {
            moveInput = Vector2.zero;
            fire = false;
            reload = false;
            jump = false;
            return;
        }

        
        //moveInput = new Vector2(Input.GetAxis(moveHorizontalAxisName), Input.GetAxis(moveVerticalAxisName));
        
        if (moveInput.sqrMagnitude > 1)
        {
            moveInput = moveInput.normalized;
        }

        if (isScreenPressed)
        {
            if (!screenPress.action.IsPressed())
            {
                isScreenPressed = false;
                cinemachineInputProvider.enabled = false;
            }
        }
        else
        {
            if (screenPress.action.IsPressed())
            {
                isScreenPressed = true;

                if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    cinemachineInputProvider.enabled = false;
                }
                else
                {
                    cinemachineInputProvider.enabled = true;
                }
            }
        }

        jump = Input.GetButtonDown(jumpButtonName);
        //fire = Input.GetButton(fireButtonName);
        //reload = Input.GetButtonDown(reloadButtonName);
    }

    public void BtnFire()
    {
        reload = false;
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        fire = true;
        yield return new WaitForSeconds(0.2f);
        fire = false;
    }
    public void BtnReload()
    {
        reload = true;
    }

    public void BtnMove(Vector2 moveDirection)
    {
        moveInput = moveDirection;
    }
}