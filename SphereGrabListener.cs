
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common;

public class SphereGrabListener : UdonSharpBehaviour
{
    public ScaleIntermediateManager intermediateScale;
    public GameObject[] sphereArray;
    private int sphereIndex = 0;
    public override void OnPickup()
    {
        Networking.LocalPlayer.SetJumpImpulse(0);
        for(sphereIndex = 0; sphereIndex < sphereArray.Length; sphereIndex++)
        {
            if (gameObject == sphereArray[sphereIndex])
                break;
        }
        intermediateScale.heldSphere = gameObject.transform;
        intermediateScale.sphereIndex = sphereIndex;
        intermediateScale.enabled = true;   //I am not sure if this is best practice over putting it in here. All udonbehaviors have that functions but I am not sure if it called unless it is in here.
    }

  

    public override void OnDrop()
    {
        Networking.LocalPlayer.SetJumpImpulse(3);
        intermediateScale.enabled = false;
    }
}
