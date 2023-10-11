
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;
using VRC.SDK3.Components;

public class ObjectManager : UdonSharpBehaviour
{
    public VRCObjectPool Objects;
    public GameObject[] hiddenObjects;
    [UdonSynced]
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

        }
    }
    public void SpawnSphere()
    {
        if (objectsIndex < Objects.Pool.Length)
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            Objects.Pool[objectsIndex].SetActive(true);
            hiddenObjects[objectsIndex].SetActive(true);
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
            RequestSerialization();
        }
    }
    public void ToggleMeshes()
    {
        for (int i = 0; i < objectMeshs.Length; i++)
        {
            Debug.Log(i);
            objectMeshs[i].enabled = !objectMeshs[i].enabled;
        }
        
    }

    public void ToggleCensor()
    {
        uiElements.SetActive(!uiElements.activeSelf);
        quadElements.SetActive(!quadElements.activeSelf);
    }

   
    public override void OnDeserialization() //consider keeping track of whether index went up or down instead
    {
        for(int i = 0; i < Objects.Pool.Length; i++)
        {

            hiddenObjects[i].SetActive(Objects.Pool[i].activeSelf); //for some reason two object pools didnt work
        }
    }
}
