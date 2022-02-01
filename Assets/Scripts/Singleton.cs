using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    //initialisation de l'instance
    protected static T m_instance = null;

    [SerializeField, Tooltip("continue a exister quand on change de scenes")]
    private bool m_dontDestroyOnLoad = true;
    
    public static T Instance
    {
        get
        {
            // sauveguarde this dans m_instance si la place est libre sinon selfdestruct
            if (m_instance == null)
            {
                CreateInstance();
            }
            return m_instance;
        }
    }

    protected static void CreateInstance()
    {
        m_instance = FindObjectOfType<T>();
        if (m_instance != null)
        {
            (m_instance as Singleton<T>).SetSingleton(true);
            return;
        }

        GameObject go = new GameObject();
        m_instance = go.AddComponent<T>();

        (m_instance as Singleton<T>).SetSingleton();
    }

    protected virtual string GetSingletonName()
    {
        return "DefaultSingleton";
    }

    protected virtual void SetSingleton(bool p_rename = false)
    {
        if (m_dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
        if(p_rename) gameObject.name = GetSingletonName();
    }

    private void Awake()
    {
        if (m_instance != null)
        {
            if(m_instance != this)
            {
                Destroy(this.gameObject);
                Debug.LogWarning("Il y a plusieurs Singleton, c'est pas bien ! :(");
            }

            return;
        }
        CreateInstance();
    }
}