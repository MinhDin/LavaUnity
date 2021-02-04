using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APool<T> where T : IPoolable
{
    Queue<T> NotUsing;

    int curSize;//optimize speed
    T prefab;
    Transform root;

    public APool(T prefab, int size, Transform root = null, bool hideInEditor = false)
    {
        this.root = root;
        curSize = size;
        this.prefab = prefab;

        NotUsing = new Queue<T>(size);

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
            obj.IsInPool = true;
            obj.Init();
            obj.GetGameObject().SetActive(false);
            NotUsing.Enqueue(obj);
        }
    }

    public T GetObject()
    {
        if (curSize > 0)
        {
            curSize--;
            T obj = NotUsing.Dequeue();

            obj.IsInPool = false;
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

            //curSize++;
            obj.OnOutOfPool();
            return obj;
        }
    }

    public void ReturnObject(T obj)
    {
        if (!obj.IsInPool)
        {
            curSize++;
            obj.OnReturnToPool();
            obj.IsInPool = true;
            NotUsing.Enqueue(obj);
        }
    }
}


public interface IPoolable
{
    bool IsInPool { get; set; }
    void OnReturnToPool();
    void OnOutOfPool();
    void Init();
    GameObject GetGameObject();
}