using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private PlayerModel _model;
    public PlayerController(PlayerModel model)
    {
        _model = model; 
    }

    public void OnUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            _model.Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition) - _model.tranform.position);
        }
        var dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _model.Move(dir);
        if (Input.GetKeyDown(KeyCode.M))
        {
            _model.swirly = !_model.swirly;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            _model.aoe = !_model.aoe;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            _model.seeking = !_model.seeking;
        }

    }
}
