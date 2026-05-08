using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    public InputActionReference triggerButton;
    public GameObject bullet;
    public Transform firePos;
    public AudioClip shotSound;
    AudioSource _audio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        triggerButton.action.performed += OnTriggerPressed;

        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerPressed(InputAction.CallbackContext context){
        Debug.Log("Trigger pressed!!!");
        _audio.PlayOneShot(shotSound);
        Instantiate(bullet,firePos.position,firePos.rotation);
    }
}
