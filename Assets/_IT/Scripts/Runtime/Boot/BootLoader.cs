using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using IT.Utils;

namespace IT.Boot
{
    public class BootLoader : MonoBehaviour
    {
        // TODO: Load scenes from Addressables or SceneReference
        [SerializeField] private int[] m_persistentScenesID;
        [SerializeField] private int m_mainSceneID;

        private async void Start() => await Boot();

        private async Awaitable Boot()
        {
            Scene currentScene = SceneManager.GetActiveScene();

            foreach (int persistentSceneID in m_persistentScenesID)
            {
                await SceneManager.LoadSceneAsync(persistentSceneID, LoadSceneMode.Additive).ToTask();
            }

            Destroy(Camera.main?.gameObject);

            Scene newCurrentScene = await SceneManager.LoadSceneAsync(m_mainSceneID, LoadSceneMode.Additive).ToTask(m_mainSceneID);
            SceneManager.SetActiveScene(newCurrentScene);

            await SceneManager.UnloadSceneAsync(currentScene).ToTask();
        }
    }
}
