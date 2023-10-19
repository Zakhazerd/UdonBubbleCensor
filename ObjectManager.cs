
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;
using VRC.SDK3.Components;
using System.Collections;
using VRC.Udon.Common;

public class ObjectManager : UdonSharpBehaviour
{
    public VRCObjectPool Objects; //is there a point to doing this over array of gameobjects? I think I had different intentions orginalls
    public GameObject[] hiddenObjects;
    public GameObject[] alwaysOnSynced; //ill think of a better solution i just wanna be done
    public VRCPickup[] forAllowPickup; //if the owner has a vrcpickup has it disabled and someone else moves that pickup ownership and syncing breaks until moved while they can see it
    [UdonSynced]                       //idk why it worked fine for the pen tho more testing required
    int objectsIndex = 0;
    public MeshRenderer[] objectMeshs;
    public GameObject uiElements;
    public GameObject quadElements;
    public Material censorMaterial;
   

  
    void Start()
    {
        for (int i = 0; i < objectsIndex; i++)
        {
         // Objects.Pool[i].SetActive(true);
            hiddenObjects[i].SetActive(true); //for some reason two object pools didnt work
            alwaysOnSynced[i].SetActive(true);
        }
    }
    public void SpawnSphere()
    {
        if (objectsIndex < Objects.Pool.Length)
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            Objects.Pool[objectsIndex].SetActive(true);
            hiddenObjects[objectsIndex].SetActive(true);
            alwaysOnSynced[objectsIndex].SetActive(true);
            objectsIndex++;
            RequestSerialization();
        }
    }
    public void DespawnSphere()
    {
        if(objectsIndex > 0)
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            objectsIndex--;
            Objects.Pool[objectsIndex].SetActive(false);
            hiddenObjects[objectsIndex].SetActive(false);
            alwaysOnSynced[objectsIndex].SetActive(false);
            RequestSerialization();
        }
    }
    public void ToggleMeshes()
    {
        for (int i = 0; i < objectMeshs.Length; i++)
        {
            objectMeshs[i].enabled = !objectMeshs[i].enabled;
        }
        
    }

    public void ToggleCensor()
    {
        uiElements.SetActive(!uiElements.activeSelf);
        quadElements.SetActive(!quadElements.activeSelf);
        for (int i = 0; i < forAllowPickup.Length; i++)
        {
            forAllowPickup[i].pickupable = uiElements.activeSelf;
            objectMeshs[i].enabled = uiElements.activeSelf;
        }
        
    }

   public void TogglePickup()
    {
        for (int i = 0; i < objectsIndex; i++)
            forAllowPickup[i].pickupable = !forAllowPickup[i].pickupable;
    }
    public override void OnDeserialization() //Consider keeping track of whether index went up or down instead. What happens when both sendnetowrkedevents for spawn and despawn are recieved
    {
        for(int i = 0; i < alwaysOnSynced.Length; i++)
        {

            hiddenObjects[i].SetActive(Objects.Pool[i].activeSelf); //for some reason two object pools didnt work
            alwaysOnSynced[i].SetActive(Objects.Pool[i].activeSelf);
        }
    }

  

}
