# Как загрузить Unity-проект в GitHub

## Главное

Не загружайте ZIP-архив всего проекта. GitHub ограничивает обычный файл размером 100 MB, а архив содержит папку `Library`, которую Unity создаёт заново на каждом компьютере.

В репозиторий нужны:

- `Assets`;
- `Packages`;
- `ProjectSettings`;
- файлы `.meta`;
- документация проекта.

Не нужны:

- `Library`;
- `Temp`;
- `Logs`;
- `Obj`;
- `Build` и `Builds`;
- архив проекта.

## Вариант через GitHub Desktop

1. Установите [GitHub Desktop](https://desktop.github.com/) и [Git LFS](https://git-lfs.com/).
2. В GitHub Desktop выберите **File → Clone repository**.
3. Клонируйте `viktoriiarec/Lost-Mine-of-Phandelver`.
4. Закройте Unity.
5. Из исходной папки проекта скопируйте в клонированный репозиторий только `Assets`, `Packages` и `ProjectSettings`.
6. Не копируйте `Library`, `Temp`, `Logs`, `Obj`, `Build` и ZIP-файл.
7. Откройте терминал в папке репозитория и один раз выполните:

```bash
git lfs install
git lfs pull
```

8. Вернитесь в GitHub Desktop. Просмотрите список изменений.
9. В поле Summary напишите, например, `feat: import Unity project`.
10. Нажмите **Commit to current branch**, затем **Push origin**.

## Если большой файл уже попал в неудачный коммит

Простое удаление файла из папки иногда не помогает: GitHub продолжает видеть его в истории локального коммита.

Если этот коммит ещё не был успешно отправлен, в GitHub Desktop выберите **Repository → Undo most recent commit**, удалите ZIP и папку `Library`, затем создайте коммит заново.

## Для отдельных файлов больше 100 MB

Форматы, перечисленные в `.gitattributes`, автоматически направляются в Git LFS. Перед коммитом можно проверить:

```bash
git lfs track
git lfs status
```

Не используйте Git LFS для папки `Library` или полного ZIP-архива проекта.
