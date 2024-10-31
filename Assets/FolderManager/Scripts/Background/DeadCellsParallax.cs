using UnityEngine;

public class DeadCellsParallax : MonoBehaviour
{
    public Transform cameraTransform;      // Camera
    public Vector2 parallaxEffectMultiplier; // Điều chỉnh tốc độ x/y parallax cho lớp này
    private float textureUnitSizeX;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        // Lấy vị trí ban đầu của camera
        lastCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        // Tính sự thay đổi của camera
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        // Điều chỉnh vị trí của lớp nền
        transform.position += new Vector3(
            deltaMovement.x * parallaxEffectMultiplier.x,
            deltaMovement.y * parallaxEffectMultiplier.y,
            0);

        // Cập nhật vị trí cuối của camera
        lastCameraPosition = cameraTransform.position;
        if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
        {
            float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
        }
    }
}