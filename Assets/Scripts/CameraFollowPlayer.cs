using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    private void OnEnable()
    {
        RedoneReformPlayer.OnSelect += SelectSlimePart;
        RedoneBlowUp.OnSelect += SelectSlimePart;
    }
    private void OnDisable()
    {
        RedoneReformPlayer.OnSelect -= SelectSlimePart;
        RedoneBlowUp.OnSelect -= SelectSlimePart;
    }
    private void SelectSlimePart(Transform t)
    {
        Transform parentT = t;
        virtualCamera.Follow = parentT;
    }
}
