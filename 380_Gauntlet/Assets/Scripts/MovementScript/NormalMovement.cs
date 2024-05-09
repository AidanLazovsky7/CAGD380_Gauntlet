using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMovement : MonoBehaviour, iMovement
{
    private float _moveSpeed = .25f;
    private EnemyParent myEnemy;

    public void ExecuteMovementPattern( Vector3 targetPos)
    {
        StartCoroutine(Move(targetPos));
    }

    private void Awake()
    {
        myEnemy = this.GetComponent<EnemyParent>();
    }

    private IEnumerator Move(Vector3 targetPos)
    {

        Vector2 _moveDirection = new Vector2();
        while (myEnemy.isMoving)
        {

            if (transform.position.x < targetPos.x) _moveDirection.x = 1;
            else _moveDirection.x = -1;

            if (transform.position.z < targetPos.z) _moveDirection.y = 1;
            else _moveDirection.y = -1;

          
                float rotateBy = 0f;

                if (_moveDirection.y < 0)
                    rotateBy = 180;
                else if (_moveDirection.y > 0)
                    rotateBy = 0;

                if (_moveDirection.x != 0 && _moveDirection.y != 0)
                {
                    if (_moveDirection.x < 0)
                        rotateBy = (-rotateBy - 90) / 2;
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

                Debug.Log("I should be turning");
            //transform.position += new Vector3(_moveDirection.x * _moveSpeed, 0, _moveDirection.y * _moveSpeed);
            yield return new WaitForSeconds(.1f);
        }
            
        }
    }

