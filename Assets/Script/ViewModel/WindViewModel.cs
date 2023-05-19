using UnityEngine;
using RocketMvvmBase;
using DG.Tweening;

public class WindViewModel : MonoBehaviour
{
    private Oberservable<float> sourceData = new();
    public Material windSpeed;

    void Start()
    {
        BindingManager.BindingData<float>((s) => CalcWind(s), sourceData);
        EventBus.Instance.Subscribe("Acc", obj => sourceData.Data = ((RocketModel.Models.Vector3)obj).Z);
    }

    private void CalcWind(float accZ)
    {
        if (accZ > 1)
        {
            var strength = (accZ - 1);
            var speed = (accZ - 1) / 0.07;
            DOTween.To(() => windSpeed.GetFloat("_Speed"), x => windSpeed.SetFloat("_Speed", (float)x), speed, Time.fixedDeltaTime);
            DOTween.To(() => windSpeed.GetFloat("_Strength"), x => windSpeed.SetFloat("_Strength", (float)x), strength, Time.fixedDeltaTime);
        }
        else
        {
            DOTween.To(() => windSpeed.GetFloat("_Speed"), x => windSpeed.SetFloat("_Speed", (float)x), 0, Time.fixedDeltaTime);
            DOTween.To(() => windSpeed.GetFloat("_Strength"), x => windSpeed.SetFloat("_Strength", (float)x), 0, Time.fixedDeltaTime);
        }
    }
}
