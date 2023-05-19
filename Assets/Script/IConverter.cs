using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketMvvmBase
{
    public interface IConverter<T, S>
    {
        T From(S sourceData);
    }
}
