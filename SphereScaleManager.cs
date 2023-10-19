
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;
using TMPro;

public class SphereScaleManager : UdonSharpBehaviour
{
    public TextMeshProUGUI sphereIndexText;
    private int sphereIndex;
    public GameObject[] sphereArray;
    [UdonSynced]
    public Vector3[] syncedScale = new Vector3[30]; 
    public Slider scaleSlider;
    public Transform sphereOrginalScale;
    private void Start()
    {
        if (Networking.LocalPlayer.IsOwner(gameObject))
        {
            for (int i = 0; i < syncedScale.Length; i++)
            {
                syncedScale[i] = sphereArray[i].transform.localScale;
                RequestSerialization();
            }
        }
        else
        {
            for (int i = 0; i < sphereArray.Length; i++)
            {
                sphereArray[i].transform.localScale = syncedScale[i];
            }
        }
    }
    public void IndexRight()
    {
        sphereIndex++;
        if(sphereIndex > 29) //hard coding the array size it bad also if is faster than modulo
        {
            sphereIndex = 0;
        }
        sphereIndexText.text = sphereIndex.ToString();
    }

  
    public void IndexLeft()
    {
        sphereIndex--;
        if(sphereIndex < 0)
        {
            sphereIndex = 29;
        }
        sphereIndexText.text = sphereIndex.ToString();
    }

    public void ScaleChange()
    {
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        syncedScale[sphereIndex] = sphereOrginalScale.localScale * scaleSlider.value;
        sphereArray[sphereIndex].transform.localScale = syncedScale[sphereIndex];
        RequestSerialization();
    }

    public override void OnDeserialization() //ya im being lazy because the documentation lied about objectsync doing scale
    {
        for (int i = 0; i < sphereArray.Length; i++)
        {
            sphereArray[i].transform.localScale = syncedScale[i];
        }
    }
}
