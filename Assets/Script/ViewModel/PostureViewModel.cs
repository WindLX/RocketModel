using DG.Tweening;
using UnityEngine;
using RocketMvvmBase;

public class PostureViewModel : MonoBehaviour
{
    private Oberservable<Vector3> sourceData = new();
    public Transform targetData;

    void Start()
    {
        BindingManager.BindingData<Vector3>(s => targetData.DORotate(s, Time.fixedDeltaTime).SetEase(Ease.Linear), sourceData);
        EventBus.Instance.Subscribe("Posture", obj => sourceData.Data = ((RocketModel.Models.Vector3)obj).Into());
    }
}
