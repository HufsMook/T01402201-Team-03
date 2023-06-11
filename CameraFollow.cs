using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // ����ٴ� ����� Transform ������Ʈ

    public float smoothSpeed = 0.125f; // ī�޶� �̵� �ӵ�
    public Vector3 offset; // ī�޶�� ��� ���� ������

    private void Start()
    {
        target = GameObject.Find("Painter").transform;
    }
    private void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, 0, target.position.y + offset.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // �ε巯�� �̵��� ���� Lerp �Լ� ���
        transform.position = smoothedPosition; // ī�޶� ��ġ ������Ʈ
    }
}

