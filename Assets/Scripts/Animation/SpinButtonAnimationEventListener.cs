using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinButtonAnimationEventListener : MonoBehaviour
{
    private WheelController wheelController;

    public void Initialize(WheelController wheelController)
    {
        this.wheelController = wheelController;
    }
    
    public void OnSpinBornAnimComplete()
    {
        wheelController.SpinBornAnimComplete();
    }
    
    public void OnSpinDisappearAnimComplete()
    {
        wheelController.SpinDisappearAnimComplete();
    }
}
