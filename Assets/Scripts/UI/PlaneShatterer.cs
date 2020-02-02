using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneShatterer : MonoBehaviour
{
    [SerializeField]
    private float transitionTime = 3f;
    [SerializeField]
    private Animator animator;

    public void ShatterScreen()
    {
        StartCoroutine(ShatterAnim());
    }
    public void RepairScreen()
    {
        StartCoroutine(RepairAnim());
    }
    public void Transition()
    {
        StartCoroutine(RepairAndShatter());
    }
    IEnumerator ShatterAnim()
    {
        animator.SetTrigger("Shatter");
        yield return new WaitForSeconds(transitionTime);
    }
    IEnumerator RepairAnim()
    {
        animator.SetTrigger("Repair");
        yield return new WaitForSeconds(transitionTime);
    }
    IEnumerator RepairAndShatter()
    {
        animator.SetTrigger("Repair");
        yield return new WaitForSeconds(transitionTime);
        animator.SetTrigger("Shatter");
        yield return new WaitForSeconds(transitionTime);
    }
}
