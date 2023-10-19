using UdonSharp;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;


public class QuadColorManager : UdonSharpBehaviour
{
    public LaserPointerRayCaster myRayCaster;
    public Material censorMaterial;
    public LineRenderer palleteLine;
    public Transform orginalPos;
    [UdonSynced]
    private Color quadColor = new Color(1f, 0, 0);
    public Rigidbody penRidgidbody;

    private void OnEnable()
    {
        censorMaterial.color = quadColor;
    }
    public override void OnPickup() //this can be desync with current setup
    {
        myRayCaster.enabled = true;
    }
    public override void OnDrop()
    {
        myRayCaster.enabled = false;
        SendCustomEventDelayedFrames("DisableLine", 1);
        penRidgidbody.velocity = new Vector3(0, 0, 0);
        penRidgidbody.angularVelocity = new Vector3(0, 0, 0);
    }

    public void UpdateColor(Color newColor)
    {
        quadColor = newColor;
        censorMaterial.color = quadColor;
        RequestSerialization();
    }

    public void RespawnPen()
    {
        gameObject.transform.position = orginalPos.position;
    }


    public override void OnDeserialization()
    {
        censorMaterial.color = quadColor;
    }

    public void DisableLine()
    {
        palleteLine.enabled = false;
    }
}

