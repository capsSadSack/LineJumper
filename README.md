# LineJumper
LineJumper game

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
	- Canvas с генерирующимся в коде объектом достижения
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
- RecordsScene: не работает маска на объекте ScrollView;
- RecordsScene: плохо работает прокрутка списка рекордов;