using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{

    public InputActionAsset InputActions;

    private InputAction m_moveAction;
    private InputAction m_lookAction;
    private InputAction m_interactAction;
    private InputAction m_toggleMenuAction;
    private InputAction m_quitGameAction;

    private Vector2 m_moveAmt;
    private Vector2 m_lookAmt;

    [SerializeField] private float walkSpeed = 5;
    [SerializeField] private float rotateSpeed = 5;

    private Rigidbody agent_rigidbody;
    [SerializeField] private Transform cameraPivot;
    private float xRotation = -2.5f;

    [SerializeField] internal GameObject canvas;



    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();

        m_toggleMenuAction.performed += ToggleMenu;
        m_quitGameAction.performed += QuitGame;
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();

        m_toggleMenuAction.performed -= ToggleMenu;
        m_quitGameAction.performed -= QuitGame;
    }

    private void Awake()
    {
        m_moveAction = InputSystem.actions.FindAction("Move");
        m_lookAction = InputSystem.actions.FindAction("Look");
        m_interactAction = InputSystem.actions.FindAction("Interact");

        m_toggleMenuAction = InputSystem.actions.FindAction("ToggleMenu");
        m_quitGameAction = InputSystem.actions.FindAction("QuitGame");

        agent_rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        m_moveAmt = m_moveAction.ReadValue<Vector2>();
        m_lookAmt = m_lookAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Walking();
        Rotating();
    }

    private void Walking()
    {
        Vector3 moveDirection = transform.forward * m_moveAmt.x + transform.right * -m_moveAmt.y;
        agent_rigidbody.MovePosition(agent_rigidbody.position + moveDirection.normalized * walkSpeed * Time.deltaTime);
    }

    private void Rotating()
    {
        //Y rotation of Player (right/left)
        float rotationAmountX = m_lookAmt.x * rotateSpeed * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0, rotationAmountX, 0);
        agent_rigidbody.MoveRotation(agent_rigidbody.rotation * deltaRotation);

        //X rotation of head / camera (up/down)
        float rotationAmountY = -m_lookAmt.y * rotateSpeed * Time.deltaTime;
        xRotation += rotationAmountY;
        xRotation = Mathf.Clamp(xRotation, -40f, 30f); //for not go up or down to far
        cameraPivot.localRotation = Quaternion.Euler(0f, 0f, xRotation);
    }

    private void ToggleMenu(InputAction.CallbackContext context)
    {
        bool isMenuVisible = canvas.gameObject.activeSelf;
        isMenuVisible = !isMenuVisible;
        canvas.gameObject.SetActive(isMenuVisible);
    }

    private void QuitGame(InputAction.CallbackContext context)
    {
        Application.Quit();
    }


}
