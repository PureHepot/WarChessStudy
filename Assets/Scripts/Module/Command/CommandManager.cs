using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���������
/// </summary>
public class CommandManager
{
    private Queue<BaseCommand> willDoCommandQueue;//��Ҫִ�е��������
    private Stack<BaseCommand> unDoStack;// �������� ջ
    public BaseCommand current;//��ǰ

    public CommandManager()
    {
        willDoCommandQueue = new Queue<BaseCommand>();
        unDoStack = new Stack<BaseCommand>();
    }

    public bool isRunningCommand
    {
        get
        {
            return current != null;
        }
    }

    public void AddCommand(BaseCommand command)
    {
        willDoCommandQueue.Enqueue(command);
        unDoStack.Push(command);
    }

    public void Update(float dt)
    {
        if (current == null)
        {
            if (willDoCommandQueue.Count > 0)
            {
                current = willDoCommandQueue.Dequeue();
                current.Do();//ִ��
            }
        }
        else
        {
            if (current.Update(dt) == true)
            {
                current = null;
            }
        }
    }

    public void Clear()
    {
        willDoCommandQueue?.Clear();
        unDoStack.Clear();
        current = null;
    }

    //������һ������
    public void Undo()
    {
        if (unDoStack.Count > 0)
        {
            unDoStack.Pop().Undo();
        }
    }

}


