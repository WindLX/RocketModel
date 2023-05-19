using UnityEngine;
using RocketMvvmBase;
using DG.Tweening;

public class RollViewModel : MonoBehaviour
{
    private Oberservable<float> sourceData = new();
    public Transform targetData;

    void Start()
    {
        BindingManager.BindingData<float>((s) => targetData.DORotate(new Vector3(0, 0, s), Time.fixedDeltaTime).SetEase(Ease.Linear), sourceData);
        EventBus.Instance.Subscribe("Roll", obj => sourceData.Data = (float)obj);
    }
}
