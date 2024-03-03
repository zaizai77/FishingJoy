using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;
using System.IO;

public class HotFixScripts : MonoBehaviour
{
    LuaEnv env;

    public static Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>();

    private void Awake()
    {
        env = new LuaEnv();
        env.AddLoader(MyLoader);
        env.DoString("require 'mfish'");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private byte[] MyLoader(ref string filePath)
    {
        //¾ø¶ÔÂ·¾¶
        string absPath = @"D:\unity\FishingJoy\UnityPackageManager\" + filePath + ".lua.txt";
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(absPath));
    }

    private void OnDisable()
    {
        env.DoString("require'mfishDispose'");
    }

    private void OnDestroy()
    {
        env.Dispose();
    }

    [LuaCallCSharp]
    public static void LoadResource(string resName,string filePath)
    {
        AssetBundle ab = AssetBundle.LoadFromFile(@"D:\unity\FishingJoy\AssetBundles\"+filePath);
        GameObject gameObject = ab.LoadAsset<GameObject>(resName);
        prefabDict.Add(resName, gameObject);
    }

    [LuaCallCSharp]
    public static GameObject GetGameObject(string goName)
    {
        return prefabDict[goName];
    }
}
