using UnityEngine;

public interface IMagnetic
{
    float PullSpeed { get; set; }
    void PullItemsAround(GameObject origin, float distance);

}
