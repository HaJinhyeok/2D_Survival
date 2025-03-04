using UnityEngine;

public interface IMagnetic
{
    float Speed { get; set; }
    void PullItemsAround(GameObject origin, float distance);

}
