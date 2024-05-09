using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SearchService;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    //use _ for private variables~
    private static T _instance;

    //this is a property! it has getters and setters (but not set for this one)
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    //this makes an empty gameobject in the hierarchy with an appropriate name :3
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    _instance = obj.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            //sets this with a dont destroy on load! helpful!!!
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //the "highlander check"
            Destroy(gameObject);
        }
    }
}
