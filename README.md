# README

# Компоненты системы:
  * UserCurrency.ApiGateway - единая точка входа, маршрутизация, аутентификация
  * UserService - регистрация, логин, логаут пользователей (Clean Architecture + CQRS)
  * CurrencyService - работа с валютами и избранным (Clean Architecture + CQRS)
  * UserCurrency.BackgroundWorker - фоновое обновление курсов валют
  * UserCurrency.MigrationService - сервис для миграции базы данных
  * UserCurrency.Tests - тестирование

# Технологический стек
  * .NET 8 - платформа разработки
  * PostgreSQL 15 - основная база данных
  * Entity Framework - ORM
  * YARP - API Gateway
  * JWT Bearer - аутентификация
  * MSTest / Moq - unit тестирование

Api Gateway запускается на localhost:5000.<br>Избранные влюты пользователя добавляются через БД в таблицу *currency_users*

# Endpoints:
  * Открытые эндпоинты:
    + /user-service/user/registration - регистрация юзера
    + /user-service/login - авторизация юзера, получение jwt токена

  * Защищенный эндпоинт:
    + /currency-service/currency - получение валют пользователя
