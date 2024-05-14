using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkMovement : MonoBehaviour, iMovement
{
    private float _moveSpeed = .25f;
    private EnemyParent myEnemy;

    public void ExecuteMovementPattern(GameObject targetEnemy, float minDist, float maxDist)
    {
        StartCoroutine(Move(targetEnemy, minDist, maxDist));
    }

    private void Awake()
    {
        myEnemy = this.GetComponent<EnemyParent>();
    }

    private IEnumerator Move(GameObject targetEnemy, float minDist, float maxDist)
    {
        float currentDist = Vector3.Distance(transform.position, targetEnemy.transform.position);

        Vector2 _moveDirection = new Vector2();

        //this is a copy-paste of the normal move function. not very nice, but the only way i could get blinking to work without modifying the original function.
        //basically, this moves normally, and will randomly blink around as well.
        //i think it could be nicer if normalMovement didn't loop so that we could integrate other patterns as well, but with this amount of time left, this *works*
        while (myEnemy.isMoving && currentDist > minDist && currentDist < maxDist)
        {
            currentDist = Vector3.Distance(transform.position, targetEnemy.transform.position);

            Vector3 targetPos = targetEnemy.transform.position;

            float distX = transform.position.x - targetPos.x;
            float distY = transform.position.z - targetPos.z;


            if (transform.position.x < targetPos.x && (distX < .5f)) _moveDirection.x = 1;
            else if (transform.position.x > targetPos.x && (distX > .5f)) _moveDirection.x = -1;
            else _moveDirection.x = 0;

            if (transform.position.z < targetPos.z && (distY < .5f)) _moveDirection.y = 1;
            else if (transform.position.z > targetPos.z && (distY > .5f)) _moveDirection.y = -1;
            else _moveDirection.y = 0;


            float rotateBy = 0f;

            if (_moveDirection.y < 0.1f)
                rotateBy = 180;
            else if (_moveDirection.y > 0.1f)
                rotateBy = 0;

            if (((transform.position.x - targetPos.x > 0.5f) || (transform.position.x - targetPos.x < -0.5f)) && ((transform.position.z - targetPos.z > 0.5f) || (transform.position.z - targetPos.z < -0.5f)))
            {
                if (_moveDirection.x < 0)
                    rotateBy = (-rotateBy - 90) / 2;
                else if (_moveDirection.x > 0)
                    rotateBy = (rotateBy + 90) / 2;
            }
            else if (!((_moveDirection.y > 0.5f) || (_moveDirection.y < -0.5f)))
            {
                if (_moveDirection.x < 0f)
                    rotateBy = -90;
                else if (_moveDirection.x > 0f)
                    rotateBy = 90;
            }

            Vector3 temp = transform.rotation.eulerAngles;
            temp.y = rotateBy;
            transform.rotation = Quaternion.Euler(temp);
            transform.position += new Vector3(_moveDirection.x * _moveSpeed, 0, _moveDirection.y * _moveSpeed);
            yield return new WaitForSeconds(.1f);

            //now that we've moved, there's a 1/30 chance to blink
            int randInt = Random.Range(0, 31);
            if (randInt == 0)
            {
                //pick a random spot in the unit circle
                Vector2 rand = Random.insideUnitCircle * 5f;
                this.transform.position += new Vector3(rand.x, 0, rand.y);
                //slight delay after blinking
                yield return new WaitForSeconds(0.75f);
            }
        }
        myEnemy.isMoving = false;
    }
}
