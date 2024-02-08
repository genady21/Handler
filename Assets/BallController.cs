using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;


public class BallController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private PointerInputModule pointer;

    Vector2 mousePosition;
    Vector2 currentPosition;

    Vector2 reverseDirection;

    Camera cam;
    [SerializeField] private float speed;
    private float speedModif;

    private float timer = 0;
    private bool _showWinScreen;
    private bool _onInside;


    void Awake()
    {
        cam = Camera.main;
        speedModif = speed;
        
    }

    void Update()
    {
        mousePosition = pointer.input.mousePosition;
        currentPosition = transform.position;
        speed = speedModif * (timer + 1);
        reverseDirection = -(mousePosition - currentPosition).normalized * speed;
        transform.Translate(reverseDirection);

        if (_onInside)
        {
            timer += Time.deltaTime;
        }

        if (timer > 1f)
        {
            Debug.Log("win");
            Time.timeScale = 0f;
            _showWinScreen = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _onInside = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exit");
        _onInside = false;
        timer = 0;
    }

    void OnGUI()
    {
        if (_showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "YOU WON!"))
            {
                SceneManager.LoadScene("SampleScene");
                Time.timeScale = 1.0f;
                _showWinScreen = false;
                timer = 0;
            }
        }
    }
}
