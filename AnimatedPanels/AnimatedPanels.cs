using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

namespace CharacterCreation
{
    /// <summary>
    /// Generic animated panel switching system with event-driven architecture.
    /// Can be used for any multi-panel UI navigation.
    /// </summary>
    public class AnimatedPanels : MonoBehaviour
    {
        [Header("Panel Settings")]
        public RectTransform[] panels;
        [SerializeField] private float transitionDuration = 0.5f;
        [SerializeField] private Ease transitionEase = Ease.InOutSine;

        [Header("Navigation Settings")]
        [SerializeField, Tooltip("If true, the navigation will loop back to the first panel after the last panel.")]
        public bool loopNavigation = false;
        [SerializeField, Tooltip("If true, the back button will work on the first panel.")]
        public bool allowBackOnFirstPanel = false;

        [Header("Events")]
        [SerializeField] public UnityEvent<int> onPanelChanged; // Panel değiştiğinde çağrılır (panel index'i ile)
        [SerializeField] public UnityEvent onFirstPanelReached; // İlk panele ulaşıldığında
        [SerializeField] public UnityEvent onLastPanelReached; // Son panele ulaşıldığında
        [SerializeField] public UnityEvent onNextRequested; // İleri tuşuna basıldığında (son paneldeyse)
        [SerializeField] public UnityEvent onBackRequested; // Geri tuşuna basıldığında (ilk paneldeyse)
        [SerializeField] public UnityEvent onTransitionStart; // Animasyon başladığında
        [SerializeField] public UnityEvent onTransitionComplete; // Animasyon bittiğinde

        private int currentIndex = 0;
        private bool isTransitioning = false;

        #region Properties
        /// <summary>
        /// Current active panel index
        /// </summary>
        public int CurrentIndex => currentIndex;

        /// <summary>
        /// Total number of panels
        /// </summary>
        public int PanelCount => panels?.Length ?? 0;

        /// <summary>
        /// Is currently transitioning between panels
        /// </summary>
        public bool IsTransitioning => isTransitioning;

        /// <summary>
        /// Is currently on the first panel
        /// </summary>
        public bool IsOnFirstPanel => currentIndex == 0;

        /// <summary>
        /// Is currently on the last panel
        /// </summary>
        public bool IsOnLastPanel => currentIndex == PanelCount - 1;
        #endregion

        #region Unity Lifecycle
        void Start()
        {
            InitializePanels();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Navigate to the next panel
        /// </summary>
        public void GoNext()
        {
            if (isTransitioning) return;

            if (IsOnLastPanel)
            {
                onNextRequested?.Invoke();
                if (!loopNavigation) return;
            }

            int nextIndex = loopNavigation && IsOnLastPanel ? 0 : currentIndex + 1;
            SlidePanels(currentIndex, nextIndex, 1);
            currentIndex = nextIndex;
        }

        /// <summary>
        /// Navigate to the previous panel
        /// </summary>
        public void GoBack()
        {
            if (isTransitioning) return;

            if (IsOnFirstPanel)
            {
                onBackRequested?.Invoke();
                if (!allowBackOnFirstPanel) return;
            }

            int prevIndex = loopNavigation && IsOnFirstPanel ? PanelCount - 1 : currentIndex - 1;
            SlidePanels(currentIndex, prevIndex, -1);
            currentIndex = prevIndex;
        }

        /// <summary>
        /// Navigate to a specific panel by index
        /// </summary>
        /// <param name="targetIndex">Target panel index</param>
        public void GoToPanel(int targetIndex)
        {
            if (isTransitioning || targetIndex < 0 || targetIndex >= PanelCount) return;

            int direction = targetIndex > currentIndex ? 1 : -1;
            SlidePanels(currentIndex, targetIndex, direction);
            currentIndex = targetIndex;
        }

        /// <summary>
        /// Reset to first panel
        /// </summary>
        public void ResetToFirst()
        {
            if (isTransitioning) return;

            currentIndex = 0;
            InitializePanels();
            onPanelChanged?.Invoke(currentIndex);
        }

        /// <summary>
        /// Get panel at specific index
        /// </summary>
        /// <param name="index">Panel index</param>
        /// <returns>RectTransform of the panel</returns>
        public RectTransform GetPanel(int index)
        {
            if (index >= 0 && index < PanelCount)
                return panels[index];
            return null;
        }

        /// <summary>
        /// Get current active panel
        /// </summary>
        /// <returns>RectTransform of current panel</returns>
        public RectTransform GetCurrentPanel()
        {
            return GetPanel(currentIndex);
        }
        #endregion

        #region Private Methods
        private void InitializePanels()
        {
            if (panels == null || panels.Length == 0)
            {
                Debug.LogWarning("AnimatedPanels: No panels assigned!");
                return;
            }

            for (int i = 0; i < panels.Length; i++)
            {
                if (panels[i] != null)
                {
                    float xPosition = (i == 0) ? 0f : panels[i].rect.width;
                    panels[i].anchoredPosition = new Vector2(xPosition, 0f);
                    panels[i].gameObject.SetActive(true);
                }
            }

            onPanelChanged?.Invoke(currentIndex);
        }

        private void SlidePanels(int fromIndex, int toIndex, int direction)
        {
            if (isTransitioning) return;

            isTransitioning = true;
            onTransitionStart?.Invoke();

            RectTransform fromPanel = panels[fromIndex];
            RectTransform toPanel = panels[toIndex];

            if (fromPanel == null || toPanel == null)
            {
                Debug.LogError("AnimatedPanels: Panel reference is null!");
                isTransitioning = false;
                return;
            }

            float panelWidth = fromPanel.rect.width;

            // Set target panel initial position
            toPanel.anchoredPosition = new Vector2(direction * panelWidth, 0f);
            toPanel.gameObject.SetActive(true);

            // Create DOTween sequence
            Sequence sequence = DOTween.Sequence();

            // Move current panel out
            sequence.Join(fromPanel.DOAnchorPosX(-direction * panelWidth, transitionDuration)
                .SetEase(transitionEase));

            // Move target panel in
            sequence.Join(toPanel.DOAnchorPosX(0f, transitionDuration)
                .SetEase(transitionEase));

            // On animation complete
            sequence.OnComplete(() =>
            {
                isTransitioning = false;
                onTransitionComplete?.Invoke();
                onPanelChanged?.Invoke(currentIndex);

                // Trigger specific events
                if (IsOnFirstPanel)
                    onFirstPanelReached?.Invoke();
                else if (IsOnLastPanel)
                    onLastPanelReached?.Invoke();
            });
        }
        #endregion

        #region Debug Methods
        [ContextMenu("Reset Panels")]
        private void ResetPanels()
        {
            currentIndex = 0;
            isTransitioning = false;
            InitializePanels();
        }

        [ContextMenu("Go Next")]
        private void DebugGoNext()
        {
            GoNext();
        }

        [ContextMenu("Go Back")]
        private void DebugGoBack()
        {
            GoBack();
        }
        #endregion
    }
}
