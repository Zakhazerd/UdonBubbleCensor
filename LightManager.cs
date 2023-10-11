
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using UnityEngine.UI;
public class LightManager : UdonSharpBehaviour
{
    public Light directionalLight;
    public Slider lightSlider;

    [UdonSynced, FieldChangeCallback(nameof(LightValue))]
    private float lightValue = 0;
    public float LightValue
    {
        set
        {
            lightValue = value;
            directionalLight.intensity = value;
        }
        get => lightValue;
    }

    public void ChangeLight()
    {
        if (LightValue != lightSlider.value)
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            LightValue = lightSlider.value;
            RequestSerialization();
        }
    }
}
