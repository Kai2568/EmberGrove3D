using UnityEngine;

public class DragonCamera : MonoBehaviour
{
    //[Header("Target")]
    //[SerializeField] private Transform target;

    //[Header("Position")]
    //[SerializeField] private Vector3 offset = new Vector3(0f, 8f, -8f);

    //[Header("Movement")]
    //[SerializeField] private float followSpeed = 5f;

    //private void LateUpdate()
    //{
    //    if (target == null)
    //    {
    //        return;
    //    }

    //    Vector3 desiredPosition = target.position + offset;

    //    transform.position = Vector3.Lerp(
    //        transform.position,
    //        desiredPosition,
    //        followSpeed * Time.deltaTime
    //        );
    //}

    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Camera Position")]
    [SerializeField]
    private Vector3 offset =
        new Vector3(0f, 8f, -8f);

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        transform.position = target.position + offset;
    }

}
