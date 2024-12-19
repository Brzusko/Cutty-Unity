using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IT.Utils
{
    public static class AsyncOperationExtensions
    {
        public static Task ToTask(this AsyncOperation asyncOperation)
        {
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
            asyncOperation.completed += _ => taskCompletionSource.SetResult(true);
            return taskCompletionSource.Task;
        }

        public static Task<Scene> ToTask(this AsyncOperation asyncOperation, int sceneID)
        {
            TaskCompletionSource<Scene> taskCompletionSource = new TaskCompletionSource<Scene>();
            asyncOperation.completed += _ => taskCompletionSource.SetResult(SceneManager.GetSceneByBuildIndex(sceneID));
            return taskCompletionSource.Task;
        }
    }
}
