using UnityEngine;
using UnityEngine.UI;

namespace Interfaces
{
    public interface IItem
    {
        string Id { get; }
        Sprite Sprite { get; }
        
        int Amount { get; }
        
        bool ChangeAmount(int value);

        IItem Copy();
    }
}