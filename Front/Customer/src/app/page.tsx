'use client';

import { useAuth } from '@/providers/auth-provider';
import { useRouter } from 'next/navigation';
import { useEffect } from 'react';
import { Car, Wrench, Clock, Star, ArrowRight } from 'lucide-react';

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
        <div className="animate-spin rounded-full h-32 w-32 border-b-2 border-blue-600"></div>
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
                <h1 className="text-2xl font-bold text-blue-600">ARM</h1>
              </div>
              <div className="hidden md:block ml-10">
                <nav className="flex space-x-8">
                  <a href="#services" className="text-gray-500 hover:text-blue-600 px-3 py-2 text-sm font-medium">
                    Услуги
                  </a>
                  <a href="#about" className="text-gray-500 hover:text-blue-600 px-3 py-2 text-sm font-medium">
                    О нас
                  </a>
                  <a href="#contact" className="text-gray-500 hover:text-blue-600 px-3 py-2 text-sm font-medium">
                    Контакты
                  </a>
                </nav>
              </div>
            </div>
            <div className="flex items-center space-x-4">
              <button
                onClick={() => router.push('/login')}
                className="text-blue-600 hover:text-blue-800 px-3 py-2 text-sm font-medium"
              >
                Войти
              </button>
              <button
                onClick={() => router.push('/register')}
                className="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-md text-sm font-medium"
              >
                Регистрация
              </button>
            </div>
          </div>
        </div>
      </header>

      {/* Hero Section */}
      <section className="relative bg-gradient-to-r from-blue-600 to-indigo-700 text-white">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-24">
          <div className="text-center">
            <h1 className="text-4xl md:text-6xl font-bold mb-6">
              Профессиональный автосервис
            </h1>
            <p className="text-xl md:text-2xl mb-8 text-blue-100">
              Заботимся о вашем автомобиле с 2010 года
            </p>
            <div className="flex flex-col sm:flex-row gap-4 justify-center">
              <button
                onClick={() => router.push('/register')}
                className="bg-white text-blue-600 hover:bg-gray-100 px-8 py-3 rounded-lg text-lg font-semibold transition-colors"
              >
                Записаться на сервис
              </button>
              <button
                onClick={() => router.push('/login')}
                className="border-2 border-white text-white hover:bg-white hover:text-blue-600 px-8 py-3 rounded-lg text-lg font-semibold transition-colors"
              >
                Личный кабинет
              </button>
            </div>
          </div>
        </div>
      </section>

      {/* Features Section */}
      <section className="py-20 bg-white">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="text-center mb-16">
            <h2 className="text-3xl font-bold text-gray-900 mb-4">
              Почему выбирают нас
            </h2>
            <p className="text-xl text-gray-600">
              Мы предлагаем качественные услуги и заботимся о каждом клиенте
            </p>
          </div>
          
          <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
            <div className="text-center p-6">
              <div className="bg-blue-100 w-16 h-16 rounded-full flex items-center justify-center mx-auto mb-4">
                <Wrench className="w-8 h-8 text-blue-600" />
              </div>
              <h3 className="text-xl font-semibold text-gray-900 mb-2">
                Профессиональные мастера
              </h3>
              <p className="text-gray-600">
                Наши специалисты имеют многолетний опыт работы с автомобилями всех марок
              </p>
            </div>
            
            <div className="text-center p-6">
              <div className="bg-blue-100 w-16 h-16 rounded-full flex items-center justify-center mx-auto mb-4">
                <Clock className="w-8 h-8 text-blue-600" />
              </div>
              <h3 className="text-xl font-semibold text-gray-900 mb-2">
                Быстрое обслуживание
              </h3>
              <p className="text-gray-600">
                Выполняем работы в кратчайшие сроки без потери качества
              </p>
            </div>
            
            <div className="text-center p-6">
              <div className="bg-blue-100 w-16 h-16 rounded-full flex items-center justify-center mx-auto mb-4">
                <Star className="w-8 h-8 text-blue-600" />
              </div>
              <h3 className="text-xl font-semibold text-gray-900 mb-2">
                Гарантия качества
              </h3>
              <p className="text-gray-600">
                Предоставляем гарантию на все виды работ и используемые запчасти
              </p>
            </div>
          </div>
        </div>
      </section>

      {/* Services Section */}
      <section id="services" className="py-20 bg-gray-50">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="text-center mb-16">
            <h2 className="text-3xl font-bold text-gray-900 mb-4">
              Наши услуги
            </h2>
            <p className="text-xl text-gray-600">
              Полный спектр услуг по обслуживанию и ремонту автомобилей
            </p>
          </div>
          
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
            {[
              { title: 'Техническое обслуживание', description: 'Плановое ТО, замена масла, фильтров' },
              { title: 'Диагностика', description: 'Компьютерная диагностика всех систем автомобиля' },
              { title: 'Ремонт двигателя', description: 'Капитальный и текущий ремонт двигателей' },
              { title: 'Ремонт подвески', description: 'Диагностика и ремонт ходовой части' },
              { title: 'Электрика', description: 'Ремонт электрооборудования и проводки' },
              { title: 'Кузовной ремонт', description: 'Покраска, рихтовка, замена деталей кузова' },
            ].map((service, index) => (
              <div key={index} className="bg-white rounded-lg shadow-md p-6 hover:shadow-lg transition-shadow">
                <div className="bg-blue-100 w-12 h-12 rounded-lg flex items-center justify-center mb-4">
                  <Car className="w-6 h-6 text-blue-600" />
                </div>
                <h3 className="text-lg font-semibold text-gray-900 mb-2">
                  {service.title}
                </h3>
                <p className="text-gray-600 mb-4">
                  {service.description}
                </p>
                <button className="text-blue-600 hover:text-blue-800 font-medium flex items-center">
                  Подробнее
                  <ArrowRight className="w-4 h-4 ml-1" />
                </button>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* CTA Section */}
      <section className="py-20 bg-blue-600">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 text-center">
          <h2 className="text-3xl font-bold text-white mb-4">
            Готовы записаться на сервис?
          </h2>
          <p className="text-xl text-blue-100 mb-8">
            Создайте аккаунт и получите доступ к личному кабинету
          </p>
          <button
            onClick={() => router.push('/register')}
            className="bg-white text-blue-600 hover:bg-gray-100 px-8 py-3 rounded-lg text-lg font-semibold transition-colors"
          >
            Создать аккаунт
          </button>
        </div>
      </section>

      {/* Footer */}
      <footer className="bg-gray-900 text-white py-12">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="grid grid-cols-1 md:grid-cols-4 gap-8">
            <div>
              <h3 className="text-lg font-semibold mb-4">ARM</h3>
              <p className="text-gray-400">
                Профессиональный автосервис с многолетним опытом работы
              </p>
            </div>
            <div>
              <h4 className="text-lg font-semibold mb-4">Услуги</h4>
              <ul className="space-y-2 text-gray-400">
                <li>Техническое обслуживание</li>
                <li>Диагностика</li>
                <li>Ремонт двигателя</li>
                <li>Ремонт подвески</li>
              </ul>
            </div>
            <div>
              <h4 className="text-lg font-semibold mb-4">Контакты</h4>
              <ul className="space-y-2 text-gray-400">
                <li>+7 (999) 123-45-67</li>
                <li>info@arm-service.ru</li>
                <li>г. Москва, ул. Автосервисная, 1</li>
              </ul>
            </div>
            <div>
              <h4 className="text-lg font-semibold mb-4">Режим работы</h4>
              <ul className="space-y-2 text-gray-400">
                <li>Пн-Пт: 8:00 - 20:00</li>
                <li>Сб: 9:00 - 18:00</li>
                <li>Вс: 10:00 - 16:00</li>
              </ul>
            </div>
          </div>
          <div className="border-t border-gray-800 mt-8 pt-8 text-center text-gray-400">
            <p>&copy; 2024 ARM. Все права защищены.</p>
          </div>
        </div>
      </footer>
    </div>
  );
}
