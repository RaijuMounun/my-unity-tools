using UnityEngine;
using UnityEngine.UI;
using CharacterCreation;

/// <summary>
/// Example usage of AnimatedPanels for character creation
/// </summary>
public class AnimatedPanelsExample : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AnimatedPanels panelController;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button[] panelButtons; // Direct panel navigation buttons

    [Header("UI Elements")]
    [SerializeField] private Text currentPanelText;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private GameObject characterSummaryPanel; // Final panel

    void Start()
    {
        SetupButtons();
        SetupEvents();
        UpdateUI();
    }

    private void SetupButtons()
    {
        if (nextButton != null)
            nextButton.onClick.AddListener(() => panelController.GoNext());

        if (backButton != null)
            backButton.onClick.AddListener(() => panelController.GoBack());

        // Setup direct panel navigation buttons
        if (panelButtons != null)
        {
            for (int i = 0; i < panelButtons.Length; i++)
            {
                int panelIndex = i; // Capture the index
                if (panelButtons[i] != null)
                    panelButtons[i].onClick.AddListener(() => panelController.GoToPanel(panelIndex));
            }
        }
    }

    private void SetupEvents()
    {
        if (panelController == null) return;

        // Panel değiştiğinde UI'ı güncelle
        panelController.onPanelChanged.AddListener(OnPanelChanged);

        // Son panele ulaşıldığında karakter özetini göster
        panelController.onLastPanelReached.AddListener(OnLastPanelReached);

        // İlk panele ulaşıldığında
        panelController.onFirstPanelReached.AddListener(OnFirstPanelReached);

        // Son panelde ileri tuşuna basıldığında
        panelController.onNextRequested.AddListener(OnNextRequested);

        // İlk panelde geri tuşuna basıldığında
        panelController.onBackRequested.AddListener(OnBackRequested);

        // Animasyon başladığında butonları devre dışı bırak
        panelController.onTransitionStart.AddListener(OnTransitionStart);

        // Animasyon bittiğinde butonları tekrar aktif et
        panelController.onTransitionComplete.AddListener(OnTransitionComplete);
    }

    private void OnPanelChanged(int panelIndex)
    {
        Debug.Log($"Panel changed to: {panelIndex}");
        UpdateUI();
    }

    private void OnLastPanelReached()
    {
        Debug.Log("Reached last panel - Character creation almost complete!");

        // Son panelde özel UI göster
        if (nextButton != null)
        {
            nextButton.GetComponentInChildren<Text>().text = "Create Character";
        }
    }

    private void OnFirstPanelReached()
    {
        Debug.Log("Reached first panel");

        // İlk panelde özel UI göster
        if (backButton != null)
        {
            backButton.GetComponentInChildren<Text>().text = "Back to Menu";
        }
    }

    private void OnNextRequested()
    {
        Debug.Log("Next requested on last panel - Show character summary!");

        // Karakter özet panelini göster
        if (characterSummaryPanel != null)
        {
            characterSummaryPanel.SetActive(true);
        }

        // Burada karakter oluşturma işlemini başlatabilirsiniz
        CreateCharacter();
    }

    private void OnBackRequested()
    {
        Debug.Log("Back requested on first panel - Return to main menu!");

        // Ana menüye dön
        // SceneManager.LoadScene("MainMenu");
    }

    private void OnTransitionStart()
    {
        // Animasyon sırasında butonları devre dışı bırak
        SetButtonsInteractable(false);
    }

    private void OnTransitionComplete()
    {
        // Animasyon bittiğinde butonları tekrar aktif et
        SetButtonsInteractable(true);
    }

    private void UpdateUI()
    {
        if (panelController == null) return;

        // Panel numarasını göster
        if (currentPanelText != null)
        {
            currentPanelText.text = $"Panel {panelController.CurrentIndex + 1} / {panelController.PanelCount}";
        }

        // Progress slider'ı güncelle
        if (progressSlider != null)
        {
            progressSlider.value = (float)panelController.CurrentIndex / (panelController.PanelCount - 1);
        }

        // Buton durumlarını güncelle
        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        if (nextButton != null)
        {
            nextButton.interactable = !panelController.IsOnLastPanel || panelController.loopNavigation;
        }

        if (backButton != null)
        {
            backButton.interactable = !panelController.IsOnFirstPanel || panelController.allowBackOnFirstPanel;
        }
    }

    private void SetButtonsInteractable(bool interactable)
    {
        if (nextButton != null)
            nextButton.interactable = interactable;

        if (backButton != null)
            backButton.interactable = interactable;

        if (panelButtons != null)
        {
            foreach (var button in panelButtons)
            {
                if (button != null)
                    button.interactable = interactable;
            }
        }
    }

    private void CreateCharacter()
    {
        Debug.Log("Creating character...");

        // Burada karakter oluşturma işlemlerini yapın
        // Örnek: CharacterData data = CollectCharacterData();
        // CharacterManager.Instance.CreateCharacter(data);
    }

    // Public methods for external access
    public void ResetToFirstPanel()
    {
        panelController?.ResetToFirst();
    }

    public void GoToSpecificPanel(int panelIndex)
    {
        panelController?.GoToPanel(panelIndex);
    }

    public int GetCurrentPanelIndex()
    {
        return panelController?.CurrentIndex ?? 0;
    }

    public bool IsOnLastPanel()
    {
        return panelController?.IsOnLastPanel ?? false;
    }
}