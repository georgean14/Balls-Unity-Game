using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryAfterAnimation : MonoBehaviour {

    public void DestroyAnimation(){
        Destroy(transform.parent.gameObject);
    }
}
