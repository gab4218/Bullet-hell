using UnityEngine;

public class PlayerController
{
    private PlayerModel _model;

    private PlayerView _view;

    public Vector2 dir;
    public PlayerController(PlayerModel model, PlayerView view)
    {
        _model = model; 
        _view = view;
    }

    public void OnUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if(_model.canAttack) _view.Attack();
            _model.Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition) - _model.tranform.position);
        }

        dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _model.Move(dir);
        _view.Move(dir);

        if (Input.GetKeyDown(KeyCode.G))
        {
            _model.Piercing();
        }

    }
}
