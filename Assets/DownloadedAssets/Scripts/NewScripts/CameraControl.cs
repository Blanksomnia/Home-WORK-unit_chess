using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour
{
    [SerializeField] float MinRotateVert = 40;
    [SerializeField] float MaxRotateVert = -30;
    [SerializeField] Transform Points;
    public Transform character;
    public Transform spine;
    private PlayerHealth health;

    public float sensitivity = 100.0f;
    public float smoothing = 2.0f;

    public bool CanUse = true;

    Vector2 mouseLook;
    Vector2 smoothV;

    bool CursorLocked = true;

    void Awake()
    {
        character = transform.parent;
        health = character.GetComponent<PlayerHealth>();
        SetCursorState();
    }
    private void SetCursorState()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            CursorLocked = CursorLocked ? false : true;
        }
        Cursor.lockState = CursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !CursorLocked;
    }

    void Update()
    {
        SetCursorState();

        if(CanUse)
        if(health.currentHealth > 0)
        {
            if (CursorLocked)
            {
                var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
                md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
                smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
                smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
                mouseLook += smoothV;
                mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

            }

            if (-mouseLook.y < MinRotateVert && -mouseLook.y > MaxRotateVert)
            {
                    if(spine != null)
                spine.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
            }
            character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
            Points.localRotation = Quaternion.AngleAxis(mouseLook.x + 180, Vector3.forward);
        }


    }
}
