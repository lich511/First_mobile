using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    private SpriteRenderer _render;
    private Rigidbody2D _rb;
    private Vector2 _position;
    private Vector2 _lastposition;
    private List<Vector2> _positionList = new List<Vector2>();
    private List<GameObject> _Line = new List<GameObject>();

    public GameObject _LineClone;
    public Text _Counter;
    // Start is called before the first frame update
    void Start()
    {
        _render = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _position = transform.position;
        _lastposition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Vector2.Distance(_lastposition, Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position) > 0.2f)
            {
                _positionList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position);

                _Line.Add(Instantiate(_LineClone));
				_Line[_Line.Count - 1].GetComponent<LineRenderer>().SetPosition(0, _lastposition);
				_Line[_Line.Count - 1].GetComponent<LineRenderer>().SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position);

				_lastposition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position;
            }
        }

		if (Vector2.Distance(_position, _rb.position) > 0.1f)
		{
			_rb.velocity = (_position - _rb.position).normalized * 1;
            _Line[0].GetComponent<LineRenderer>().SetPosition(0,_rb.position);
		}
		else
		{
            if (_positionList.Count > 0 && _position == _positionList[0])
            {
                _positionList.RemoveAt(0);
                Destroy(_Line[0]);
                _Line.RemoveAt(0);
            }

			if (_positionList.Count > 0)
			{
				_position = _positionList[0];
				_rb.velocity = (_position - _rb.position).normalized * 1;
			}
			else
				_rb.velocity = new Vector2(0, 0);
		}

	}
	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            Destroy(collision.gameObject);
            _Counter.text = (int.Parse(_Counter.text) + 1).ToString();

			GameObject[] GO = GameObject.FindGameObjectsWithTag("Coin");
            if (GO.Length == 1 && GO[0] == collision.gameObject)
			{
                DataHolder._Status = true;
                SceneManager.LoadScene(1);
            }
                
        }
        else if (collision.tag == "Spikes")
            SceneManager.LoadScene(1);
    }
}
