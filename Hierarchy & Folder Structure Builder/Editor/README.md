# Hierarchy & Folder Structure Builder

Unity için geliştirilen bu editor tool, projelerinizde tutarlı bir hierarchy ve folder yapısı oluşturmanıza yardımcı olur.


## Özellikler

- Önceden tanımlanmış hierarchy template'leri ile hızlıca hierarchy yapısı oluşturma
- Önceden tanımlanmış folder template'leri ile proje klasör yapısı oluşturma
- Kendi özel hierarchy ve folder template'lerinizi oluşturma ve kaydetme
- Hızlı şablonlar ile yaygın klasör yapılarını tek tıkla ekleme

## Kullanım

> **⚠️ Önemli Uyarı:** Bu tool, projenizin yapısında kalıcı değişiklikler yapar. Kullanmadan önce önlem alın:
> 
> - Versiyon kontrol sistemi (Git, SVN, vb.) kullanıyorsanız (ki kesinlikle kullanmalısınız), tool'u kullanmadan önce izole bir ortam (yeni bir branch gibi) yarattığınızdan emin olun.
> - Önemli projelerinizde kullanmadan önce bir yedek alın veya test projesi üzerinde deneyin.

### Tool'ları Açma

Unity Editor'da üst menüden aşağıdaki seçeneklere tıklayarak tool'ları açabilirsiniz:

- **Tools > Hierarchy & Folder Structure > Open Builder Tools**: Her iki tool'u da yan yana açar
- **Tools > Hierarchy & Folder Structure > Hierarchy Builder**: Sadece Hierarchy Builder'ı açar
- **Tools > Hierarchy & Folder Structure > Folder Structure Builder**: Sadece Folder Structure Builder'ı açar

### Hierarchy Yapısı Oluşturma

1. Hierarchy Builder penceresinden bir template seçin
2. Template'in önizlemesini kontrol edin
3. "Create Hierarchy Structure" butonuna tıklayarak seçili template'i açık olan sahneye uygulayın

### Folder Yapısı Oluşturma

1. Folder Structure Builder penceresinden bir template seçin
2. Template'in önizlemesini kontrol edin
3. "Create Folder Structure" butonuna tıklayarak seçili template'i projenize uygulayın

### Yeni Template Oluşturma

#### Hierarchy Template Oluşturma

1. Hierarchy Builder penceresinden "Create New Hierarchy Template" butonuna tıklayın
2. Template'e bir isim verin
3. "Add Root Node" butonu ile root node'lar ekleyin
4. Her node için "Add Child Node" butonu ile çocuk node'lar ekleyin
5. "Save Template" butonuna tıklayarak template'i kaydedin

#### Folder Template Oluşturma

1. Folder Structure Builder penceresinden "Create New Folder Template" butonuna tıklayın
2. Template'e bir isim verin
3. Klasör yollarını ekleyin (Assets klasörüne göre relatif olarak, örn. "Scripts/Managers")
4. Hızlı şablonlar ile yaygın klasör yapılarını ekleyebilirsiniz
5. "Save Template" butonuna tıklayarak template'i kaydedin

## Notlar

- Oluşturulan tüm template'ler "Assets/Editor/Templates" klasöründe JSON formatında saklanır
- Önceden tanımlı template'ler silinemez, ancak kendi oluşturduğunuz template'leri silebilirsiniz

## Gereksinimler

- Unity 6'nın herhangi bir sürümünde çalışmalı. 