## Задание 15. Merge и разрешение конфликта
```text
*   62ba817 merge: resolve conflict in README.md
|\  
| * 48adaba docs: update title in feature branch
* | af5ad97 docs: update title in main branch
|/  
* 49e3109 feat: add feature note to README.md
* 9b76eff docs: add description to README.md
* b084694 docs: create README.md
* 7ad3c0b chore: save C# tasks progress
* 981d3a2 ╨Ф╨╛╨▒╨░╨▓╤М╤В╨╡ ╤Д╨░╨╣╨╗╤Л ╨┐╤А╨╛╨╡╨║╤В╨░.
* abc403f ╨Ф╨╛╨▒╨░╨▓╨╕╤В╤М .gitattributes ╨╕ .gitignore.
```

## Задание 35. .gitignore, .editorconfig, PR и rebase
```text
df31dbe feat: rebase step 1
803c099 Merge pull request #1 from misto4ka/feature/task-35
92480e4 docs: update task 35 demo file
c658d6a feat: add initial task 35 demo file
200995d chore: add gitignore and editorconfig
1888ef8 docs: add task 15 graph to git-notes.md
```

## Задание 46. git stash / stash pop
**git stash list (до pop):**
```text
stash@{0}: On main: WIP: temp changes for task 46
```
**git stash list (после pop):**
```text
(пусто)
```

## Задание 47. rebase -i (сквош 4 коммитов)
**git log --oneline (до squash):**
```text
258f6ce feat: squash commit 4
cf0f7a8 feat: squash commit 3
a1f131d feat: squash commit 2
81ce6b9 feat: squash commit 1
7aef8ad docs: add task 46 stash output to git-notes.md
828472a docs: add task 35 rebase log to git-notes.md
```
**git log --oneline (после squash):**
```text
6511bc3 feat: squash commit 1
7aef8ad docs: add task 46 stash output to git-notes.md
828472a docs: add task 35 rebase log to git-notes.md
```

## Задание 48. Теги (v1.0.0)
**git tag:**
```text
v1.0.0
```
**git show v1.0.0:**
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

## Задание 49. Восстановление коммита через reflog
**git reflog:**
```text
7512901 HEAD@{0}: reset: moving to HEAD~1
ca470ba HEAD@{1}: commit: feat: commit to be deleted and restored via reflog
7512901 HEAD@{2}: commit: docs: add task 48 tag output to git-notes.md
f6f1dd8 HEAD@{3}: commit: docs: add task 47 squash logs to git-notes.md
7aef8ad HEAD@{4}: checkout: moving from feature/squash-demo to main
6511bc3 HEAD@{5}: rebase (finish): returning to refs/heads/feature/squash-demo
```
**git log --oneline (после восстановления):**
```text
ca470ba feat: commit to be deleted and restored via reflog
7512901 docs: add task 48 tag output to git-notes.md
f6f1dd8 docs: add task 47 squash logs to git-notes.md
7aef8ad docs: add task 46 stash output to git-notes.md
828472a docs: add task 35 rebase log to git-notes.md
```

## Задание 50. Поиск «плохого» коммита через git bisect
**Начальный рабочий коммит (good):** `5bdc9ed`
**Ход бисекции:**
```text
Проверка f46a3b6 -> обнаружен баг -> git bisect bad
9c66287f91099dc36dd0243ed5e7a8ffc293dc7b was both 'good' and 'bad'
Проверка 9c6687 -> работает верно -> git bisect good
error: Bad rev input: 9c6687
```
**Завершение:** `git bisect reset`
