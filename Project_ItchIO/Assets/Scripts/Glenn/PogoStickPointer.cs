using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PogoStickPointer : MonoBehaviour
{
  
    void Update()
    {
        PogoStickMover();
    }
    void PogoStickMover()
    {
        transform.localPosition = new Vector2(0, 1.5f);
        print(transform.localPosition.y);
    }
    
}
