using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    //these keep track of character selection
    private CharacterClass _myCharacter;
    private int _selection = 0;

    //these keep track of the player's movement
    private bool _moving = false;
    private Vector2 _moveDirection;

    //these are the player's statistics
    private int _health;
    private float _armor;
    private int _moveSpeed;
    private int _magic;
    private int[] _damage;
    private int _score = 0;
    private int _keys = 0;
    private int _potions = 0;
    private int[] _powerups;
    private GameObject _projectile;

    private Rigidbody _rigidbody;
    private PlayerManager _playerManager;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();

        //this automatically puts the selector on the first available character
        for (int i = 0; i < 3; i++)
        {
            if (!_playerManager.isAvailable(_selection))
                _selection++;
        }
    }

    //gives the player the stats from their character class
    private void loadStats()
    {
        _health = _myCharacter.health;
        _armor = _myCharacter.armor;
        _moveSpeed = _myCharacter.moveSpeed;
        _magic = _myCharacter.magic;
        _damage = _myCharacter.damage;
        GetComponent<Renderer>().material = _myCharacter.myColor;
        _projectile = _myCharacter.projectile;
    }

    //this function detects movement input
    public void OnMove(CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>();
        if (context.performed)
        {
            _moving = true;
            //_rigidbody.AddForce(new Vector3(direction.x * _moveSpeed * 100, 0, direction.y * _moveSpeed * 100));
        }
        else if (context.canceled)
        {
            _moving = false;
        }
    }

    //if we are moving, move the player
    private void FixedUpdate()
    {
        if (_moving)
            transform.position += new Vector3(_moveDirection.x * _moveSpeed * 0.125f, 0, _moveDirection.y * _moveSpeed * 0.125f);
    }

    //INCOMPLETE!
    //This needs to: spawn a projectile in the player's direction, and pass that projectile stats
    protected virtual void attack()
    {

    }

    //INCOMPLETE!
    //This needs to: kill the player if health is below 0, update the ui when hit
    public void takeDamage(int dmg)
    {
        _health = _health - dmg;
        if (_health <= 0)
        {
            //kill the player
        }
    }

    //INCOMEPLETE!
    //This needs to: damage all enemies on screen based on the user's magic stat
    //This should totally implement a design pattern because it'd be really helpful here actually
    private void usePotion()
    {

    }

    //these four functions are used to scroll the selected character
    public void ScrollDown()
    {
        subtractAndScroll();
        if (!_playerManager.isAvailable(_selection))
        {
            subtractAndScroll();
            if (!_playerManager.isAvailable(_selection))
            {
                subtractAndScroll();
            }
        }
    }

    private void subtractAndScroll()
    {
        _selection--;
        if (_selection < 0)
            _selection = 3;
    }

    public void ScrollUp()
    {
        addAndScroll();
        if (!_playerManager.isAvailable(_selection))
        {
            addAndScroll();
            if (!_playerManager.isAvailable(_selection))
            {
                addAndScroll();
            }
        }
    }

    private void addAndScroll()
    {
        _selection++;
        if (_selection > 3)
            _selection = 0;
    }

    //this lets the player select their character
    public void SelectCharacter()
    {
        if (_playerManager.isAvailable(_selection))
        {
            _myCharacter = _playerManager.selectCharacter(_selection);
            loadStats();
        }
    }

    //randomly returns the low damage number or the high damage number
    public int getDamage()
    {
        return Random.Range(_damage[0], _damage[1] + 1);
    }
}
