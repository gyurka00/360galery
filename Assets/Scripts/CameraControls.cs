using UnityEngine;
using UnityEngine.SceneManagement;
using OVR;

public class CameraControls : MonoBehaviour
{
    public float speed = 10;
    public string sceneName;

    void Update()
    {

#if (UNITY_EDITOR || DEVELOPMENT_BUILD)


        if (Time.deltaTime > 0.2f) return; // Prevent Random bullshit rotation on app loading or lag

        // LOOK

        transform.Rotate(new Vector3( // look up / down
            -Input.GetAxis("Mouse Y"),
            0,
            0
        ), Space.Self);

        transform.Rotate(new Vector3( // look left / right
            0,
            Input.GetAxis("Mouse X"),
            0
        ), Space.World); // Space.World to avoid ROLL: https://en.wikipedia.org/wiki/Aircraft_principal_axes#/media/File:Yaw_Axis_Corrected.svg

        // MOVE (forward, back, left, right)

        transform.position += transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.position += transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed;

        transform.Rotate(new Vector3( // look up / down
            -Input.GetAxisRaw("Vertical"),
            0,
            0
        ), Space.Self);

        transform.Rotate(new Vector3( // look left / right
            0,
            Input.GetAxisRaw("Horizontal"),
            0
        ), Space.World);
        // MOVE (up, down)

        if (Input.GetButton("Fire1"))
        {
            transform.position += transform.up * Time.deltaTime * speed * 0.6f;
        }
        if (Input.GetButton("Fire2"))
        {
            transform.position -= transform.up * Time.deltaTime * speed * 0.6f;
        }

#endif
    }

    void FixedUpdate()
    {
#if (DEVELOPMENT_BUILD)
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            SceneManager.LoadScene(sceneName);
        }
#endif
    }
}
