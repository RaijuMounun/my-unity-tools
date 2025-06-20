# AnimatedPanels - Generic Panel Navigation System

Bu sistem, Unity'de çoklu panel geçişleri için generic ve yeniden kullanılabilir bir çözüm sunar. DOTween ile smooth animasyonlar sağlar.

## 🚀 Özellikler

- ✅ **Generic Yapı**: Herhangi bir UI projesinde kullanılabilir
- ✅ **Event-Driven**: UnityEvent sistemi ile esnek entegrasyon
- ✅ **Smooth Animasyonlar**: DOTween ile profesyonel geçişler
- ✅ **Loop Navigation**: İsteğe bağlı döngüsel navigasyon
- ✅ **Direct Navigation**: Belirli panele direkt geçiş
- ✅ **Progress Tracking**: Panel durumu takibi
- ✅ **Debug Tools**: Context menu ile test araçları

## 📋 Kurulum

### 1. Script Ekleme

```csharp
using CharacterCreation;

public class YourScript : MonoBehaviour
{
    [SerializeField] private AnimatedPanels panelController;
}
```

### 2. Inspector Ayarları

- **Panels**: RectTransform array'ine panellerinizi sürükleyin
- **Transition Duration**: Animasyon süresi (varsayılan: 0.5f)
- **Transition Ease**: Animasyon eğrisi (varsayılan: InOutSine)
- **Loop Navigation**: Son panelden sonra ilk panele dönüş
- **Allow Back On First Panel**: İlk panelde geri tuşu çalışsın mı?

## 🎯 Temel Kullanım

### Basit Navigasyon

```csharp
// İleri git
panelController.GoNext();

// Geri git
panelController.GoBack();

// Belirli panele git
panelController.GoToPanel(2);

// İlk panele dön
panelController.ResetToFirst();
```

### Event Sistemi

```csharp
// Panel değiştiğinde
panelController.onPanelChanged.AddListener((index) => {
    Debug.Log($"Panel {index} aktif");
});

// Son panele ulaşıldığında
panelController.onLastPanelReached.AddListener(() => {
    Debug.Log("Son panel!");
});

// İleri tuşuna basıldığında (son paneldeyse)
panelController.onNextRequested.AddListener(() => {
    // Karakter oluştur, özet göster vs.
});
```

## 📊 Properties

```csharp
int currentIndex = panelController.CurrentIndex;        // Mevcut panel
int totalPanels = panelController.PanelCount;          // Toplam panel sayısı
bool isTransitioning = panelController.IsTransitioning; // Animasyon durumu
bool isFirst = panelController.IsOnFirstPanel;         // İlk panelde mi?
bool isLast = panelController.IsOnLastPanel;           // Son panelde mi?
```

## 🎨 Event Listesi

| Event                  | Açıklama                                   | Parametre        |
| ---------------------- | ------------------------------------------ | ---------------- |
| `onPanelChanged`       | Panel değiştiğinde                         | `int panelIndex` |
| `onFirstPanelReached`  | İlk panele ulaşıldığında                   | -                |
| `onLastPanelReached`   | Son panele ulaşıldığında                   | -                |
| `onNextRequested`      | İleri tuşuna basıldığında (son paneldeyse) | -                |
| `onBackRequested`      | Geri tuşuna basıldığında (ilk paneldeyse)  | -                |
| `onTransitionStart`    | Animasyon başladığında                     | -                |
| `onTransitionComplete` | Animasyon bittiğinde                       | -                |

## 🔧 Gelişmiş Kullanım

### Buton Durumu Yönetimi

```csharp
private void UpdateButtonStates()
{
    nextButton.interactable = !panelController.IsOnLastPanel;
    backButton.interactable = !panelController.IsOnFirstPanel;
}
```

### Progress Bar

```csharp
private void UpdateProgressBar()
{
    float progress = (float)panelController.CurrentIndex / (panelController.PanelCount - 1);
    progressSlider.value = progress;
}
```

### Animasyon Sırasında UI Kilitleme

```csharp
panelController.onTransitionStart.AddListener(() => {
    SetAllButtonsInteractable(false);
});

panelController.onTransitionComplete.AddListener(() => {
    SetAllButtonsInteractable(true);
});
```

## 🎮 Örnek Senaryolar

### Karakter Oluşturma

```csharp
panelController.onNextRequested.AddListener(() => {
    // Son panelde ileri tuşuna basıldığında
    ShowCharacterSummary();
    CreateCharacter();
});
```

### Ayarlar Menüsü

```csharp
panelController.onBackRequested.AddListener(() => {
    // İlk panelde geri tuşuna basıldığında
    SaveSettings();
    ReturnToMainMenu();
});
```

### Tutorial Sistemi

```csharp
panelController.onPanelChanged.AddListener((index) => {
    ShowTutorialStep(index);
    UpdateTutorialProgress(index);
});
```

## 🐛 Debug Araçları

Inspector'da sağ tık → Context Menu:

- **Reset Panels**: Panelleri başlangıç durumuna getir
- **Go Next**: Test için ileri git
- **Go Back**: Test için geri git

## 📝 Notlar

- Panellerin `RectTransform` bileşeni olmalı
- Panel genişlikleri eşit olmalı (farklıysa animasyon bozulabilir)
- Event'ler null-safe, boş bırakılabilir
- Namespace `CharacterCreation` kullanılıyor

## 🔄 Yeniden Kullanım

Bu sistemi başka projelerde kullanmak için:

1. `AnimatedPanels.cs` dosyasını kopyalayın
2. `CharacterCreation` namespace'ini değiştirin (isteğe bağlı)
3. DOTween paketinin yüklü olduğundan emin olun
4. Panellerinizi atayın ve event'leri bağlayın

---

**Versiyon**: 1.0  
**Unity**: 2021.3+  
**Dependencies**: DOTween
