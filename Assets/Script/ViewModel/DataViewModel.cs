using UnityEngine;
using RocketMvvmBase;
using TMPro;

public class DataViewModel : MonoBehaviour
{
    public string eventName;
    private Oberservable<float> sourceData = new();
    public TMP_Text targetData;

    void Start()
    {
        FloatToStringConverter converter = new();
        BindingManager.BindingData<float>((float s) => targetData.text = converter.From(s), sourceData);
        EventBus.Instance.Subscribe(eventName, obj => sourceData.Data = (float)obj);
    }
}
