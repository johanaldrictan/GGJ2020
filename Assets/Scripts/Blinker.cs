using UnityEngine;

public class Blinker : MonoBehaviour
{
    private Animator animCont;

    public float blinkInterval;

    private void Start()
    {
        animCont = GetComponent<Animator>();
        InvokeRepeating("DoBlink", blinkInterval*1.5f, blinkInterval);
    }

    private void DoBlink()
    {
        float rand = Random.value;
        if (rand < 0.25)
        {
            animCont.SetTrigger("Blink");
        }
    }
}
