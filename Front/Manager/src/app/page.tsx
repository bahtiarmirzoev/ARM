'use client';

import { useAuth } from '@/providers/auth-provider';
import { useRouter } from 'next/navigation';
import { useEffect } from 'react';
import { Car, Wrench, Users, BarChart3, ArrowRight, Shield, Clock, Star } from 'lucide-react';

export default function HomePage() {
  const { isAuthenticated, loading } = useAuth();
  const router = useRouter();

  useEffect(() => {
    if (!loading) {
      if (isAuthenticated) {
        router.push('/dashboard');
      }
    }
  }, [isAuthenticated, loading, router]);

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="animate-spin rounded-full h-32 w-32 border-b-2 border-green-600"></div>
      </div>
    );
  }

  return (
    <div className="min-h-screen">
      {/* Header */}
      <header className="bg-white shadow-sm">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="flex justify-between items-center py-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <h1 className="text-2xl font-bold text-green-600">ARM Manager</h1>
              </div>
              <div className="hidden md:block ml-10">
                <nav className="flex space-x-8">
                  <a href="#features" className="text-gray-500 hover:text-green-600 px-3 py-2 text-sm font-medium">
                    Возможности
                  </a>
                  <a href="#about" className="text-gray-500 hover:text-green-600 px-3 py-2 text-sm font-medium">
                    О системе
                  </a>
                  <a href="#contact" className="text-gray-500 hover:text-green-600 px-3 py-2 text-sm font-medium">
                    Поддержка
                  </a>
                </nav>
              </div>
            </div>
            <div className="flex items-center space-x-4">
              <button
                onClick={() => router.push('/login')}
                className="text-green-600 hover:text-green-800 px-3 py-2 text-sm font-medium"
              >
                Войти
              </button>
            </div>
          </div>
        </div>
      </header>

      {/* Hero Section */}
      <section className="relative bg-gradient-to-r from-green-600 to-emerald-700 text-white">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-24">
          <div className="text-center">
            <h1 className="text-4xl md:text-6xl font-bold mb-6">
              Панель управления автосервисом
            </h1>
            <p className="text-xl md:text-2xl mb-8 text-green-100">
              Эффективное управление клиентами, заявками и ремонтами
            </p>
            <div className="flex flex-col sm:flex-row gap-4 justify-center">
              <button
                onClick={() => router.push('/login')}
                className="bg-white text-green-600 hover:bg-gray-100 px-8 py-3 rounded-lg text-lg font-semibold transition-colors"
              >
                Войти в систему
              </button>
              <button
                onClick={() => router.push('/login')}
                className="border-2 border-white text-white hover:bg-white hover:text-green-600 px-8 py-3 rounded-lg text-lg font-semibold transition-colors"
              >
                Демо доступ
              </button>
            </div>
          </div>
        </div>
      </section>

      {/* Features Section */}
      <section id="features" className="py-20 bg-white">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="text-center mb-16">
            <h2 className="text-3xl font-bold text-gray-900 mb-4">
              Возможности системы
            </h2>
            <p className="text-xl text-gray-600">
              Все необходимые инструменты для эффективного управления автосервисом
            </p>
          </div>
          
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-8">
            <div className="text-center p-6">
              <div className="bg-green-100 w-16 h-16 rounded-full flex items-center justify-center mx-auto mb-4">
                <Users className="w-8 h-8 text-green-600" />
              </div>
              <h3 className="text-xl font-semibold text-gray-900 mb-2">
                Управление клиентами
              </h3>
              <p className="text-gray-600">
                Ведение базы клиентов, история обслуживания, контактная информация
              </p>
            </div>
            
            <div className="text-center p-6">
              <div className="bg-green-100 w-16 h-16 rounded-full flex items-center justify-center mx-auto mb-4">
                <Car className="w-8 h-8 text-green-600" />
              </div>
              <h3 className="text-xl font-semibold text-gray-900 mb-2">
                Автопарк клиентов
              </h3>
              <p className="text-gray-600">
                Учет автомобилей, технические характеристики, история ремонтов
              </p>
            </div>
            
            <div className="text-center p-6">
              <div className="bg-green-100 w-16 h-16 rounded-full flex items-center justify-center mx-auto mb-4">
                <Wrench className="w-8 h-8 text-green-600" />
              </div>
              <h3 className="text-xl font-semibold text-gray-900 mb-2">
                Заявки на обслуживание
              </h3>
              <p className="text-gray-600">
                Создание и отслеживание заявок, планирование работ, контроль статусов
              </p>
            </div>
            
            <div className="text-center p-6">
              <div className="bg-green-100 w-16 h-16 rounded-full flex items-center justify-center mx-auto mb-4">
                <BarChart3 className="w-8 h-8 text-green-600" />
              </div>
              <h3 className="text-xl font-semibold text-gray-900 mb-2">
                Аналитика и отчеты
              </h3>
              <p className="text-gray-600">
                Статистика продаж, анализ эффективности, финансовые отчеты
              </p>
            </div>
          </div>
        </div>
      </section>

      {/* Benefits Section */}
      <section className="py-20 bg-gray-50">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="text-center mb-16">
            <h2 className="text-3xl font-bold text-gray-900 mb-4">
              Преимущества системы
            </h2>
            <p className="text-xl text-gray-600">
              Почему стоит выбрать нашу систему управления
            </p>
          </div>
          
          <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
            <div className="bg-white rounded-lg shadow-md p-8">
              <div className="flex items-center mb-4">
                <Clock className="h-8 w-8 text-green-600 mr-3" />
                <h3 className="text-xl font-semibold text-gray-900">
                  Экономия времени
                </h3>
              </div>
              <p className="text-gray-600">
                Автоматизация рутинных процессов позволяет сосредоточиться на важных задачах
              </p>
            </div>
            
            <div className="bg-white rounded-lg shadow-md p-8">
              <div className="flex items-center mb-4">
                <Shield className="h-8 w-8 text-green-600 mr-3" />
                <h3 className="text-xl font-semibold text-gray-900">
                  Безопасность данных
                </h3>
              </div>
              <p className="text-gray-600">
                Надежная защита информации клиентов и бизнес-данных
              </p>
            </div>
            
            <div className="bg-white rounded-lg shadow-md p-8">
              <div className="flex items-center mb-4">
                <Star className="h-8 w-8 text-green-600 mr-3" />
                <h3 className="text-xl font-semibold text-gray-900">
                  Простота использования
                </h3>
              </div>
              <p className="text-gray-600">
                Интуитивно понятный интерфейс, не требующий специального обучения
              </p>
            </div>
          </div>
        </div>
      </section>

      {/* Dashboard Preview */}
      <section className="py-20 bg-white">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="text-center mb-16">
            <h2 className="text-3xl font-bold text-gray-900 mb-4">
              Современный интерфейс
            </h2>
            <p className="text-xl text-gray-600">
              Удобная панель управления с актуальной информацией
            </p>
          </div>
          
          <div className="bg-gray-100 rounded-lg p-8">
            <div className="grid grid-cols-1 md:grid-cols-4 gap-6 mb-8">
              <div className="bg-white rounded-lg p-6 text-center">
                <div className="text-2xl font-bold text-green-600 mb-2">156</div>
                <div className="text-sm text-gray-600">Активных клиентов</div>
              </div>
              <div className="bg-white rounded-lg p-6 text-center">
                <div className="text-2xl font-bold text-blue-600 mb-2">23</div>
                <div className="text-sm text-gray-600">Заявок в работе</div>
              </div>
              <div className="bg-white rounded-lg p-6 text-center">
                <div className="text-2xl font-bold text-orange-600 mb-2">89</div>
                <div className="text-sm text-gray-600">Завершенных работ</div>
              </div>
              <div className="bg-white rounded-lg p-6 text-center">
                <div className="text-2xl font-bold text-purple-600 mb-2">₽2.4M</div>
                <div className="text-sm text-gray-600">Выручка за месяц</div>
              </div>
            </div>
            
            <div className="text-center">
              <button
                onClick={() => router.push('/login')}
                className="bg-green-600 text-white px-8 py-3 rounded-lg text-lg font-semibold hover:bg-green-700 transition-colors"
              >
                Попробовать демо
              </button>
            </div>
          </div>
        </div>
      </section>

      {/* CTA Section */}
      <section className="py-20 bg-green-600">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 text-center">
          <h2 className="text-3xl font-bold text-white mb-4">
            Готовы начать работу?
          </h2>
          <p className="text-xl text-green-100 mb-8">
            Войдите в систему и начните эффективно управлять автосервисом
          </p>
          <button
            onClick={() => router.push('/login')}
            className="bg-white text-green-600 hover:bg-gray-100 px-8 py-3 rounded-lg text-lg font-semibold transition-colors"
          >
            Войти в систему
          </button>
        </div>
      </section>

      {/* Footer */}
      <footer className="bg-gray-900 text-white py-12">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="grid grid-cols-1 md:grid-cols-4 gap-8">
            <div>
              <h3 className="text-lg font-semibold mb-4">ARM Manager</h3>
              <p className="text-gray-400">
                Система управления автосервисом для менеджеров и администраторов
              </p>
            </div>
            <div>
              <h4 className="text-lg font-semibold mb-4">Функции</h4>
              <ul className="space-y-2 text-gray-400">
                <li>Управление клиентами</li>
                <li>Заявки на обслуживание</li>
                <li>Аналитика и отчеты</li>
                <li>Планирование работ</li>
              </ul>
            </div>
            <div>
              <h4 className="text-lg font-semibold mb-4">Поддержка</h4>
              <ul className="space-y-2 text-gray-400">
                <li>+7 (999) 123-45-67</li>
                <li>support@arm-manager.ru</li>
                <li>Документация</li>
                <li>Обучение</li>
              </ul>
            </div>
            <div>
              <h4 className="text-lg font-semibold mb-4">Режим работы</h4>
              <ul className="space-y-2 text-gray-400">
                <li>Пн-Пт: 9:00 - 18:00</li>
                <li>Сб: 10:00 - 16:00</li>
                <li>Вс: Выходной</li>
              </ul>
            </div>
          </div>
          <div className="border-t border-gray-800 mt-8 pt-8 text-center text-gray-400">
            <p>&copy; 2024 ARM Manager. Все права защищены.</p>
          </div>
        </div>
      </footer>
    </div>
  );
}
