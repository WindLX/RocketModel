using UnityEngine;
using RocketMvvmBase;
using DG.Tweening;

public class PitchViewModel : MonoBehaviour
{
    private Oberservable<float> sourceData = new();
    public Material targetData;

    void Start()
    {
        BindingManager.BindingData<float>((s) => CalcPitch(s), sourceData);
        EventBus.Instance.Subscribe("Pitch", obj => sourceData.Data = (float)obj);
    }

    private void CalcPitch(float pitch)
    {
        targetData.DOOffset(new Vector2(0, pitch * 0.00284444444444f), Time.fixedDeltaTime).SetEase(Ease.Linear);
    }
}
