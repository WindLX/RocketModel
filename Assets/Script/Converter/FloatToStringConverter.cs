namespace RocketMvvmBase
{
    public class FloatToStringConverter : IConverter<string, float>
    {
        public string From(float sourceData)
        {
            return sourceData.ToString();
        }
    }
}