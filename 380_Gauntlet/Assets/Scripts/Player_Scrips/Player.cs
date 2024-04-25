using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private bool _haveControl = false;

    //these are the player's statistics
    private int _health;
    private float _armor;
    private int _moveSpeed;
    private int _magic;
    private int[] _damage;
    private float _shotSpeed;
    private int _score = 0;
    private int _keys = 0;
    private int _potions = 0;
    private List<UpgradeType> _myUpgrades = new List<UpgradeType>();
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
    private void decorateStats()
    {
        this.name = _myCharacter.name;
        _health = _myCharacter.health;
        _armor = _myCharacter.armor;
        _moveSpeed = _myCharacter.moveSpeed;
        _magic = _myCharacter.magic;
        _damage = _myCharacter.damage;
        _shotSpeed = _myCharacter.shotSpeed;
        GetComponent<Renderer>().material = _myCharacter.myColor;
        _projectile = _myCharacter.projectile;
    }

    //this function detects movement input
    public void OnMove(CallbackContext context)
    {
        if (_haveControl)
        {
            _moveDirection = context.ReadValue<Vector2>();
            if (context.performed)
            {
                updateModelDir();
                _moving = true;
            }
            else if (context.canceled)
            {
                _moving = false;
            }
        }
    }

    private void updateModelDir()
    {
        float rotateBy = 0f;

        if (_moveDirection.y < 0)
            rotateBy = 180;
        else if (_moveDirection.y > 0)
            rotateBy = 0;

        if (_moveDirection.x != 0 && _moveDirection.y != 0)
        {
            if (_moveDirection.x < 0)
                rotateBy = (-rotateBy -90) / 2;
            else if (_moveDirection.x > 0)
                rotateBy = (rotateBy + 90) / 2;
        }
        else
        {
            if (_moveDirection.x < 0)
                rotateBy = -90;
            else if (_moveDirection.x > 0)
                rotateBy = 90;
        }

        Vector3 temp = transform.rotation.eulerAngles;
        temp.y = rotateBy;
        transform.rotation = Quaternion.Euler(temp);

    }

    //if we are moving, move the player
    private void FixedUpdate()
    {
        if (_moving)
            transform.position += new Vector3(_moveDirection.x * _moveSpeed * 0.125f, 0, _moveDirection.y * _moveSpeed * 0.125f);
    }

    //INCOMPLETE!
    //This needs to: spawn a projectile in the player's direction, and pass that projectile stats
    public void attack(CallbackContext context)
    {
        if (_haveControl)
        {
            if (context.performed)
            {
                GameObject proj = Instantiate(_projectile, this.transform.position, this.transform.rotation);
                proj.gameObject.GetComponent<Projectile>().damage = getDamage();
                proj.gameObject.GetComponent<Projectile>().shotSpeed = _shotSpeed;
            }
        }
    }

    //INCOMPLETE!
    //This needs to: kill the player if health is below 0, update the ui when hit
    public void takeDamage(int dmg)
    {
        _health = _health - Mathf.RoundToInt(dmg * (1 - _armor));
        if (_health <= 0)
        {
            //kill the player
        }
    }

    public void addHealth(int heal)
    {
        _health += heal;
    }

    public void addScore(int val)
    {
        _score += val;
    }

    public void getPotion()
    {
        if (_potions + _keys <= 12)
            _potions++;
    }

    public void getKey()
    {
        if (_potions + _keys <= 12)
            _keys++;
    }

    public void getUpgrade(UpgradeType upgrade)
    {
        if (!_myUpgrades.Contains(upgrade))
        {
            _myUpgrades.Add(upgrade);
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

    //this lets the player select their character, then enables the action controls
    public void SelectCharacter()
    {
        if (_playerManager.isAvailable(_selection))
        {
            _myCharacter = _playerManager.selectCharacter(_selection, this);
            decorateStats();
            _haveControl = true;
        }
    }

    //randomly returns the low damage number or the high damage number
    public int getDamage()
    {
        return Random.Range(_damage[0], _damage[1] + 1);
    }

    //player collisions! and such
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<iCollectable>() != null)
        {
            collision.gameObject.GetComponent<iCollectable>().pickup(this.GetComponent<Player>());
        }
    }
}
