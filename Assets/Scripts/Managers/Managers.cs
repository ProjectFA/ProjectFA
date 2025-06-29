using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance = null;
    static Managers Inst
    {
        get
        {
            if (s_instance == null)
                Init();
            return s_instance;
        }
    }

    //////////////////////////////////////////////
    /// Managers

    DataManager _data = new DataManager();
    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();

    public static DataManager Data { get { return Inst._data; } }
    public static InputManager Input { get { return Inst._input; } }
    public static ResourceManager Resource { get { return Inst._resource; } }
    public static SceneManagerEx Scene { get { return Inst._scene; } }
    public static SoundManager Sound { get { return Inst._sound; } }
    public static UIManager UI { get { return Inst._ui; } }

    //////////////////////////////////////////////


    void Awake()
    {
        Init();
    }

    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        GameObject obj = GameObject.Find("@Managers");

        if (obj == null)
        {
            obj = new GameObject { name = "@Managers" };
            obj.AddComponent<Managers>();
        }

        DontDestroyOnLoad(obj);
        s_instance = obj.GetComponent<Managers>();

        s_instance._sound.Init();
        s_instance._data.Init();
    }

    public static void Clear()
    {
        Sound.Clear();
        Input.Clear();
        Scene.Clear();
        UI.Clear();
    }
}
