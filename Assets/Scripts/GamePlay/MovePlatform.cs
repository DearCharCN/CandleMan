using GamePlay;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [Header("����")]
    [SerializeField]
    public PlatformDirector platformDirector;

    [Header("��ʼ�㣨���������")]
    [SerializeField]
    public float startValue;
    [Header("�����㣨���������")]
    [SerializeField]
    public float endValue;
    [Header("�ƶ��ٶ�")]
    [SerializeField]
    public float speed;
    [Header("��ʼ�˶�ʱ��������")]
    [SerializeField]
    public StartTarget startTarget;

    [Header("����һ���ȴ�ʱ��")]
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
            case PlatformDirector.��ֱ:
                startPoint = new Vector3(initPosition.x, initPosition.y + startValue);
                endPoint = new Vector3(initPosition.x, initPosition.y + endValue);
                break;
            case PlatformDirector.ˮƽ:
                startPoint = new Vector3(initPosition.x + startValue, initPosition.y);
                endPoint = new Vector3(initPosition.x + endValue, initPosition.y);
                break;
        }

        switch (startTarget)
        {
            case StartTarget.���:
                currentState = PlatformState.ȥ���;
                break;
            case StartTarget.�յ�:
                currentState = PlatformState.ȥ�յ�;
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case PlatformState.ȥ���:
            case PlatformState.ȥ�յ�:
                RunMove();
                break;
            case PlatformState.�������:
            case PlatformState.�����յ�:
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
                case PlatformState.�������:
                    currentState = PlatformState.ȥ�յ�;
                    break;
                case PlatformState.�����յ�:
                    currentState = PlatformState.ȥ���;
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
            case PlatformState.ȥ���:
                targetPoint = startPoint;
                break;
            case PlatformState.ȥ�յ�:
                targetPoint = endPoint;
                break;
        }

        Vector3 diff = targetPoint - curPoint;
        float canMoveLen = deltaTime * speed;

        Vector3 newPos = Vector3.zero;
        Vector3 realMove = Vector3.zero;
        if (canMoveLen * canMoveLen > diff.sqrMagnitude)
        {
            //�ƶ���Target����ת״̬
            realMove = targetPoint - curPoint;
            if (currentState == PlatformState.ȥ���)
                currentState = PlatformState.�������;
            else
                currentState = PlatformState.�����յ�;
            runTimeWait = 0f;
        }
        else
        {
            //û�ﵽ���������ƶ������ƶ�
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
            case PlatformDirector.��ֱ:
                startPoint = new Vector3(initPosition.x, initPosition.y + com.startValue);
                endPoint = new Vector3(initPosition.x, initPosition.y + com.endValue);
                break;
            case PlatformDirector.ˮƽ:
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
    ˮƽ,
    ��ֱ,
}

public enum StartTarget
{
    ���,
    �յ�,
}

public enum PlatformState
{
    ȥ���,
    ȥ�յ�,
    �������,
    �����յ�,
}