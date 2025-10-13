using UnityEngine;

public class rabbit : MonoBehaviour
{
    public float accelX;
    private float speedX;
    
    public float span = 3f;
    private float curTime = 0f;

    private Rigidbody2D rb;

    void Start()
    {
        accelX = -0.01f;
        speedX = 0f;

        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        curTime += Time.deltaTime;

        if (curTime > span) {
            speedX += accelX;
            curTime = 0f;
        }
        rb.AddForce(new Vector3(speedX, 0f, 0f));
    }
}
