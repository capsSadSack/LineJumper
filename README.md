# LineJumper
LineJumper game

## Расширение приложения
### Добавление нового достижения
Для добавления нового достижения необходимо:
1. Завести новый элемент enum Achievement;
2. Для этого enum в AchievementsData задать в словаре names его название, а в словаре descriptions - описание;
3. Создать Sprite с иконкой достижения размера 512х512 px;
4. В AchievementsSceneController и AchievementMessageShower:
	- создать новое публичное поле типа Sprite, подцепить в Inspector созданную на шаге 3 иконку;
	- в методе GetSprite в конструкции switch-case добавить новый case, в котором вернуть поле из п.4.
5. Сгенерировать событие AchievementUnlockedEvent в момент открытия достижения с аргументом AchievementUnlockedArgs.
6. Подписать на созданное событие следующие методы: 
    - AchievementsDataSver.UnlockAchievement(args), 
	- AchievementMessageShower.ShowAchievementMessage(args).

### Изменение Prefab'ов
Изменение конструкций следующих prefab'ов может привести к нарушению работы приложения из-за наличия в их скриптах метода gameObject.transform.GetChild(n), где n - порядковый номер, забитый в коде:
- RecordItem,
- AchievementUnlocked

## TODO (first priority):
1) коррекция сложности
	- настроить числовые параметры DifficultySettings для всех Difficulty;
	- добавить в настройки сложности скоростной интервал врагов?
2) достижения
	- придумать достижения
		- собрать (уничтожить) 100/500/1000 врагов (потребуется вести подсчет и сохранять);
		- собрать (уничтожить) 10/25/50/100 врагов за игру (проверка вызывается при окончании игры);
	- иконки достижений
3) Game Center (Google Play Games plugin for Unity)
	- подключить аналог GameCenter для Android,
	- научиться получать глобальный рейтинг среди игроков, рейтинг 'World';
	- подключить достижения.
4) "Навести красоту"

## TODO (second priority):
1) RecordsScene: 
	- подкрашивать выбранные элементы (Local/World, Easy/Medium/Hard)

## Bugs:
- Когда показывается достижение нельзя нажимать на кнопки в EndGameMenu

## UI Bugs:
- MainMenuScene (и др., где используется MenuButton): 
     действие OnMouseClick() происходит раньше окончания анимации нажатой кнопки;
	 проблема наблюдается не только у MenuButton, но и у всех анимированных кнопок;

## Заглушки:
- Сообщение о получении достижения отображается всегда (не проверяется, было ли достижени уже открыто).
