using GamePlay;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [Header("方向")]
    [SerializeField]
    public PlatformDirector platformDirector;

    [Header("开始点（相对于自身）")]
    [SerializeField]
    public float startValue;
    [Header("结束点（相对于自身）")]
    [SerializeField]
    public float endValue;
    [Header("移动速度")]
    [SerializeField]
    public float speed;
    [Header("开始运动时先往哪走")]
    [SerializeField]
    public StartTarget startTarget;

    [Header("到达一侧后等待时间")]
    [SerializeField]
    public float waitTIme;

    private void OnValidate()
    {
        if (!Application.isPlaying)
            return;

        InitValidate();
    }

    Vector3 initPosition;
    Vector3 startPoint;
    Vector3 endPoint;
    PlatformState currentState;

    private void Awake()
    {
        initPosition = transform.position;
        InitValidate();
    }

    private void InitValidate()
    {
        switch (platformDirector)
        {
            case PlatformDirector.垂直:
                startPoint = new Vector3(initPosition.x, initPosition.y + startValue);
                endPoint = new Vector3(initPosition.x, initPosition.y + endValue);
                break;
            case PlatformDirector.水平:
                startPoint = new Vector3(initPosition.x + startValue, initPosition.y);
                endPoint = new Vector3(initPosition.x + endValue, initPosition.y);
                break;
        }

        switch (startTarget)
        {
            case StartTarget.起点:
                currentState = PlatformState.去起点;
                break;
            case StartTarget.终点:
                currentState = PlatformState.去终点;
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case PlatformState.去起点:
            case PlatformState.去终点:
                RunMove();
                break;
            case PlatformState.到达起点:
            case PlatformState.到达终点:
                RunWait();
                break;
        }
    }

    float runTimeWait = 0;
    private void RunWait()
    {
        float deltaTime = Time.fixedDeltaTime;
        runTimeWait += deltaTime;
        if (runTimeWait >= waitTIme)
        {
            runTimeWait = 0;
            switch (currentState)
            {
                case PlatformState.到达起点:
                    currentState = PlatformState.去终点;
                    break;
                case PlatformState.到达终点:
                    currentState = PlatformState.去起点;
                    break;
            }
        }
    }

    private void RunMove()
    {
        float deltaTime = Time.fixedDeltaTime;
        Vector3 curPoint = transform.position;
        Vector3 targetPoint = startPoint;
        switch (currentState)
        {
            case PlatformState.去起点:
                targetPoint = startPoint;
                break;
            case PlatformState.去终点:
                targetPoint = endPoint;
                break;
        }

        Vector3 diff = targetPoint - curPoint;
        float canMoveLen = deltaTime * speed;

        Vector3 newPos = Vector3.zero;
        Vector3 realMove = Vector3.zero;
        if (canMoveLen * canMoveLen > diff.sqrMagnitude)
        {
            //移动到Target，并转状态
            realMove = targetPoint - curPoint;
            if (currentState == PlatformState.去起点)
                currentState = PlatformState.到达起点;
            else
                currentState = PlatformState.到达终点;
            runTimeWait = 0f;
        }
        else
        {
            //没达到，按最大可移动距离移动
            realMove = (diff.normalized * canMoveLen);
        }
        transform.position = curPoint + realMove;

        for (int i = 0; i < connected.Count; i++) 
        {
            connected[i].transform.position += realMove;
        }

    }

    private void OnDrawGizmosSelected()
    {
        MovePlatform com = (MovePlatform)this;
        Vector3 initPosition = com.transform.position;
        Vector3 startPoint = Vector3.zero;
        Vector3 endPoint = Vector3.zero;
        switch (com.platformDirector)
        {
            case PlatformDirector.垂直:
                startPoint = new Vector3(initPosition.x, initPosition.y + com.startValue);
                endPoint = new Vector3(initPosition.x, initPosition.y + com.endValue);
                break;
            case PlatformDirector.水平:
                startPoint = new Vector3(initPosition.x + com.startValue, initPosition.y);
                endPoint = new Vector3(initPosition.x + com.endValue, initPosition.y);
                break;
        }

        Gizmos.DrawLine(initPosition, startPoint);
        Gizmos.DrawLine(initPosition, endPoint);
    }

    List<Character> connected = new List<Character>();

    public void OnConnectCharacter(Character character)
    {
        if (connected.Contains(character)) return;
        connected.Add(character);
    }

    public void OnDisconnectCharacter(Character character)
    {
        connected.Remove(character);
    }
}

public enum PlatformDirector
{
    水平,
    垂直,
}

public enum StartTarget
{
    起点,
    终点,
}

public enum PlatformState
{
    去起点,
    去终点,
    到达起点,
    到达终点,
}