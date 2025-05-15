using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = System.Random;
using System;

public class PuzzleBlocks : InteractiveItem
{
    [SerializeField] List<MeshRenderer> blocks = new List<MeshRenderer>();
    [SerializeField] Item puzzle;
    [SerializeField] ItemObject key;
    [SerializeField] Transform posToDropKey;
    List<Vector3> blocksRotateRight = new List<Vector3>();
    [SerializeField] InputPlayer _player;
    [SerializeField] Color colorSelected;
    [SerializeField] AudioClip puzzleAudio;
    [SerializeField] AudioClip donePuzzleAudio;
    AudioSource source;
    Random random = new Random();
    int blocksActivated = 0;
    int selected = 0;

    [SerializeField] InputActionAsset control;
    InputAction left;
    InputAction right;
    InputAction up;
    InputAction down;
    InputAction rotate;
    InputAction exit;

    private void Awake()
    {
        for (int i = 0; i < blocks.Count; i++)
        {
            Vector3 localRot = blocks[i].transform.localRotation.eulerAngles;
            blocksRotateRight.Add(new Vector3( Convert.ToInt32(localRot.x), Convert.ToInt32(localRot.y), Convert.ToInt32(localRot.z) ));
            blocks[i].transform.localRotation = Quaternion.AngleAxis(GetRotate(), Vector3.up);
            blocks[i].gameObject.SetActive(false);
        }

        string nameMap = "Puzzle";
        InputActionMap map = control.FindActionMap(nameMap);
        left = map.FindAction("Left");
        right = map.FindAction("Right");
        up = map.FindAction("Up");
        down = map.FindAction("Down");
        rotate = map.FindAction("Rotate");
        exit = map.FindAction("Exit");
        InputActivation(true);
    }

    private void Start()
    {
        key.CleanItem();
    }

    private void InputActivation(bool activation)
    {
        if (activation)
        {
            right.Enable();
            left.Enable();
            up.Enable();
            down.Enable();
            rotate.Enable();
            exit.Enable();
        }
        else
        {
            right.Disable();
            left.Disable();
            up.Disable();
            down.Disable();
            rotate.Disable();
            exit.Disable();
        }

    }

    private float GetRotate()
    {
        int rand = random.Next(0, 4);

        switch(rand)
        {
            case 0: return 0;
            case 1: return 90;
            case 2: return -90;
            case 3: return 180;
            default: return 0;
        }
    }

    private bool CanGetKey()
    {
        int blocksCount = 0;

        for (int i = 0;i < blocks.Count; i++)
        {
            Vector3 localRot = blocks[i].transform.localRotation.eulerAngles;
            Vector3 current = new Vector3(Convert.ToInt32(localRot.x), Convert.ToInt32(localRot.y), Convert.ToInt32(localRot.z));

            if (current == blocksRotateRight[i])
                blocksCount++;
        }

        if(blocksCount == blocks.Count)
            return true;
        else 
            return false;
    }

    public override void Activate(Character character)
    {
        if(key != null)
            if (blocksActivated < blocks.Count)
            {
                ItemObject item = character.inventory.GetSelectedItem();

                if(item != null)
                    if (puzzle._name == item.nameItem)
                    {
                        blocks[blocksActivated].gameObject.SetActive(true);
                        character.inventory.DropSelected(Vector3.zero);
                        item.CleanItem();
                        blocksActivated++;
                        source.clip = puzzleAudio;
                        source.Play();
                    }
            }
            else
            {
                _player.InputSetActivity(false);
                SelectBlock(0);
                InputActivation(true);
            }
    }

    public override void Updata(float deltaTime)
    {
        if (up.WasPressedThisFrame())
            SelectBlock(-3);
        if (down.WasPressedThisFrame())
            SelectBlock(3);
        if (left.WasPressedThisFrame())
            SelectBlock(-1);
        if (right.WasPressedThisFrame())
            SelectBlock(1);
        if (rotate.WasPressedThisFrame())
            Rotate();
        if (exit.WasPressedThisFrame())
        {
            blocks[selected].material.color = Color.white;
            selected = 0;
            _player.InputSetActivity(true);
            InputActivation(false);
        }
    }

    private void SelectBlock(int current)
    {
        current += selected;

        if(current >= 0 && current < blocks.Count)
        {
            blocks[selected].material.color = Color.white;
            blocks[current].material.color = colorSelected;
            selected = current;
        }

    }

    private void Rotate()
    {
        Vector3 localRotate = blocks[selected].transform.localRotation.eulerAngles;
        blocks[selected].transform.localRotation = Quaternion.AngleAxis(localRotate.y + 90, Vector3.up);

        if (CanGetKey())
        {
            blocks[selected].material.color = Color.white;
            selected = 0;
            key.DropItem(posToDropKey.position);
            key = null;
            source.clip = donePuzzleAudio;
            source.Play();
            _player.InputSetActivity(true);
            InputActivation(false);
        }
    }


}
