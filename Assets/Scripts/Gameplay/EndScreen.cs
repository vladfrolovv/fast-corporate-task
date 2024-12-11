using DataProxies;
using DG.Tweening;
using TMPro;
using Transitions;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;
namespace Gameplay
{
    public class EndScreen : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI _timeSurvivedText;
        [SerializeField] private Button _restartButton;

        [SerializeField] private CanvasGroup _rootCanvasGroup;
        [SerializeField] private CanvasGroup _endScreenCanvasGroup;

        private GameManager _gameManager;
        private Transition _transition;
        private HealthDataProxy _healthDataProxy;

        [Inject]
        public void Construct(GameManager gameManager, Transition transition, HealthDataProxy healthDataProxy)
        {
            _gameManager = gameManager;
            _transition = transition;
            _healthDataProxy = healthDataProxy;
        }

        public void Show()
        {
            gameObject.SetActive(true);

            _rootCanvasGroup.DOFade(0f, .32f);
            _endScreenCanvasGroup.DOFade(1f, .32f);

            _timeSurvivedText.text = $"Time survived: {_gameManager.TimeSinceStart:mm\\:ss}";
        }

        protected void Awake()
        {
            _restartButton.OnClickAsObservable().Subscribe(delegate
            {
                _healthDataProxy.Reset();

                _restartButton.interactable = false;
                _transition.FadeIn(delegate
                {
                    SceneManager.LoadSceneAsync(1).completed +=
                        delegate(AsyncOperation operation)
                        {
                            _transition.FadeOut();
                        };
                });
            }).AddTo(this);

            _endScreenCanvasGroup.alpha = 0f;
            gameObject.SetActive(false);
        }

        protected void OnDestroy()
        {
            _restartButton.onClick.RemoveAllListeners();
        }

    }
}
