using System;
using Transitions;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Menu
{
    [RequireComponent(typeof(Button))]
    public class PlayButton : MonoBehaviour, IDisposable
    {

        private CompositeDisposable _compositeDisposable = new ();

        private Transition _transition;

        [Inject]
        public void Construct(Transition transition)
        {
            _transition = transition;
        }

        private void Awake()
        {
            Button button = GetComponent<Button>();
            button.OnClickAsObservable().Subscribe(delegate
            {
                button.interactable = false;
                _transition.FadeIn(delegate
                {
                    SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive).completed +=
                        delegate(AsyncOperation operation)
                        {
                            _transition.FadeOut();
                            SceneManager.UnloadSceneAsync(0);
                        };
                });
            }).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

    }
}
