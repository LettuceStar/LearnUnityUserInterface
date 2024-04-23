using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class ClickAndSwipe : MonoBehaviour
{
    private UIGameManager _gameMgr;
    private Camera _cam;
    private Vector3 _mousePos;
    private TrailRenderer _trail;
    private BoxCollider _boxCol;

    private bool _swiping = false;


    // Start is called before the first frame update
    void Awake()
    {
        _cam = Camera.main;
        _trail = GetComponent<TrailRenderer>();
        _boxCol = GetComponent<BoxCollider>();
        _trail.enabled = false;
        _boxCol.enabled = false;

        _gameMgr = GameObject.Find("Scripts").GetComponent<UIGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameMgr.isGameActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _swiping = true;
                UpdateComponents();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _swiping = false;
                UpdateComponents();
            }

            if (_swiping)
            {
                UpdateMousePosition();
            }
        }
    }

    // ScreenToWorldwill convert the screen position ofthe mouse to a world position.
    // The reason we use 10.0f on the z axis,
    // is because the camera has thez position of -10.0f.
    private void UpdateMousePosition()
    {
        _mousePos = _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 
            Input.mousePosition.y, 10.0f));
        transform.position = _mousePos;
    }

    private void UpdateComponents()
    {
        _trail.enabled = _swiping;
        _boxCol.enabled = _swiping;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Target>())
        {
            collision.gameObject.GetComponent<Target>().DestroyTarget();
        }
    }

}
