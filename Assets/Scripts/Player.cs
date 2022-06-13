using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Vector2 dir;
    public Vector2 position;

    public Vector2 Position
    {
        get
        {
            return position;
        }
        set
        {
            position = value;
            this.transform.DOMove(new Vector3(position.x, this.transform.position.y, position.y), moveTime).SetEase(Ease.InOutBack);
        }
    }

    public float moveTime = 0.1f;

    public Transform head;

    public static Action OnPlayerDead;
    public static Action OnPlayerMove;

    void Start()
    {
        
    }

    public void SetDirection(Vector2 dir)
    {
        this.dir = dir;
    }

    public void SetPosition(Vector2 pos)
    {
        this.position = pos;
    }

    public void Move(Vector2 move)
    {
        Position += dir * move.y;
        OnPlayerMove?.Invoke();
    }

    public void Turn(float angle)
    {
        dir = Rotate(dir, angle * Mathf.Deg2Rad);

        head.LookAt(new Vector3(this.transform.position.x + dir.x, head.transform.position.y,this.transform.position.z + dir.y));
    }

    public static Vector2 Rotate(Vector2 v, float delta)
    {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }
}
    
     