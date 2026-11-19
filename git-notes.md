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
