# Настройка окружения

## 1. Создайте файл .env.local

В корне проекта `Front/Admin/` создайте файл `.env.local` со следующим содержимым:

```env
# API Configuration
NEXT_PUBLIC_API_URL=http://localhost:7033

# Authentication
NEXT_PUBLIC_AUTH_ENABLED=true

# Development
NODE_ENV=development
```

## 2. Запуск проекта

### Запустите бэкенд:
```bash
cd Back
dotnet run --project ARM.Presentation
```

### Запустите фронтенд:
```bash
cd Front/Admin
npm run dev
```

## 3. Доступ к приложению

Фронтенд будет доступен по адресу: http://localhost:3000

## 4. Тестовые данные

Для тестирования используйте следующие учетные данные:

- **Super Admin**: admin@arm.com / password123
- **Manager**: manager@arm.com / password123  
- **Customer**: customer@arm.com / password123

## 5. Возможные проблемы

### Если бэкенд не запускается:
- Убедитесь, что .NET 8 SDK установлен
- Проверьте, что PostgreSQL запущен
- Проверьте строку подключения в `appsettings.json`

### Если фронтенд не подключается к API:
- Убедитесь, что бэкенд запущен на порту 7033
- Проверьте файл `.env.local`
- Проверьте консоль браузера на ошибки CORS

### Если возникают ошибки TypeScript:
- Выполните `npm install` для установки зависимостей
- Проверьте версии пакетов в `package.json` 