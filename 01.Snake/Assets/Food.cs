using UnityEngine;

public class Food : MonoBehaviour
{
    //ע����Ҫ��������򣬹�����gridArea
    public BoxCollider2D gridArea;
    // Start is called before the first frame update
    void Start()
    {
        RandomizePosition();
    }
    private void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y),0.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RandomizePosition();
        }
    }
}
