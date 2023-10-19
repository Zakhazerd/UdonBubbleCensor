
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class LaserPointerRayCaster : UdonSharpBehaviour
{
    public Texture2D palleteTexture;
    public Collider palleteCollider;
    public LineRenderer palleteLine;
    public Transform lineTransform;
    public QuadColorManager myColorManager;
    private Vector3 localHitPos;
    private Color quadColor;
    public VRC_Pickup myPickUp;
    
    //at some point test spawning a camera to get any arbitrary color from collider
    private void Update()//should i used fixedupdate? that is called befoer input events but im not sure how the behavior changes from update
    {

        Ray laserRay = new Ray(lineTransform.position, lineTransform.forward);
        RaycastHit colliderInfo;
        palleteLine.SetPosition(0, lineTransform.position);
        if (Physics.Raycast(laserRay, out colliderInfo, Mathf.Infinity) && colliderInfo.collider == palleteCollider)
        {
            palleteLine.enabled = true;
            palleteLine.SetPosition(1, colliderInfo.point);
            localHitPos = colliderInfo.collider.gameObject.transform.InverseTransformPoint(colliderInfo.point);

        }
        else
        {
            palleteLine.enabled = false;
        }
    }
    public override void OnPickupUseDown()
    {
        if (myPickUp.currentPlayer == Networking.LocalPlayer && palleteLine.enabled)//i may be stupid this->pickup??? but also make sure it only does it while looking at pallete.
        {
            quadColor = palleteTexture.GetPixel((int)(255 * localHitPos.x), (int)(255 * localHitPos.y));
            myColorManager.UpdateColor(quadColor);

        }

    }
    
}

