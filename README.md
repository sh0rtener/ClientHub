# ClientHub
> Сервис для работы с бизнес-клиентами (просмотр, добавление, редактирование, удаление).

## О чем проект?
Проект представляет собой API для работы с клиентами.
Клиентами в данном сервисе выступают:
- Юридические лица;
- Индивидуальные предприниматели;

Для обеих категорий доступны получение списка с фильтрацией, получение клиента по ID, создание, обновление и удаление.

### Юридические лица
Для юридического лица хранятся:
- ИНН, ОГРН, наименование;
- регион, адрес, код ОКАТО, количество сотрудников;
- статус, товарный знак;
- даты регистрации и ликвидации;
- контакты, действия и события;
- арбитражные дела, исполнительные производства и дела в судах общей юрисдикции;
- юридические лица и физические лица — учредители;
- бенефициары, руководители и КПП.

### Индивидуальные предприниматели
Для индивидуального предпринимателя хранятся:
- ИНН, ОГРНИП, ФИО/наименование, регион и адрес;
- статус, товарный знак;
- даты регистрации и ликвидации;
- контакты, действия и события;
- арбитражные дела, исполнительные производства и дела в судах общей юрисдикции;
- сведения о физическом лице;
- регистрационные и налоговые реквизиты.

## Техническая составляющая
- **C# / .NET 8** — Web API;
- **PostgreSQL** — хранение данных;
- **Entity Framework Core** — ORM и миграции базы данных;
- **Redis** — кэширование;
- **MediatR** — обработка запросов и команд;
- **AutoMapper** — преобразование моделей;
- **Swagger** — документация и тестирование API;
- **Docker** — Dockerfile и docker-compose для сборки и запуска API.

## Конфигурация
- `ConnectionStrings:Default` — строка подключения к PostgreSQL;
- `Caching:ExpirationInMinutes` - время жизни кеша для запросов с большим количеством данных
- `Caching:SingleExpirationInMinutes` - время жизни кеша для запросов с определенными моделями (get/id и прочее)
- `Caching:Redis:Configuration` — адрес Redis;
- `Caching:Redis:InstanceName` — имя экземпляра кэша.

## Информация по миграции БД
При запуске приложение автоматически применяет миграции к базе данных.
Сами миграции располагаются тут: `Infra/ClientHub.Persistense`.
Для создания миграций необходимо указать как стартовый проект `Presenters/ClientHub.WebApi`, и как просто проект `Infra/ClientHub.Persistense`

## Как запустить?
### Запуск в Docker
Для того, чтобы запустить готовое решение в docker, достаточно:
- Перейти в директорию `infra`
- Запустить команду `docker compose up`

*ВАЖНО* - в docker-compose.yml хранится ссылка на файл конфигурации .env.example, для того чтобы настроить под себя необходимо создать идентичный файл .env, заполнить его по аналогии с .env.example и поменять ссылку в docker-compose.yml

### Запуск из исходников
Перед запуском создайте файл `appsettings.json` и заполните настройки относительно `Presenters/ClientHub.WebApi/appsettings.example.json`:

- `ConnectionStrings:Default` — строка подключения к PostgreSQL;
- `Caching:ExpirationInMinutes` - время жизни кеша для запросов с большим количеством данных
- `Caching:SingleExpirationInMinutes` - время жизни кеша для запросов с определенными моделями (get/id и прочее)
- `Caching:Redis:Configuration` — адрес Redis;
- `Caching:Redis:InstanceName` — имя экземпляра кэша.
Значения в файле являются локальными настройками по умолчанию. При необходимости замените их на свои.

## Документация по методам

Базовый адрес API: `http://localhost:5289`.

Адрес из docker: `http://localhost:8080`.

Все успешные ответы обёрнуты в объект с полем `message`. Если метод возвращает данные, они находятся в `data`.

### Индивидуальные предприниматели

#### Получение ИП с фильтрацией

Возвращает список ИП с краткой информацией. Все параметры фильтрации необязательные.

**Url:** `http://localhost:5289/api/ip/all` (`GET`)

**Параметры:**
- `Inn` — поиск по ИНН;
- `Ogrn` — поиск по ОГРНИП;
- `Region` — поиск по региону;
- `Status` — идентификатор статуса;
- `RegistrationDate` — дата регистрации;
- `LiquidationDate` — дата ликвидации;
- `Limit` — количество записей, по умолчанию `100`;
- `Offset` — смещение, по умолчанию `0`.

**Запрос:**
```bash
curl -G 'http://localhost:5289/api/ip/all' \
  --data-urlencode 'Inn=770123456789' \
  --data-urlencode 'Region=Москва' \
  --data-urlencode 'Limit=20' \
  --data-urlencode 'Offset=0'
```

**Пример ответа (200):**
```json
{
  "message": "success",
  "data": [
    {
      "id": 1,
      "inn": "770123456789",
      "ogrn": "315774600000000",
      "region": "Москва",
      "address": "Москва",
      "clientStatus": 1,
      "registrationDate": "2015-01-01T00:00:00Z",
      "liquidationDate": null,
      "requisite": {
        "id": "00000000-0000-0000-0000-000000000001",
        "kind": "ИП",
        "registrator": "ИФНС",
        "taxAuthority": "ИФНС",
        "registrationDate": "2015-01-01T00:00:00Z",
        "taxRegistrationDate": "2015-01-01T00:00:00Z"
      },
      "person": {
        "id": "00000000-0000-0000-0000-000000000002",
        "firstName": "Иван",
        "lastName": "Иванов",
        "patronymic": "Иванович",
        "birthDate": "1990-01-01T00:00:00Z",
        "gender": 0,
        "regionCode": "77",
        "createdAt": "2015-01-01T00:00:00Z",
        "updatedAt": null
      }
    }
  ]
}
```

#### Получение ИП по ID

Возвращает полную карточку индивидуального предпринимателя.

**Url:** `http://localhost:5289/api/ip/{id}` (`GET`)

**Параметры:** `id` — идентификатор ИП (`int`), передаётся в URL.

**Запрос:**
```bash
curl --location 'http://localhost:5289/api/ip/1'
```

**Пример ответа (200):**
```json
{
  "message": "success",
  "data": {
    "id": 1,
    "inn": "770123456789",
    "name": "Иванов Иван Иванович",
    "ogrn": "315774600000000",
    "region": "Москва",
    "address": "Москва",
    "status": 1,
    "goodsMark": {
      "id": "00000000-0000-0000-0000-000000000003",
      "name": "",
      "assignedAt": "2015-01-01T00:00:00Z",
      "dissasignedAt": null
    },
    "registrationDate": "2015-01-01T00:00:00Z",
    "liquidationDate": null,
    "clientContacts": [],
    "clientActions": [],
    "clientEvents": [],
    "arbitrageDeals": [],
    "proceedings": [],
    "jurisdictions": [],
    "requisite": {
      "id": "00000000-0000-0000-0000-000000000001",
      "kind": "ИП",
      "registrator": "ИФНС",
      "taxAuthority": "ИФНС",
      "registrationDate": "2015-01-01T00:00:00Z",
      "taxRegistrationDate": "2015-01-01T00:00:00Z"
    },
    "person": {
      "id": "00000000-0000-0000-0000-000000000002",
      "firstName": "Иван",
      "lastName": "Иванов",
      "patronymic": "Иванович",
      "birthDate": "1990-01-01T00:00:00Z",
      "gender": 0,
      "regionCode": "77",
      "createdAt": "2015-01-01T00:00:00Z",
      "updatedAt": null
    },
    "createdAt": "2015-01-01T00:00:00Z",
    "updatedAt": null
  }
}
```

#### Создание ИП

Создаёт карточку индивидуального предпринимателя.

**Url:** `http://localhost:5289/api/ip` (`POST`)

**Модель запроса:** `IndividualEntrepreneursDto`. Вложенные массивы можно передать пустыми, если связанные сведения отсутствуют.

**Запрос:**
```bash
curl --location 'http://localhost:5289/api/ip' \
  --header 'Content-Type: application/json' \
  --data '{
    "inn": "770123456789",
    "name": "Иванов Иван Иванович",
    "ogrn": "315774600000000",
    "region": "Москва",
    "address": "Москва",
    "status": 1,
    "goodsMark": {
      "name": "",
      "assignedAt": "2015-01-01T00:00:00Z",
      "dissasignedAt": null
    },
    "registrationDate": "2015-01-01T00:00:00Z",
    "liquidationDate": null,
    "clientContacts": [],
    "clientActions": [],
    "clientEvents": [],
    "arbitrageDeals": [],
    "proceedings": [],
    "jurisdictions": [],
    "requisite": {
      "kind": "ИП",
      "registrator": "ИФНС",
      "taxAuthority": "ИФНС",
      "registrationDate": "2015-01-01T00:00:00Z",
      "taxRegistrationDate": "2015-01-01T00:00:00Z"
    },
    "person": {
      "firstName": "Иван",
      "lastName": "Иванов",
      "patronymic": "Иванович",
      "birthDate": "1990-01-01T00:00:00Z",
      "gender": 0,
      "regionCode": "77"
    }
  }'
```

**Пример ответа (200):** возвращается созданная карточка в `data` с полями `IndividualEntrepreneursDto`, включая присвоенный `id`.

```json
{
  "message": "success",
  "data": {
    "id": 1,
    "inn": "770123456789",
    "name": "Иванов Иван Иванович",
    "ogrn": "315774600000000",
    "region": "Москва",
    "address": "Москва",
    "status": 1,
    "registrationDate": "2015-01-01T00:00:00Z",
    "liquidationDate": null,
    "clientContacts": [],
    "clientActions": [],
    "clientEvents": [],
    "arbitrageDeals": [],
    "proceedings": [],
    "jurisdictions": []
  }
}
```

#### Обновление ИП

Обновляет карточку ИП по идентификатору. Тело запроса соответствует `IndividualEntrepreneursDto`, как при создании.

**Url:** `http://localhost:5289/api/ip/{id}` (`PUT`)

**Запрос:**
```bash
curl --location --request PUT 'http://localhost:5289/api/ip/1' \
  --header 'Content-Type: application/json' \
  --data @individual-entrepreneur.json
```


**Пример ответа (200):** возвращается обновлённая карточка в `data`.

#### Удаление ИП

Удаляет ИП по идентификатору.

**Url:** `http://localhost:5289/api/ip/{id}` (`DELETE`)

**Запрос:**
```bash
curl --location --request DELETE 'http://localhost:5289/api/ip/1'
```

**Пример ответа (200):**
```json
{
  "message": "success"
}
```

### Юридические лица

#### Получение юридических лиц с фильтрацией

Возвращает список юридических лиц с краткой информацией. Все параметры фильтрации необязательные.

**Url:** `http://localhost:5289/api/legal-entities/all` (`GET`)

**Параметры:**
- `Inn` — поиск по ИНН;
- `Ogrn` — поиск по ОГРН;
- `Region` — поиск по региону;
- `Status` — идентификатор статуса;
- `RegistrationDate` — дата регистрации;
- `LiquidationDate` — дата ликвидации;
- `Limit` — количество записей, по умолчанию `100`;
- `Offset` — смещение, по умолчанию `0`.

**Запрос:**
```bash
curl --location --get 'http://localhost:5289/api/legal-entities/all' \
  --data-urlencode 'Inn=7701234567' \
  --data-urlencode 'Region=Москва' \
  --data-urlencode 'Limit=20' \
  --data-urlencode 'Offset=0'
```

**Пример ответа (200):**
```json
{
  "message": "success",
  "data": [
    {
      "id": 1,
      "inn": "7701234567",
      "ogrn": "1027700000000",
      "region": "Москва",
      "address": "Москва",
      "clientStatus": 1,
      "registrationDate": "2002-01-01T00:00:00Z",
      "liquidationDate": null
    }
  ]
}
```

#### Получение юридического лица по ID

Возвращает полную карточку юридического лица.

**Url:** `http://localhost:5289/api/legal-entities/{id}` (`GET`)

**Параметры:** `id` — идентификатор юридического лица (`int`), передаётся в URL.

**Запрос:**
```bash
curl --location 'http://localhost:5289/api/legal-entities/1'
```

**Пример ответа (200):** объект `LegalEntityDto` находится в `data` и содержит основные реквизиты, контакты, события, судебные дела, исполнительные производства, учредителей, бенефициаров, руководителей и КПП.

#### Создание юридического лица

Создаёт карточку юридического лица.

**Url:** `http://localhost:5289/api/legal-entities` (`POST`)

**Модель запроса:** `LegalEntityDto`.

**Запрос:**
```bash
curl --location 'http://localhost:5289/api/legal-entities' \
  --header 'Content-Type: application/json' \
  --data '{
    "inn": "7701234567",
    "name": "ООО Ромашка",
    "ogrn": "1027700000000",
    "region": "Москва",
    "address": "Москва",
    "status": 1,
    "okato": "45286585000",
    "headCount": 10,
    "goodsMark": {
      "name": "",
      "assignedAt": "2002-01-01T00:00:00Z",
      "dissasignedAt": null
    },
    "registrationDate": "2002-01-01T00:00:00Z",
    "liquidationDate": null,
    "clientContacts": [],
    "clientActions": [],
    "clientEvents": [],
    "arbitrageDeals": [],
    "proceedings": [],
    "jurisdictions": [],
    "founderClients": [],
    "founderPersons": [],
    "beneficiars": [],
    "maintainers": [],
    "kpps": []
  }'
```

**Пример ответа (200):** возвращается созданная карточка юридического лица в поле `data`.

#### Обновление юридического лица

Обновляет карточку юридического лица по идентификатору. Тело запроса соответствует `LegalEntityDto`, как при создании.

**Url:** `http://localhost:5289/api/legal-entities/{id}` (`PUT`)

**Запрос:**
```bash
curl --location --request PUT 'http://localhost:5289/api/legal-entities/1' \
  --header 'Content-Type: application/json' \
  --data @legal-entity.json
```


**Пример ответа (200):** возвращается обновлённая карточка юридического лица в поле `data`.

#### Удаление юридического лица

Удаляет юридическое лицо по идентификатору.

**Url:** `http://localhost:5289/api/legal-entities/{id}` (`DELETE`)

**Запрос:**
```bash
curl --location --request DELETE 'http://localhost:5289/api/legal-entities/1'
```

**Пример ответа (200):**
```json
{
  "message": "success"
}
```