using UnityEngine;
using UnityEngine.UI;

namespace Interfaces
{
    public interface IItem
    {
        string Id { get; }
        Sprite Sprite { get; }

        IItem Copy();
    }
}