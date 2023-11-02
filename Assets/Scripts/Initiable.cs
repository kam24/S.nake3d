using UnityEngine;


public abstract class Initiable : MonoBehaviour
{
    public virtual void Init()
    {
        enabled = true;
    }    
}

