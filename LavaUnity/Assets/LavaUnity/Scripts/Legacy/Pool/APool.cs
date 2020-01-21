using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APool<T> where T : IPoolable
{
    Stack<T> NotUsing;

    int size;
    int curSize;//optimize speed
    T prefab;
    Transform root;

    public APool(T prefab, int size, Transform root = null, bool hideInEditor = false)
    {
        this.root = root;
        this.size = size;
        curSize = size;
        this.prefab = prefab;

        NotUsing = new Stack<T>(size);

        for (int i = 0; i < size; ++i)
        {
            T obj;
            if (root == null)
            {
                obj = GameObject.Instantiate<GameObject>(prefab.GetGameObject()).GetComponent<T>();
            }
            else
            {
                obj = GameObject.Instantiate<GameObject>(prefab.GetGameObject(), root, false).GetComponent<T>();
            }
#if UNITY_EDITOR
            if (hideInEditor)
            {
                obj.GetGameObject().hideFlags = HideFlags.HideInHierarchy;
            }
#endif
            obj.Init();
            NotUsing.Push(obj);
        }
    }

    public T GetObject()
    {
        if (curSize > 0)
        {
            curSize--;
            T obj = NotUsing.Pop();
            obj.OnOutOfPool();
            return obj;
        }
        else
        {
            T obj;
            if (root == null)
            {
                obj = GameObject.Instantiate<GameObject>(prefab.GetGameObject()).GetComponent<T>();
            }
            else
            {
                obj = GameObject.Instantiate<GameObject>(prefab.GetGameObject(), root, false).GetComponent<T>();
            }
            obj.Init();
            obj.OnReturnToPool();

            size++;

            obj.OnOutOfPool();
            return obj;
        }
    }

    public void ReturnObject(T obj)
    {
        curSize++;
        obj.OnReturnToPool();

        NotUsing.Push(obj);
    }
}


public interface IPoolable
{
    void OnReturnToPool();
    void OnOutOfPool();
    void Init();
    GameObject GetGameObject();
}