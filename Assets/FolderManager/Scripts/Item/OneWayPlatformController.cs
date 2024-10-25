using UnityEngine;

public class OneWayPlatformController : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime = 0.5f;

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        // Kiểm tra nếu nhấn phím S
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Đặt offset của effector để cho phép nhân vật rơi xuống
            effector.rotationalOffset = 180f;
            Invoke("ResetRotation", waitTime); // Trả lại offset sau thời gian chờ
        }
    }

    void ResetRotation()
    {
        effector.rotationalOffset = 0;
    }
}