using UnityEngine;
using RocketMvvmBase;
using DG.Tweening;

public class AltitudeRulerViewModel : MonoBehaviour
{
    private Oberservable<float> sourceData = new();
    public Material targetData;

#nullable enable
    private float? firstAltitude;
#nullable disable

    void Start()
    {
        BindingManager.BindingData<float>(s => CalcAltitude(s), sourceData);
        EventBus.Instance.Subscribe("Altitude", obj => sourceData.Data = (float)obj);
    }

    private void CalcAltitude(float altitude)
    {
        if (firstAltitude == null)
            firstAltitude = altitude;
        else
            targetData.DOOffset(new Vector2(0, (float)(altitude - firstAltitude) * 0.004266666667f), Time.fixedDeltaTime).SetEase(Ease.Linear);
    }
}
