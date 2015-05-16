## About ##

Synchronize directories at different computers through a proxy server with the ability to access files through the site and preservation of all changes in the files.
Is written on .NET Framework 4, C#.

## Описание ##

Sinchosaur - это программный комплекс с открытым исходным кодом под лицензией GPL3 ([перевод](http://code.google.com/p/gpl3rus/wiki/LatestRelease)).
Позволяет синхронизировать каталоги на разных компьютерах с доступом в сеть через промежуточный сервер с помощью приложения-клиента, либо через веб сайт. К достоинствам системы можно отнести сохранение полной истории изменения файлов.

Система написана на .NET Framework, C#. Сайт - ASP MVC 2.

## Установка ##
1. Получить исходники из svn репозитария http://sinchosaur.googlecode.com/svn/trunk/<br>
2. Для успешной компиляции проекта требуется IDE с поддержкой WCF проектов и .NET Framework 4, SQL Server 2008 <br>
3. Добавить БД из файла резервной копии DBBackUp.bak<br>
4. Прописать в файле \Server.Service\app.config строку подключения к БД<br>
5. Изменить при необходимости порты для прослушивания сервером в файле \Server\app.config, порт по умолчанию 8085 <br>
6. Изменить при необходимости каталог для хранение файлов пользователей в файле \Server.Service\app.config, по умолчанию С:\ServerFolder<br>
7. Скомпилировать проект.<br>
8. Добавить пользователей в БД, таблица Users<br>
9. Изменить в настройках клиента IP сервера, email и пароль пользователя<br>


<h2>TODO for 0.1.2 version</h2>

1. Public - <b>done</b> и shared файлы <br>
2. Глобализация клиента и сайта - <b>done</b> <br>
3. Приложение для управления сервером <br>
4. Сделать клиенты под MacOS и Linux <br>
5. Интеграция с Explorer-ом <br>
6. Реализовать ограничения по дисковому занимаемому пространству - <b>done</b> <br>
7. Сделать инсталлятор для клиента<br>
8. Сделать фото галереи <br>
9. Сделать возможность скачать каталог как zip архив <br>



<h2>Скриншоты</h2>

<h3>Клиент</h3>

<i>настройки</i><br>
<img src='http://dl.dropbox.com/u/7926208/Sinchosaur_screenshots/app1.png' />

<i>нотификация</i><br>
<img src='http://dl.dropbox.com/u/7926208/Sinchosaur_screenshots/app3.png' />

<i>история изменений</i><br>
<img src='http://dl.dropbox.com/u/7926208/Sinchosaur_screenshots/app4.png' />

<i>синхронизация файла</i><br>
<img src='http://dl.dropbox.com/u/7926208/Sinchosaur_screenshots/app5.png' />

<h3>Сервер</h3>

<img src='http://dl.dropbox.com/u/7926208/Sinchosaur_screenshots/server.png' />

<i>логирование</i><br>
<img src='http://dl.dropbox.com/u/7926208/Sinchosaur_screenshots/server1.png' />

<h3>Сайт</h3>

<i>управление файлами</i><br>
<img src='http://dl.dropbox.com/u/7926208/Sinchosaur_screenshots/site1.png' />

<img src='http://dl.dropbox.com/u/7926208/Sinchosaur_screenshots/site4.png' />

<i>история изменений</i><br>
<img src='http://dl.dropbox.com/u/7926208/Sinchosaur_screenshots/site2.png' />

<i>добавление файлов</i><br>
<img src='http://dl.dropbox.com/u/7926208/Sinchosaur_screenshots/site3.png' />

<h3>AdminTool</h3>
<i>в разработке</i><br>
<img src='http://dl.dropbox.com/u/7926208/Sinchosaur_screenshots/admin_tool1.png' />









