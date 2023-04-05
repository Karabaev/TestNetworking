using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Aboba.New.StateMachine
{
  public class SceneService
  {
    public UniTask OpenSceneAsync(string sceneName)
    {
      if(SceneManager.GetActiveScene().name == sceneName)
        return UniTask.CompletedTask;

      return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single).ToUniTask();
    }
  }
}