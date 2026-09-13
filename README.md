# study-git

Küçük bir görev listesi (to-do) komut satırı aracı (.NET / C#).

Bu repo, gerçek bir takım ortamında git kullanmayı pratik etmek için oluşturuldu:
branch stratejileri, merge conflict çözümü, rebase/history düzenleme ve
PR/code review akışı üzerinde çalışılıyor.

## Kullanım

```bash
dotnet run add "Sütü al"
dotnet run list
dotnet run done 1
```

## Geliştirme

- `main` her zaman çalışır durumda tutulur.
- Yeni işler `feature/<kısa-isim>` branch'lerinde yapılır.
- Değişiklikler PR ile `main`'e alınır.
