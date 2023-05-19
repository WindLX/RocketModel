using UnityEngine;
using RocketMvvmBase;
using DG.Tweening;

public class YawViewModel : MonoBehaviour
{
    private Oberservable<float> sourceData = new();
    public Material targetData;

    void Start()
    {
        BindingManager.BindingData<float>((s) => CalcYaw(s), sourceData);
        EventBus.Instance.Subscribe("Yaw", obj => sourceData.Data = (float)obj);
    }

    private void CalcYaw(float yaw)
    {
        if (yaw < 0)
            yaw += 360;
        targetData.DOOffset(new Vector2(yaw * 0.00284444444444f, 0), Time.fixedDeltaTime).SetEase(Ease.Linear);
    }
}
