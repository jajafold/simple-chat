# Simple Chat
# Авторы
Дырда Лев  
Окулова Ольга  
Чевычелов Максим
# Описание
Клиент и чат-сервер. Сервер написан на ASP.NET, клиент - на WinForms. Весь проект разбит на слои, UI, домен и инфраструктуру. Инфраструктура для сервера и клиента общая, распространяется отдельной dll-библиотекой. База данных реализована с помощью SQLITE и EntityFramework, сообщения и комнаты сохраняются даже при случае падения сервера.  
# Архитектура решения
<img width="440" alt="Снимок экрана 2023-01-23 в 11 17 52" src="https://user-images.githubusercontent.com/71509527/213976773-2ac973a1-af94-4646-a2c5-7e05955b016f.png">

# Основные возможности
- Отправка сообщений
- Создание комнат
- Задание пароля к комнатам
- Отображение доступных комнат

# Начало работы
Необходимо включить сервер. Никаких дополнительных настроек не предусмотрено. Если сервер не найдет базу данных в директории, он создаст и проинициализирует ее сам.  
При работающем сервере клиент разрешит ввести логин, иначе - будет ошибка. Можно заходить и начинать работу.
