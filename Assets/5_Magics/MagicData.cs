using UnityEngine;

[CreateAssetMenu(fileName = "MagicData", menuName = "Scriptable Objects/MagicData")]
public abstract class MagicData : ScriptableObject, IMagic
{
    public string magicName;
    public abstract string Name { get; }
    public abstract void Cast(Vector3 position);
}
