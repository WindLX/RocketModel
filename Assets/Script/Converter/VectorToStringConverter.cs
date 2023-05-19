using RocketModel.Models;

namespace RocketMvvmBase
{
    public class VectorToStringConverter : IConverter<string, Vector3>
    {
        public string From(Vector3 sourceData)
        {
            return sourceData.ToString();
        }
    }
}