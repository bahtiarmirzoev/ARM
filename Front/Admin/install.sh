#!/bin/bash

echo "🚀 Установка зависимостей для ARM Admin Dashboard..."

# Проверка наличия Node.js
if ! command -v node &> /dev/null; then
    echo "❌ Node.js не установлен. Пожалуйста, установите Node.js 18+"
    exit 1
fi

# Проверка версии Node.js
NODE_VERSION=$(node -v | cut -d'v' -f2 | cut -d'.' -f1)
if [ "$NODE_VERSION" -lt 18 ]; then
    echo "❌ Требуется Node.js версии 18 или выше. Текущая версия: $(node -v)"
    exit 1
fi

echo "✅ Node.js версии $(node -v) найден"

# Установка зависимостей
echo "📦 Установка npm зависимостей..."
npm install

if [ $? -eq 0 ]; then
    echo "✅ Зависимости успешно установлены"
    
    # Создание .env.local файла
    if [ ! -f .env.local ]; then
        echo "🔧 Создание .env.local файла..."
        cat > .env.local << EOF
NEXT_PUBLIC_API_URL=http://localhost:7033
EOF
        echo "✅ .env.local файл создан"
    else
        echo "ℹ️  .env.local файл уже существует"
    fi
    
    echo ""
    echo "🎉 Установка завершена!"
    echo ""
    echo "Для запуска в режиме разработки выполните:"
    echo "  npm run dev"
    echo ""
    echo "Приложение будет доступно по адресу: http://localhost:3000"
    echo ""
    echo "Убедитесь, что backend сервер запущен на http://localhost:7033"
else
    echo "❌ Ошибка при установке зависимостей"
    exit 1
fi 