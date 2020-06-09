# LineJumper
LineJumper game

## Расширение приложения
### Добавление нового достижения
Для добавления нового достижения необходимо:
1. Завести новый элемент enum Achievement;
2. Для этого enum в AchievementsData задать в словаре names его название, а в словаре descriptions - описание;
3. Создать Sprite с иконкой достижения размера 512х512 px;
4. В AchievementsSceneController создать новое публичное поле типа Sprite, подцепить в Inspector созданную на шаге 3 иконку;
5. В AchievementsSceneController в методе GetSprite в конструкции switch-case добавить новый case, в котором вернуть поле из п.4;
6. (доработать пункт) аналогичные действия проделать с AchievementMessageShower.

### Изменение Prefab'ов
Изменение конструкций следующих prefab'ов может привести к нарушению работы приложения из-за наличия в их скриптах метода gameObject.transform.GetChild(n), где n - порядковый номер, забитый в коде:
- RecordItem,
- AchievementUnlocked

## TODO (first priority):
1) Игра:
	- сделать так, чтобы враги не стремились покидать игровое поле;
2) коррекция сложности
	- настроить числовые параметры DifficultySettings для всех Difficulty
4) достижения
	- придумать достижения
		- собрать (уничтожить) 100/500/1000 врагов (потребуется вести подсчет и сохранять);
		- собрать (уничтожить) 10/25/50/100 врагов за игру (проверка вызывается при окончании игры);
		- уничтожить 2 врага за один прыжок (потребуется добавить локальный счетчик);
	- иконки достижений
5) Game Center (Google Play Games plugin for Unity)
	- подключить аналог GameCenter для Android,
	- научиться получать глобальный рейтинг среди игроков, рейтинг 'World';
	- подключить достижения.
6) реклама

## TODO (second priority):
1) RecordsScene: 
	- подкрашивать выбранные элементы (Local/World, Easy/Medium/Hard)

## Bugs:
- enemies can change direction in jump when OnGameAction event occurs.
- enemies can cange aggression state in jump!!!

## UI Bugs:
- MainMenuScene (и др., где используется MenuButton): 
     действие OnMouseClick() происходит раньше окончания анимации нажатой кнопки;
	 проблема наблюдается не только у MenuButton, но и у всех анимированных кнопок;
