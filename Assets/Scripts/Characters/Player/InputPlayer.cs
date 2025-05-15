using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class InputPlayer : MonoBehaviour
{
    [SerializeField] InputActionAsset control;
    [SerializeField] Player player;
    [SerializeField] int IDScene;

    InputAction moveX;
    InputAction moveY;
    InputAction jump;
    InputAction interactiveItem;

    InputAction dropItem;
    InputAction activateItem;
    InputAction scrollItem;

    InputAction restart;
    InputAction timeScaleActivate;

    private string dead = "Dead";

    private void Start()
    {
        InputActionMap map = control.FindActionMap("PlayerMovement");
        moveX = map.FindAction("MoveX");
        moveY = map.FindAction("MoveY");
        jump = map.FindAction("Jump");
        interactiveItem = map.FindAction("InteractiveItem");

        map = control.FindActionMap("Inventory");
        activateItem = map.FindAction("ActivateItem");
        scrollItem = map.FindAction("Scroll");
        dropItem = map.FindAction("DropItem");

        map = control.FindActionMap("Debug");
        restart = map.FindAction("Restart");
        timeScaleActivate = map.FindAction("TimeScale");

        restart.Enable();
        timeScaleActivate.Enable();

        InputSetActivity(true);

    }


    public void InputSetActivity(bool enable)
    {
        player.enabled = enable;
        if (enable)
        {
            moveX.Enable();
            moveY.Enable();
            jump.Enable();
            interactiveItem.Enable();
            activateItem.Enable();
            scrollItem.Enable();
            dropItem.Enable();
        }
        else
        {
            moveX.Disable();
            moveY.Disable();
            jump.Disable();
            interactiveItem.Disable();
            activateItem.Disable();
            scrollItem.Disable();
            dropItem.Disable();
        }

    }


    private void Update()
    {
        if (restart.WasPressedThisFrame())
            SceneManager.LoadScene(IDScene);
        if (timeScaleActivate.WasPressedThisFrame())
        {
            if(Time.timeScale > 0)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }



        if(player.transform.tag == dead)
        {
            InputSetActivity(false);
            enabled = false;
        }
        else
        {
            Move();
            ScrollMouse();
            if (jump.WasPressedThisFrame())
                player.Jump();
            if (interactiveItem.WasPressedThisFrame())
                player.InteractiveItem();
            if (activateItem.WasPressedThisFrame())
                player.inventory.ActivateItem(player);
            if(dropItem.WasPressedThisFrame())
                player.inventory.DropSelected(player.transform.position + player.transform.forward * 2.5f);
        }

    }

    private void ScrollMouse()
    {
        int current = 0;
        float scroll = scrollItem.ReadValue<float>();
        if (scroll != 0)
        {
            if(scroll < 0)
                current = 1;
            else if (scroll > 0)
                current = -1;

            player.inventory.indexSelected += current;
        }

    }

    private void Move()
    {
        Vector2 current = new Vector2(moveX.ReadValue<float>(), moveY.ReadValue<float>()) * Time.deltaTime;
        player.Move(current);
    }

}
