using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 따라다닐 대상의 Transform 컴포넌트

    public float smoothSpeed = 0.125f; // 카메라 이동 속도
    public Vector3 offset; // 카메라와 대상 간의 오프셋

    private void Start()
    {
        target = GameObject.Find("Painter").transform;
    }
    private void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, 0, target.position.y + offset.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // 부드러운 이동을 위해 Lerp 함수 사용
        transform.position = smoothedPosition; // 카메라 위치 업데이트
    }
}

