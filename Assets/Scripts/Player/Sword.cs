using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Transform firePoint;
    public Transform beforeFirePoint;
    private Vector2 FireDirection
    {
        get { return firePoint.position - beforeFirePoint.position; }
    }

    private void Update()
    {
        Vector3 cpos = Utils.MouseWorldPos;
        Vector2 vector21 = (cpos - beforeFirePoint.position).normalized;
        Debug.DrawLine(firePoint.position, beforeFirePoint.position, Color.red);
        Debug.DrawLine(cpos, beforeFirePoint.position, Color.blue);
        float angle = Vector2.SignedAngle(FireDirection, vector21);
        transform.Rotate(new(0, 0, angle / 5 * PlayerController.Instance.facingRight));
    }
}
