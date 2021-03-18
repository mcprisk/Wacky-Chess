using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Rotation : MonoBehaviour
{
    // Start is called before the first frame update
    public float rot_speed;

    private float rot_h;
    private float rot_v;

    private Vector3 rot;

    // Update is called once per frame
    void FixedUpdate()
    {
        rot_h += Input.GetAxis("Horizontal");
        rot_v += Input.GetAxis("Vertical");

        rot = new Vector3(rot_v + 180, rot_h, 0f);

        transform.localEulerAngles = (rot * rot_speed * Time.deltaTime);
    }
}
