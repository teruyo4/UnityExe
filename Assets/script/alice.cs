using UnityEngine;

public class alice : MonoBehaviour {
    private GameManager gm;

    void Awake() {
        gm = (GameObject.FindWithTag("gamemanager")).GetComponent<GameManager>();
    }
    
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "rabbit") {
            gm.becaught();
        }
    }

    public void remove() {
        Destroy(this.gameObject);
    }
}
