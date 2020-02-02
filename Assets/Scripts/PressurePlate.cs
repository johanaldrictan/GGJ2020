using UnityEngine;

//Goes on a pressure plate with a trigger collider.
public class PressurePlate : MonoBehaviour
{
    public Transform door;
    public float doorMax; //How high up the door can go
    public float doorMin; //The door's resting position, how low it is when not lifted
    public float doorSpeed; //How fast the door raises and lowers

    private bool pressed;
    private float baseY;

    private void Awake()
    {
        baseY = transform.position.y;
    }

    private void FixedUpdate()
    {
        if (pressed)
        {
            if (door.position.y < doorMax)
            {
                door.Translate(0, doorSpeed, 0);
            }
        }
        else if (door.position.y > doorMin)
        {
            door.Translate(0, -doorSpeed, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("Pressed plate.");
            pressed = true;
            transform.Translate(0,-0.05f*transform.localScale.y,0); //This assumes you are using the plate.fbx model
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("Un-pressed plate.");
            pressed = false;
            transform.Translate(0, baseY - transform.position.y, 0);
        }
    }

    private void OnDisable()
    {
        pressed = false;
        door.Translate(0, doorMin - door.position.y, 0);
        transform.Translate(0, baseY - transform.position.y, 0);
    }
}
