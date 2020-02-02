using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneShatterer : MonoBehaviour
{
    [SerializeField]
    private float transitionTime = 1f;
    [SerializeField]
    private Animator animator;
    private void OnEnable()
    {
        InGameMenu.OnTransition += ShatterOrRepair;
    }
    private void OnDisable()
    {
        InGameMenu.OnTransition -= ShatterOrRepair;
    }
    private void ShatterOrRepair(bool transitionIn)
    {
        if(transitionIn)
        {
            RepairScreen();
        }
        else
        {
            ShatterScreen();
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

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
    public void PlayShatter()
    {
        SFXManager.instance.PlayWithBothMod("TransitionBreak");
    }
    public void PlayRepair()
    {
        SFXManager.instance.PlayWithBothMod("TransitionRepair");
    }
}
