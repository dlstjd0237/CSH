using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;


public enum CamerCategory
{
    RigCam = 0,
    CannonCam = 1,
    BallCam =2
}
public class CameraSet
{
    public CamerCategory Category;
    public CinemachineVirtualCamera VCam;
   
}
public class CameraManneger : MonoBehaviour
{
    public static CameraManneger Instance;

    private List<CameraSet> _camList = new List<CameraSet>();

    private CinemachineBrain _brainCam;
    private CinemachineBasicMultiChannelPerlin _bPerlin;
    private Tween _prevTween = null;
   
    public void Init()
    {
        var cannonCam = transform.Find("CannonCam").GetComponent<CinemachineVirtualCamera>();
        var ballCam = transform.Find("BallCam").GetComponent<CinemachineVirtualCamera>();
        var rigCam = transform.Find("RigCam").GetComponent<CinemachineVirtualCamera>();

        _bPerlin = ballCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        _camList.Add(new CameraSet { Category = CamerCategory.CannonCam, VCam = cannonCam });
        _camList.Add(new CameraSet { Category = CamerCategory.BallCam, VCam = ballCam });
        _camList.Add(new CameraSet { Category = CamerCategory.RigCam, VCam = rigCam });
    }

    public void ShakeCam(float time, float power)
    {
        if(_prevTween != null && _prevTween.IsActive())
        {
            _prevTween.Kill();
        }
        _bPerlin.m_AmplitudeGain = power;
        _prevTween= DOTween.To(
            () => _bPerlin.m_AmplitudeGain,
            value => _bPerlin.m_AmplitudeGain = value,
            0, time);
        
    }
    public void ChangeFollowTarget(CamerCategory categry, Transform target)
    {
        foreach (CameraSet cs in _camList)  
        {
            if (cs.Category == categry)
            {
                cs.VCam.m_Follow = target;
            }

        }
    }
    public void ChangeActiveCam(CamerCategory category)
    {
        foreach (CameraSet cs in _camList)
        {
            if(cs.Category == category)
            {
                cs.VCam.Priority = 15;
            }
            else
            {
                cs.VCam.Priority = 10;
            }
        }
    }
    
}
