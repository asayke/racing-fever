using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


public class CameraSwitcher : MonoBehaviour
{
    public List<CinemachineVirtualCamera> Cams;
    private int _currCameraIndex;

    private void SwitchCamera()
    {
        _currCameraIndex = (_currCameraIndex + 1) % Cams.Count;
        var newCamera = Cams[_currCameraIndex];
        newCamera.Priority = 10;
        foreach (var cam in Cams)
        {
            if(cam!=newCamera)
                cam.Priority = 0;
        }
    }

    private void Update()
    {
        if (SimpleInput.GetKeyDown(KeyCode.Tab))
        {
            SwitchCamera();
        }
    }
}
