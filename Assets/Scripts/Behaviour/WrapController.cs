using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle the object position for a looping ilusion
/// </summary>
public class WrapController : MonoBehaviour{
    const float start = 0.0f;
    const float end = 1.0f;

    /// <summary>
    /// Check the object position to make it appear in the opposite side fo the screen
    /// </summary>
    public void Wrap(){
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(this.transform.position);

        if (viewportPos.x < start){
            viewportPos.x = end;
        }
        else if (viewportPos.x > end){
            viewportPos.x = start;
        }

        if (viewportPos.y < start) {
            viewportPos.y = end;
        }
        else if (viewportPos.y > end){
            viewportPos.y = start;
        }

        this.transform.position = Camera.main.ViewportToWorldPoint(viewportPos);
    }
    /// <summary>
    /// Called when the objects is off-screen
    /// </summary>
    void OnBecameInvisible(){
        Wrap();
    }
}
