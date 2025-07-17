# 🚀 Быстрый старт ARM Frontend Projects

## 📋 Предварительные требования

- Node.js 18+ 
- npm или yarn
- Git

## ⚡ Быстрый запуск

### 1. Клонирование и установка

```bash
# Клонируйте репозиторий
git clone <repository-url>
cd ARM/Front

# Установите зависимости для всех проектов
cd Customer && npm install && cd ..
cd Manager && npm install && cd ..
cd Admin && npm install && cd ..
```

### 2. Запуск всех проектов

```bash
# Запустите все проекты одновременно
./start-all.sh
```

Или запустите каждый проект отдельно:

```bash
# Customer Frontend (порт 3000)
cd Customer && npm run dev

# Manager Frontend (порт 3001) 
cd Manager && npm run dev

# Admin Frontend (порт 3002)
cd Admin && npm run dev
```

### 3. Доступ к приложениям

После запуска откройте в браузере:

- 🚗 **Customer**: http://localhost:3000
- 👔 **Manager**: http://localhost:3001  
- 🔧 **Admin**: http://localhost:3002

### 4. Демо доступ

Для всех проектов используйте:
- **Email**: любой (например: `demo@example.com`)
- **Пароль**: любой (например: `password`)

## 🛑 Остановка проектов

```bash
# Остановить все проекты
./stop-all.sh

# Или нажмите Ctrl+C в каждом терминале
```

## 🔧 Настройка портов

Если порты заняты, измените их в `package.json` каждого проекта:

```json
{
  "scripts": {
    "dev": "next dev -p 3000"  // Измените порт
  }
}
```

## 📱 Что тестировать

### Customer Frontend
- ✅ Регистрация и вход
- ✅ Личный кабинет
- ✅ Просмотр автомобилей
- ✅ Создание заявок

### Manager Frontend  
- ✅ Вход в систему
- ✅ Панель управления
- ✅ Просмотр статистики
- ✅ Управление заявками

### Admin Frontend
- ✅ Вход администратора
- ✅ Управление пользователями
- ✅ Управление ролями
- ✅ Системные настройки

## 🐛 Устранение проблем

### Порт занят
```bash
# Найти процесс на порту
lsof -ti:3000

# Убить процесс
kill -9 <PID>
```

### Зависимости не установлены
```bash
cd Customer && npm install
cd ../Manager && npm install  
cd ../Admin && npm install
```

### Ошибки TypeScript
```bash
# Очистить кэш
rm -rf .next
npm run dev
```

## 📞 Поддержка

- 📧 Email: support@arm-service.ru
- 📱 Телефон: +7 (999) 123-45-67
- 📚 Документация: [README.md](./README.md)

---

**Готово!** 🎉 Все проекты запущены и готовы к тестированию. 