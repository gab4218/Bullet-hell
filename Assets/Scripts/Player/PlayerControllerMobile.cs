using UnityEngine;

public class PlayerControllerMobile : IController
{
    private PlayerModel _model;

    private PlayerView _view;

    private JoystickInput _movement, _attack;

    public PlayerControllerMobile(PlayerModel model, PlayerView view)
    {
        _model = model;
        _view = view;
    }

    public PlayerControllerMobile SetInputs(JoystickInput m, JoystickInput a)
    {
        _movement = m;
        _attack = a;
        return this;
    }

    public void OnUpdate()
    {
        if (_attack.dir.magnitude > 0)
        {
            if(_model.canAttack) _view.Attack();
            _model.Shoot(_attack.dir.normalized);
        }

        _model.Move(_movement.dir);
        _view.Move(_movement.dir);

    }
}
