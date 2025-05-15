using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] Material activatedButton;
    Material button;
    MeshRenderer _renderer;
    AudioSource _audioSource;

    int boxes = 0;

    private bool _activated = false;
    public bool activated => _activated;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        button = _renderer.material;
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(boxes <= 0)
        {
            _audioSource.Play();
            _activated = true;
            _renderer.material = activatedButton;
        }
        boxes++;
    }



    private void OnTriggerExit(Collider other)
    {
        boxes--;
        if(boxes <= 0)
        {
            _audioSource.Play();
            _activated = false;
            _renderer.material = button;
        }
    }

}
