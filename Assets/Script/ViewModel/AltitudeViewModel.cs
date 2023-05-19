using UnityEngine;
using RocketMvvmBase;
using DG.Tweening;

public class AltitudeViewModel : MonoBehaviour
{
    public float disPara = 1;
    private Oberservable<float> sourceData = new();
    public Transform targetData;

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
            targetData.DOLocalMoveY((float)(altitude - firstAltitude) / disPara, Time.fixedDeltaTime).SetEase(Ease.Linear);
    }
}
