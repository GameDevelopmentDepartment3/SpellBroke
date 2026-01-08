using UnityEngine;

public interface IMagic
{
    string Name { get; }
    int Cast(Vector3 position);
}
