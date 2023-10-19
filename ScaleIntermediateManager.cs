
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common;

public class ScaleIntermediateManager : UdonSharpBehaviour
{
    public SphereScaleManager scaleManager;
    public Transform heldSphere;
    public int sphereIndex;
    public Transform orginalScale;
    private float scaleStep;
    private Vector3 twoTimeScale;
    private Vector3 minScale;
    private void Start() 
    {
        scaleStep = orginalScale.localScale.x * .3f;
        twoTimeScale = orginalScale.localScale * 2f;
        minScale = orginalScale.localScale * .1f;
    }
    public override void InputLookVertical(float value, UdonInputEventArgs args)
    {
        if (value == 1 || value == -1)   //Without this analog controlers scale through entire stick movement. Vive is only able to do 1, 0, or -1
        {
            heldSphere.localScale += heldSphere.localScale * (value * scaleStep);
            if (heldSphere.localScale.x < minScale.x)
                heldSphere.localScale = minScale;
            else if (heldSphere.localScale.x > twoTimeScale.x)
                heldSphere.localScale = twoTimeScale;
            scaleManager.syncedScale[sphereIndex] = heldSphere.localScale;
            Networking.SetOwner(Networking.LocalPlayer, scaleManager.gameObject);
            scaleManager.RequestSerialization();
        }
    }
}
