using UnityEngine;
using RocketMvvmBase;
using TMPro;

public class VectorViewModel : MonoBehaviour
{
    public string eventName;
    private Oberservable<RocketModel.Models.Vector3> sourceData = new();
    public TMP_Text targetData;

    void Start()
    {
        VectorToStringConverter converter = new();
        BindingManager.BindingData<RocketModel.Models.Vector3>((RocketModel.Models.Vector3 s) => targetData.text = converter.From(s), sourceData);
        EventBus.Instance.Subscribe(eventName, obj => sourceData.Data = (RocketModel.Models.Vector3)obj);
    }
}
