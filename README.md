# ARM - AutoRepairManager

🚗 **Современная SaaS-платформа для управления автосервисами**

Полнофункциональная система управления автосервисами с тремя ролями пользователей: клиент, менеджер и супер-админ.

## 🏗️ Архитектура проекта

```
ARM/
├── Back/                    # Backend (.NET 8)
│   ├── ARM.Core/           # Доменная модель
│   ├── ARM.Infrastructure/ # Реализация репозиториев
│   ├── ARM.Application/    # Бизнес-логика
│   ├── ARM.Presentation/   # API контроллеры
│   ├── ARM.RequestPipeline/# CQRS команды
│   └── ARM.Common/         # Общие утилиты
├── Front/                   # Frontend приложения
│   ├── Admin/              # Админ-панель (Next.js)
│   └── Landing/            # Лендинг (Next.js)
└── README.md
```

## 🚀 Технологический стек

### Backend
- **.NET 8** - Основной фреймворк
- **Entity Framework Core** - ORM
- **PostgreSQL** - База данных
- **Redis** - Кэширование
- **JWT** - Аутентификация
- **MediatR** - CQRS паттерн
- **AutoMapper** - Маппинг объектов
- **FluentValidation** - Валидация

### Frontend
- **Next.js 15** - React фреймворк
- **React 19** - UI библиотека
- **TypeScript** - Типизация
- **Tailwind CSS** - Стилизация
- **React Query** - Управление состоянием
- **Axios** - HTTP клиент
- **Heroicons** - Иконки

## 📋 Функциональность

### 👤 Клиент
- ✅ Просмотр своих автомобилей
- ✅ История заказов на ремонт
- ✅ Создание новых заказов
- ✅ Просмотр статуса ремонта
- ✅ Оставление отзывов
- ✅ Управление профилем

### 👨‍💼 Менеджер
- ✅ Управление клиентами
- ✅ Управление автомобилями
- ✅ Создание и редактирование заказов
- ✅ Управление услугами
- ✅ Настройка рабочих часов
- ✅ Просмотр отзывов
- ✅ Генерация отчетов

### 👑 Супер-Админ
- ✅ Управление пользователями системы
- ✅ Управление ролями и разрешениями
- ✅ Управление автосервисами (брендами)
- ✅ Полный доступ ко всем функциям
- ✅ Системные настройки

## 🛠️ Установка и запуск

### Предварительные требования
- **.NET 8 SDK**
- **Node.js 18+**
- **PostgreSQL 14+**
- **Redis 6+**

### Backend

1. **Клонирование репозитория**
```bash
git clone <repository-url>
cd ARM/Back
```

2. **Настройка базы данных**
```bash
# Обновите строку подключения в appsettings.json
"ConnectionStrings": {
  "ARM": "Host=localhost;Port=5432;Database=arm;Username=your_username;Password=your_password"
}
```

3. **Запуск миграций**
```bash
dotnet ef database update
```

4. **Запуск приложения**
```bash
dotnet run --project ARM.Presentation
```

Backend будет доступен по адресу: http://localhost:7033

### Frontend (Admin Panel)

1. **Переход в директорию**
```bash
cd Front/Admin
```

2. **Установка зависимостей**
```bash
npm install
```

3. **Настройка переменных окружения**
```bash
# Создайте .env.local файл
NEXT_PUBLIC_API_URL=http://localhost:7033
```

4. **Запуск в режиме разработки**
```bash
npm run dev
```

Frontend будет доступен по адресу: http://localhost:3000

## 🔐 Аутентификация

Система использует JWT токены для аутентификации:

### Тестовые аккаунты
- **Админ**: admin@arm-service.com / admin123
- **Менеджер**: manager@arm-service.com / manager123
- **Клиент**: customer@arm-service.com / customer123

## 📊 Основные сущности

1. **Brand** - Автосервисы/бренды
2. **Customer** - Клиенты
3. **Car** - Автомобили клиентов
4. **RepairOrder** - Заказы на ремонт
5. **Service** - Услуги
6. **Review** - Отзывы
7. **User** - Пользователи системы
8. **Role/Permission** - Роли и разрешения
9. **Venue** - Местоположения
10. **WorkingHour** - Рабочие часы

## 🎨 UI/UX Особенности

- **Responsive Design** - Адаптивный дизайн
- **Dark/Light Mode** - Поддержка тем
- **Real-time Updates** - Обновления в реальном времени
- **Interactive Charts** - Интерактивные графики
- **Advanced Filtering** - Продвинутая фильтрация
- **Bulk Operations** - Массовые операции
- **Export/Import** - Экспорт/импорт данных

## 🔧 Разработка

### Backend
```bash
# Запуск тестов
dotnet test

# Создание миграции
dotnet ef migrations add MigrationName

# Обновление базы данных
dotnet ef database update
```

### Frontend
```bash
# Линтинг
npm run lint

# Сборка
npm run build

# Запуск тестов
npm test
```

## 📈 Мониторинг и логирование

- **Health Checks** - Проверка состояния сервисов
- **Structured Logging** - Структурированное логирование
- **Performance Monitoring** - Мониторинг производительности
- **Error Tracking** - Отслеживание ошибок

## 🚀 Деплой

### Backend (Docker)
```bash
docker build -t arm-backend .
docker run -p 7033:7033 arm-backend
```

### Frontend (Vercel)
```bash
npm run build
vercel --prod
```

## 📝 API Документация

После запуска backend, Swagger документация доступна по адресу:
http://localhost:7033/swagger

## 🤝 Вклад в проект

1. Fork репозитория
2. Создайте feature branch (`git checkout -b feature/amazing-feature`)
3. Commit изменения (`git commit -m 'Add amazing feature'`)
4. Push в branch (`git push origin feature/amazing-feature`)
5. Откройте Pull Request

## 📄 Лицензия

Этот проект лицензирован под MIT License - см. файл [LICENSE](LICENSE) для деталей.

## 📞 Поддержка

Если у вас есть вопросы или предложения, создайте issue в репозитории или свяжитесь с командой разработки.

---

**ARM - AutoRepairManager** - Современное решение для управления автосервисами 🚗⚡ 