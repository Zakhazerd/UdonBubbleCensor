using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;
using VRC.SDK3.Components;

public class QuadColorManager : UdonSharpBehaviour
{
    public LaserPointerRayCaster myRayCaster;
    public Material censorMaterial;
    [UdonSynced]
    private Color quadColor = new Color(1f,0,0);

    private void OnEnable()
    {
        censorMaterial.color = quadColor;
    }
    public override void OnPickup()
    {
        myRayCaster.enabled = true;
    }
    public override void OnDrop()
    {
        myRayCaster.enabled = false;
    }

    public void UpdateColor(Color newColor)
    {
        quadColor = newColor;
        censorMaterial.color = quadColor;
        RequestSerialization();
    }


    
    public override void OnDeserialization()
    {
        censorMaterial.color = quadColor;
    }
}
