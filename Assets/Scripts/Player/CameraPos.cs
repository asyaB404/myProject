using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    [SerializeField]
    PlayerController player;

    public Vector3 direction;

    [SerializeField]
    //注意计算时考虑了摄像机的Z轴距离
    private float maxDistanceDelta = 8;

    //是否开启摄像机随玩家和鼠标指针的跟随
    public bool isOpen = true;

    void Update()
    {
        Vector3 mousePos = Utils.MouseWorldPos;
        direction = (mousePos - transform.position).normalized;
        if (isOpen)
        {
            float distance = (mousePos - player.transform.position).magnitude;
            if (distance >= maxDistanceDelta)
            {
                transform.localPosition =
                    maxDistanceDelta
                    * new Vector3(direction.x * player.facingRight, direction.y, 0);
            }
            else
            {
                transform.localPosition =
                    distance * new Vector3(direction.x * player.facingRight, direction.y, 0);
            }
        }
    }
}
