#!/bin/bash

# ARM Frontend Projects - Stop All Script
# Останавливает все запущенные фронтенд проекты

echo "🛑 Остановка всех ARM Frontend проектов..."

# Проверяем, что мы в правильной директории
if [ ! -d "Customer" ] || [ ! -d "Manager" ] || [ ! -d "Admin" ]; then
    echo "❌ Ошибка: Убедитесь, что вы находитесь в директории Front/"
    echo "Структура должна быть: Front/Customer, Front/Manager, Front/Admin"
    exit 1
fi

# Функция для остановки проекта
stop_project() {
    local project=$1
    local pid_file="$project.pid"
    
    if [ -f "$pid_file" ]; then
        local pid=$(cat "$pid_file")
        if kill -0 "$pid" 2>/dev/null; then
            echo "🛑 Остановка $project (PID: $pid)..."
            kill "$pid"
            rm "$pid_file"
            echo "✅ $project остановлен"
        else
            echo "⚠️  $project уже не запущен"
            rm "$pid_file"
        fi
    else
        echo "⚠️  PID файл для $project не найден"
    fi
}

# Останавливаем все проекты
stop_project "Customer"
stop_project "Manager"
stop_project "Admin"

# Дополнительно убиваем процессы на портах если они остались
echo "🔍 Проверка процессов на портах..."

for port in 3000 3001 3002; do
    pid=$(lsof -ti:$port 2>/dev/null)
    if [ ! -z "$pid" ]; then
        echo "🛑 Остановка процесса на порту $port (PID: $pid)..."
        kill -9 "$pid"
        echo "✅ Процесс на порту $port остановлен"
    fi
done

echo ""
echo "🎉 Все проекты остановлены!"
echo ""
echo "📝 Для запуска всех проектов выполните: ./start-all.sh" 