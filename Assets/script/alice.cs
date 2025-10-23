using UnityEngine;

public class alice : MonoBehaviour {
    public bool opeFlag;

    private float speedX; // 移動速度
    private GameManager gm;

    void Awake()
    {
        gm = (GameObject.FindWithTag("gamemanager")).GetComponent<GameManager>();
    }
    
    void Start()
    {
        opeFlag = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "rabbit") {
            gm.becaught();
        }
    }

    void FixedUpdate()
    {
      if (opeFlag)
        transform.Translate(speedX, 0f, 0f);
    }

    public void ChangeBehaviour(float sp, float animsp)
    {
        this.gameObject.GetComponent<Animator>().speed = animsp;
        speedX = sp;
    }

    public void remove()
    {
        Destroy(this.gameObject);
    }
}
