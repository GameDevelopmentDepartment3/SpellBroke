using UnityEngine;

public interface IMagic
{
    string Name { get; }
    void Cast(Vector3 position);
}
