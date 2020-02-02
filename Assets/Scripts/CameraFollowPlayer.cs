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
    }
    private void OnEnable()
    {
        ReformPlayer.OnSelect += SelectSlimePart;
    }
    private void OnDisable()
    {
        ReformPlayer.OnSelect += SelectSlimePart;
    }
    private void SelectSlimePart(Transform t)
    {
        Transform parentT = t;
        virtualCamera.Follow = parentT;
    }
}
