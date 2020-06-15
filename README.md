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
6. Подписать на созданное событие следующие методы в указанном порядке: 
    - AchievementMessageShower.ShowAchievementMessage(args)
    - AchievementsDataSver.UnlockAchievement(args).

### Изменение Prefab'ов
Изменение конструкций следующих prefab'ов может привести к нарушению работы приложения из-за наличия в их скриптах метода gameObject.transform.GetChild(n), где n - порядковый номер, забитый в коде:
- RecordItem,
- AchievementUnlocked

## Управление таблицей рекордов
Доступ к таблицам рекордов можно осуществить по ссылкам:
http://dreamlo.com/lb/LSJi_XdPkE2pPJV6Nvfruw3CYGcNbAU0mY1Z0BPEVOFg

http://dreamlo.com/lb/2BwxuH-vXkaLtS7TcRAl0wkVWB7f-X6kSeSApHSHWaNg

http://dreamlo.com/lb/olqSn4c-2ESYmMFrvJzOxw9uz_Pi6xlU2FuABbOBkt5w

## TODO (first priority):
1) достижения
	- придумать достижения
		- собрать (уничтожить) 100/500/1000 врагов (потребуется вести подсчет и сохранять);
		- "Я - скорость" - уничтожить 5 врагов за 10 секунд.
	- иконки достижений
2) Game Center (Google Play Games plugin for Unity)
	- подключить аналог GameCenter для Android,
	- подключить достижения.
3) "Навести красоту"
4) Звук

## TODO (second priority):
1) RecordsScene: 
	- подкрашивать выбранные элементы (Local/World, Easy/Medium/Hard)

## Bugs:
- Когда показывается достижение нельзя нажимать на кнопки в EndGameMenu
- Кнопоки на сцене с рекордами надо нажимать дважды.

## UI Bugs:
- MainMenuScene (и др., где используется MenuButton): 
     действие OnMouseClick() происходит раньше окончания анимации нажатой кнопки;
	 проблема наблюдается не только у MenuButton, но и у всех анимированных кнопок;
