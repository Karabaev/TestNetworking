using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Aboba.New.StateMachine
{
  public class SceneService
  {
    public UniTask OpenSceneAsync(string sceneName)
    {
      return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single).ToUniTask();
    }
  }
}