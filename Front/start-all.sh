#!/bin/bash

# ARM Frontend Projects - Start All Script
# Запускает все фронтенд проекты одновременно

echo "🚀 Запуск всех ARM Frontend проектов..."

# Проверяем, что мы в правильной директории
if [ ! -d "Customer" ] || [ ! -d "Manager" ] || [ ! -d "Admin" ]; then
    echo "❌ Ошибка: Убедитесь, что вы находитесь в директории Front/"
    echo "Структура должна быть: Front/Customer, Front/Manager, Front/Admin"
    exit 1
fi

# Функция для установки зависимостей
install_dependencies() {
    local project=$1
    local port=$2
    
    echo "📦 Установка зависимостей для $project..."
    cd "$project"
    
    if [ ! -d "node_modules" ]; then
        npm install
        if [ $? -ne 0 ]; then
            echo "❌ Ошибка установки зависимостей для $project"
            exit 1
        fi
    else
        echo "✅ Зависимости для $project уже установлены"
    fi
    
    cd ..
}

# Функция для запуска проекта
start_project() {
    local project=$1
    local port=$2
    
    echo "🚀 Запуск $project на порту $port..."
    cd "$project"
    
    # Запускаем в фоне
    npm run dev -- -p "$port" &
    local pid=$!
    
    # Сохраняем PID для последующего завершения
    echo $pid > "../$project.pid"
    
    cd ..
    echo "✅ $project запущен (PID: $pid)"
}

# Устанавливаем зависимости для всех проектов
echo "📦 Установка зависимостей..."
install_dependencies "Customer" 3000
install_dependencies "Manager" 3001
install_dependencies "Admin" 3002

# Создаем .env.local файлы если их нет
create_env_file() {
    local project=$1
    local env_file="$project/.env.local"
    
    if [ ! -f "$env_file" ]; then
        echo "📝 Создание .env.local для $project..."
        cat > "$env_file" << EOF
# API URL
NEXT_PUBLIC_API_URL=http://localhost:5000/api

# App settings
NEXT_PUBLIC_APP_NAME=ARM $project
NEXT_PUBLIC_APP_VERSION=1.0.0
EOF
        echo "✅ .env.local создан для $project"
    fi
}

create_env_file "Customer"
create_env_file "Manager"
create_env_file "Admin"

# Запускаем все проекты
echo "🚀 Запуск проектов..."
start_project "Customer" 3000
sleep 2
start_project "Manager" 3001
sleep 2
start_project "Admin" 3002

# Ждем немного для запуска
sleep 5

echo ""
echo "🎉 Все проекты запущены!"
echo ""
echo "📱 Доступные приложения:"
echo "   🚗 Customer: http://localhost:3000"
echo "   👔 Manager:  http://localhost:3001"
echo "   🔧 Admin:    http://localhost:3002"
echo ""
echo "🔐 Демо доступ:"
echo "   - Email: любой"
echo "   - Пароль: любой"
echo ""
echo "🛑 Для остановки всех проектов выполните: ./stop-all.sh"
echo ""

# Функция для graceful shutdown
cleanup() {
    echo ""
    echo "🛑 Остановка всех проектов..."
    
    for project in "Customer" "Manager" "Admin"; do
        if [ -f "$project.pid" ]; then
            pid=$(cat "$project.pid")
            if kill -0 "$pid" 2>/dev/null; then
                echo "🛑 Остановка $project (PID: $pid)..."
                kill "$pid"
                rm "$project.pid"
            fi
        fi
    done
    
    echo "✅ Все проекты остановлены"
    exit 0
}

# Обработка сигналов для graceful shutdown
trap cleanup SIGINT SIGTERM

# Ждем завершения
echo "⏳ Проекты работают. Нажмите Ctrl+C для остановки..."
wait 