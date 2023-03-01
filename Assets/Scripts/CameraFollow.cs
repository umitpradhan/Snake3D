using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject platform;

    private Collider platformCollider;
    private float xBound;
    private float zBound;

    private void Awake()
    {
        // Get the collider of the platform object
        platformCollider = GameObject.FindWithTag("Platform").GetComponent<Collider>();

        // Calculate the x and z bounds of the platform based on its size
        xBound = platformCollider.bounds.size.x / 2;
        zBound = platformCollider.bounds.size.z / 2;
    }
    // Use this for initialization
    void Start()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = platformCollider.bounds.size.x / platformCollider.bounds.size.z;

        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = platformCollider.bounds.size.z / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = platformCollider.bounds.size.z / 2 * differenceInSize;
        }

        //public Transform target;
        ////public Camera cam;

        //public bool isCustomOffset;
        //public Vector3 offset;

        //public float smoothSpeed = 1.0f;

        //private void Start()
        //{
        //    // You can also specify your own offset from inspector
        //    // by making isCustomOffset bool to true
        //    if (!isCustomOffset)
        //    {
        //        offset = transform.position - target.position;
        //    }
        //}

        //private void LateUpdate()
        //{
        //    SmoothFollow();   
        //}

        //public void SmoothFollow()
        //{
        //    //Vector3 direction = (target.position - cam.transform.position).normalized;
        //    Vector3 targetPos = target.position + offset;
        //    Vector3 smoothFollow = Vector3.Lerp(transform.position, targetPos, smoothSpeed);

        //    transform.position = smoothFollow;
        //    //Quaternion lookRotation = Quaternion.LookRotation(direction);
        //    //lookRotation.x = transform.rotation.x;
        //    //lookRotation.z = transform.rotation.z;
        //    //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 100);
        //    //transform.position = Vector3.Slerp(transform.position, target.position, Time.deltaTime * smoothSpeed);

        //    transform.LookAt(target);
        //}
    }
}