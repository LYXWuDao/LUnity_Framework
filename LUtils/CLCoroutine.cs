using LGame.LBehaviour;
using LGame.LCommon;

namespace LGame.LUtils
{

    /***
     * 
     * 
     * 开辟一个调用协成的类
     * 
     * 
     */

    public class CLCoroutine : ALBehaviour
    {

        private static CLCoroutine _instance;

        private static object _lock = new object();

        public static CLCoroutine Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = SLToolsHelper.Create<CLCoroutine>("_game coroutine");
                        }
                    }
                }
                return _instance;
            }
        }

    }

}
