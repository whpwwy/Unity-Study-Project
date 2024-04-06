using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Snake : MonoBehaviour
{
    //初始化方向向右
    private Vector2 _direction = Vector2.right;

    //初始化Snake的列表
    private List<Transform> _segments = new List<Transform>();
    //注意需要关联预制Transform
    public Transform segmentPrefab;

    public int initialSize = 4;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(this.transform);

        for (int i = 1; i < this.initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        this.transform.position = Vector3.zero;
    }

    // Update is called once per frame
    // 每次update 都识别按键，根据按键将_direction的方向进行修改
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector2.up;
        }else if(Input.GetKeyDown(KeyCode.S))
        { 
            _direction = Vector2.down;
        }else if(Input.GetKeyDown(KeyCode.D)) 
        {
            _direction = Vector2.right;
        }else if (Input.GetKeyDown(KeyCode.A))
        {
            _direction = Vector2.left;
        }else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit ();
        }

    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }
        
        //重新计算坐标后，更新Snake的位置
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
            );
    }

    //Snake身体增长
    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
        }else if (other.tag == "Obstacle")
        {
            Start();
        }
    }
}
