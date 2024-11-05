using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFly : MonoBehaviour
{
    [SerializeField] private Vector2 _rotationXMinMax = new Vector2(-40, 40);
    public float sensitivity = 100.0f;
    public float smoothing = 2.0f;
    Vector2 mouseLook;
    Vector2 smoothV;
    GameObject character;
    public bool isFirstCam;
    private ParametresUnit _unit;

    void Start()
    {
        character = this.transform.parent.gameObject;
        _unit = character.GetComponent<ParametresUnit>();
    }

    void Update()
    {
        
        if(_unit._isDead == false)
        {
            var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
            smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
            mouseLook += smoothV;
            mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
            character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
        }


    }
}
