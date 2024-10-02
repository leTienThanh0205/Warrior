using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingleft;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        initScale = enemy.localScale;
    }
    private void OnDisable()
    {
        anim.SetBool(AnimationStrings.movingEnemy, false);
        //anim.SetBool("moving", false);
    }
    private void Update()
    {
        if (movingleft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveDirection(1);
            else
                DirectionChange();
        }
    }
    private void DirectionChange()
    {
        anim.SetBool(AnimationStrings.movingEnemy, false);
        idleTimer += Time.deltaTime;
        if(idleTimer > idleDuration)
            movingleft = !movingleft;
    }
    private void MoveDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool(AnimationStrings.movingEnemy,true);
        //make enemy face direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);
        //Move in that direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }
}
