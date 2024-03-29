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

### Добавление нового типа PickUp-объекта
Для добавления нового PickUp-объекта необходимо:
1. Завести новый элемент enum PickUp;
2. Добавить новый класс-декоратор, наследующий от APickUpSpawnerDecorator с переопределенным методом CreateSpawnObject;
3. В классе PickUpSpawner дополнить метод InitializePickUpSpawners с использованием созданного в п.2 класса-декоратора.

### Добавление нового типа врага
Для добавления нового врага необходимо:
1. Завести новый элемент enum Enemy;
2. Создать новый prefab с тегом Enemy с нужными аниматорами, эффектами и т.п. в Resources/Prefabs.
3. Создать необходимые декораторы (AEnemyDecorator) для изменения поведения;
4. В классе EnemyFabric применить необходимые декораторы;
5. В классе EnemySpawner дополнить метод GetEnemyType() с учетом вероятности появления созданного типа врага;
6. В классе EnemySpawner дополнить метод GetEnemy(), указав путь к новому prefab-объекту.
7. У Prefab'а должен быть компонент Animator с определенными параметрами типа bool isExploding, isAggressive.

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

1) Положительные эффекты:
	- Добаботать NuclearBomb:
		- звук подготовки бомбы;
		- анимация взрыва + звук взрыва;
	- Overpower:
		- режим бога на 5 секунд;
		- анимация (шипы, как у врага, а сам игрок "золотой" и светится);
		- обратный отсчёт до окончания действия режима;
	- TimeStop:
		- замедление времени (Снизить TimeScale);
		- particleSystem из какой-нибудь медленно-летающей фигни (пыли).
		
2) Звуки

3) Достижения
	- придумать достижения
		- собрать (уничтожить) 100/500/1000 врагов (потребуется вести подсчет и сохранять);
		- "Я - скорость" - уничтожить 10 врагов за 5 секунд, не находясь под действием положительных эффектов;
		- уничтожить 3 врага за один прыжок.
	- иконки достижений
	- одно из достижений отключает рекламу (уничтожить 250 за одну игру)
	
4) Game Center (Google Play Games plugin for Unity)
	- подключить аналог GameCenter для Android,	
	- подключить достижения.

5) Дополнить раздел How To Play новой информацией:
	- описание pick-up объектов,
	- описание врагов,
	- схема "кто кого сильнее".
	
6) "Навести красоту"


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
