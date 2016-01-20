using System.IO;
using System.Text;
using LGame.LDebug;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;
using System.Collections;

namespace LGame.LEditor
{

    /***
     * 
     * 
     * 创建模板脚本
     * 
     */

    public class SLCreateScripts : MonoBehaviour
    {

        /// <summary>
        /// 得到创建脚本的文件夹
        /// </summary>
        /// <returns></returns>
        public static string CreateScriptFolder()
        {
            string path = "Assets";
            foreach (Object obj in Selection.GetFiltered(typeof(Object), SelectionMode.Assets))
            {
                path = AssetDatabase.GetAssetPath(obj);
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    path = Path.GetDirectoryName(path);
                    break;
                }
            }
            return path;
        }

        /// <summary>
        /// 创建lua 脚本
        /// </summary>
        [MenuItem("Assets/Create/Lua Script", false, 80)]
        public static void CreatLuaScript()
        {
            string folderPath = CreateScriptFolder();
            if (string.IsNullOrEmpty(folderPath)) return;
            SLDebugHelper.WriteError(folderPath);

            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
                ScriptableObject.CreateInstance<CreateLuaScriptAction>(),
                folderPath + "/New Lua.lua",
                null,
                "");
        }

    }

    /// <summary>
    /// 创建lua 脚本模板
    /// </summary>
    public class CreateLuaScriptAction : EndNameEditAction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="pathName">创建路径</param>
        /// <param name="resourceFile">模板文件路径</param>
        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            Object o = CreateTemplateScripts(pathName, resourceFile);
            ProjectWindowUtil.ShowCreatedAsset(o);
        }

        /// <summary>
        /// 创建模板脚本
        /// </summary>
        /// <param name="pathName"></param>
        /// <param name="resourceFile"></param>
        /// <returns></returns>
        internal static Object CreateTemplateScripts(string pathName, string resourceFile)
        {
            string fullPath = Path.GetFullPath(pathName);
            string fileName = Path.GetFileNameWithoutExtension(fullPath);
            SLDebugHelper.WriteError(fileName);
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("--region cretate new lua File");
            builder.AppendLine(string.Format("--Lua FileName {0}", fileName));
            builder.AppendLine("--Start Date\n\n");
            // todo: 增加实际内容
            builder.AppendLine("--endregion");

            string scriptContent = builder.ToString();
            StreamWriter streamWriter = new StreamWriter(fullPath);
            streamWriter.Write(scriptContent);
            streamWriter.Close();

            AssetDatabase.ImportAsset(pathName);
            return AssetDatabase.LoadAssetAtPath(pathName, typeof(Object));
        }

    }
}
