using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashPlayer : MonoBehaviour
{
    public Vector2 projectileSpeed = new Vector2(8f,0);  // Tốc độ chiêu bắn ra
    public float returnTime = 3f;       // Thời gian sau bao lâu chiêu quay lại
    public float returnSpeed = 8f;      // Tốc độ chiêu quay trở lại
    private Transform player;            // Vị trí của nhân vật
    private bool isReturning = false;    // Kiểm tra xem chiêu có đang quay lại không

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Lấy nhân vật qua tag
        StartCoroutine(ReturnAfterTime());
    }
       // rb.velocity = new Vector2(projectileSpeed.x * transform.localScale.x, projectileSpeed.y);
    void Update()
    {
        if (!isReturning)
        {
            // Nếu chưa quay lại, chiêu bắn ra phía trước
            //rb.velocity = transform.right * projectileSpeed;
            rb.velocity = new Vector2(projectileSpeed.x * transform.localScale.x, projectileSpeed.y);
        }
        else
        {
            // Nếu chiêu đang quay lại, di chuyển chiêu về phía nhân vật
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            rb.velocity = directionToPlayer * returnSpeed;

            // Nếu chiêu đến gần nhân vật, tiêu diệt nó
            if (Vector2.Distance(transform.position, player.position) < 0.5f)
            {
                Destroy(gameObject);  // Chiêu biến mất khi chạm vào nhân vật
            }
        }
    }

    IEnumerator ReturnAfterTime()
    {
        // Đợi trong khoảng thời gian returnTime trước khi chiêu quay lại
        yield return new WaitForSeconds(returnTime);

        // Đổi hướng animation

        isReturning = true;  // Kích hoạt trạng thái quay lại
    }
}
