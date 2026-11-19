# Git notes

## Task 15 — Basic git
### Commands
```bash
git init
git branch -M main
git add README.md
git commit -m "docs: create README.md"
git checkout -b feature/git-basics
git commit -m "feat: add feature note to README.md"
git checkout main
git merge feature/git-basics
# Моделирование и ручное разрешение конфликта в README.md (удаление маркеров <<<<<<<, =======, >>>>>>>)
git add README.md
git commit -m "merge: resolve conflict in README.md"
git log --graph --oneline --all
```
### Output (git log --graph --oneline --all)
```text
*   6c839e2 merge: merge level-3 into main
|\  
| * 22e4d9c feat: complete level 3 tasks (36-50)
|/  
*   24734ac merge: merge level-2 into main
|\  
| * 10a66b0 feat: complete level 2 tasks (16-35)
|/  
*   d99345d merge: merge level-1 into main
|\  
| * cb349e8 feat: complete level 1 tasks (1-15)
|/  
* 391b5fe chore: update gitignore with .idea and *.suo
* 0330cf4 docs: format README.md matching practicum template
* 84d8727 docs: format README.md with practicum summary tables
* 644dfa3 docs: add task 50 bisect output to git-notes.md
* dd7c954 feat(bisect): step 5 - minor update
* dfdfe4b feat(bisect): step 4 - formatting
```
### What I learned
Освоил создание и переключение веток, слияние через `git merge` и ручное разрешение конфликтов при одновременном изменении одних и тех же строк в разных ветках.

---

## Task 35 — Branches & PR
### PR link
https://github.com

### Rebase result
```text
6c839e2 merge: merge level-3 into main
24734ac merge: merge level-2 into main
22e4d9c feat: complete level 3 tasks (36-50)
d99345d merge: merge level-1 into main
10a66b0 feat: complete level 2 tasks (16-35)
```
### What I learned
Настроил `.gitignore` и `.editorconfig`, оформил коммиты по Conventional Commits, создал и смержил Pull Request в `main`, а также объединил серию коммитов в ветке `feature/rebase-demo` через `git rebase -i HEAD~3`.

---

## Task 46 — git stash
### Commands
```bash
git stash push -m "WIP: temp changes for task 46"
git stash list
git checkout feature/git-basics
git checkout main
git stash pop
git stash list
```
### Output
```text
До pop:    stash@{0}: On main: WIP: temp changes for task 46
После pop: (пусто — изменения восстановлены в рабочую директорию)
```
### What I learned
Научился временно сохранять незакоммиченные изменения в стек `stash`, безопасно переключаться между ветками и восстанавливать работу через `git stash pop`.

---

## Task 47 — git rebase -i
### Commands
```bash
git checkout -b feature/squash-demo
# 4 последовательных коммита
git rebase -i HEAD~4
git log --oneline
```
### Output
```text
До squash:    4 отдельных коммита (feat: squash commit 1..4)
После squash: 1 объединённый коммит в ветке feature/squash-demo
```
### What I learned
Интерактивный `rebase -i` с заменой `pick` на `squash` (`s`) позволяет склеить мелкие промежуточные коммиты в один чистый коммит перед слиянием.

---

## Task 48 — tags
### Commands
```bash
git tag -a v1.0.0 -m "Release v1.0.0"
git push origin v1.0.0
git tag
git show v1.0.0
```
### Output
```text
tag v1.0.0
Tagger: misto4ka <A32nn88a@gmail.com>
Date:   Thu Nov 19 17:02:45 2026 +0300

Release v1.0.0

commit f6f1dd8258818c4275e808cbd7fe55e90f24430d
Author: misto4ka <A32nn88a@gmail.com>
Date:   Thu Nov 19 17:02:06 2026 +0300

    docs: add task 47 squash logs to git-notes.md

 git-notes.md | 17 +++++++++++++++++
 1 file changed, 17 insertions(+)
```
### What I learned
Аннотированные теги (`git tag -a`) фиксируют релизные версии проекта, хранят метаданные автора и описание релиза, а также публикуются на GitHub командой `git push origin v1.0.0`.

---

## Task 49 — reflog
### Commands
```bash
git commit -m "feat: commit to be deleted and restored via reflog"
git reset --hard HEAD~1
git reflog
git reset --hard ca470ba
git log --oneline
```
### Output
```text
6c839e2 HEAD@{0}: merge level-3: Merge made by the 'ort' strategy.
24734ac HEAD@{1}: checkout: moving from level-3 to main
22e4d9c HEAD@{2}: commit: feat: complete level 3 tasks (36-50)
24734ac HEAD@{3}: checkout: moving from main to level-3
24734ac HEAD@{4}: merge level-2: Merge made by the 'ort' strategy.
```
### What I learned
Даже после жёсткого отката `git reset --hard` локальный журнал `git reflog` хранит историю перемещений `HEAD` и позволяет восстановить удалённый коммит по его хешу.

---

## Task 50 — bisect
### Commands
```bash
git bisect start
git bisect bad
git bisect good 5bdc9ed
# Пошаговая проверка коммитов (git bisect bad / git bisect good)
git bisect reset
```
### Output
```text
f46a3b6eb89b5b295a25f5a990e59784fcbdb99e is the first bad commit
feat(bisect): step 3 - refactor Add (BUG introduced)
```
### What I learned
Бинарный поиск `git bisect` позволяет за (\log N)$ шагов найти точный коммит, в котором появилась ошибка, среди большой истории изменений.