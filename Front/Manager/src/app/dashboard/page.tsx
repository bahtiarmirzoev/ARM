'use client';

import { useAuth } from '@/providers/auth-provider';
import { useRouter } from 'next/navigation';
import { useEffect } from 'react';
import { Users, Car, Wrench, DollarSign, TrendingUp, Clock, CheckCircle, AlertCircle } from 'lucide-react';
import { formatCurrency, formatDate } from '@/lib/utils';

// Mock data for manager dashboard
const mockData = {
  user: {
    firstName: 'Алексей',
    lastName: 'Петров',
    email: 'alexey@example.com',
    phone: '+7 (999) 123-45-67',
    role: 'manager',
  },
  stats: {
    totalCustomers: 156,
    totalCars: 234,
    activeRequests: 23,
    completedRequests: 89,
    totalRevenue: 2400000,
    monthlyRevenue: 450000,
  },
  recentRequests: [
    {
      id: '1',
      title: 'Замена масла и фильтров',
      customerName: 'Иван Иванов',
      carInfo: 'Toyota Camry 2020',
      status: 'in_progress',
      priority: 'medium',
      estimatedCost: 5000,
      createdAt: '2024-01-25',
    },
    {
      id: '2',
      title: 'Диагностика подвески',
      customerName: 'Петр Сидоров',
      carInfo: 'Honda Civic 2018',
      status: 'pending',
      priority: 'high',
      estimatedCost: 3000,
      createdAt: '2024-01-26',
    },
    {
      id: '3',
      title: 'Замена тормозных колодок',
      customerName: 'Анна Козлова',
      carInfo: 'BMW X5 2021',
      status: 'completed',
      priority: 'medium',
      estimatedCost: 8000,
      createdAt: '2024-01-24',
    },
  ],
  upcomingAppointments: [
    {
      id: '1',
      customerName: 'Иван Иванов',
      service: 'Техническое обслуживание',
      date: '2024-02-01',
      time: '10:00',
      car: 'Toyota Camry',
    },
    {
      id: '2',
      customerName: 'Петр Сидоров',
      service: 'Диагностика',
      date: '2024-02-01',
      time: '14:00',
      car: 'Honda Civic',
    },
  ],
  revenueChart: [
    { month: 'Янв', revenue: 1800000 },
    { month: 'Фев', revenue: 2100000 },
    { month: 'Мар', revenue: 1900000 },
    { month: 'Апр', revenue: 2400000 },
    { month: 'Май', revenue: 2200000 },
    { month: 'Июн', revenue: 2600000 },
  ],
};

export default function ManagerDashboard() {
  const { user, isAuthenticated, loading } = useAuth();
  const router = useRouter();

  useEffect(() => {
    if (!loading && !isAuthenticated) {
      router.push('/login');
    }
  }, [isAuthenticated, loading, router]);

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="animate-spin rounded-full h-32 w-32 border-b-2 border-green-600"></div>
      </div>
    );
  }

  if (!isAuthenticated) {
    return null;
  }

  return (
    <div className="min-h-screen bg-gray-50">
      {/* Header */}
      <header className="bg-white shadow-sm">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="flex justify-between items-center py-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <h1 className="text-2xl font-bold text-green-600">ARM Manager</h1>
              </div>
            </div>
            <div className="flex items-center space-x-4">
              <div className="text-right">
                <p className="text-sm font-medium text-gray-900">
                  {mockData.user.firstName} {mockData.user.lastName}
                </p>
                <p className="text-sm text-gray-500">{mockData.user.email}</p>
              </div>
              <button
                onClick={() => router.push('/')}
                className="text-gray-500 hover:text-gray-700 px-3 py-2 text-sm font-medium"
              >
                Выйти
              </button>
            </div>
          </div>
        </div>
      </header>

      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        {/* Welcome Section */}
        <div className="mb-8">
          <h1 className="text-3xl font-bold text-gray-900 mb-2">
            Добро пожаловать, {mockData.user.firstName}!
          </h1>
          <p className="text-gray-600">
            Обзор ключевых показателей и управление автосервисом
          </p>
        </div>

        {/* Stats Cards */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
          <div className="bg-white rounded-lg shadow p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <Users className="h-8 w-8 text-blue-600" />
              </div>
              <div className="ml-4">
                <p className="text-sm font-medium text-gray-500">Клиенты</p>
                <p className="text-2xl font-semibold text-gray-900">{mockData.stats.totalCustomers}</p>
              </div>
            </div>
          </div>

          <div className="bg-white rounded-lg shadow p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <Car className="h-8 w-8 text-green-600" />
              </div>
              <div className="ml-4">
                <p className="text-sm font-medium text-gray-500">Автомобили</p>
                <p className="text-2xl font-semibold text-gray-900">{mockData.stats.totalCars}</p>
              </div>
            </div>
          </div>

          <div className="bg-white rounded-lg shadow p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <Wrench className="h-8 w-8 text-orange-600" />
              </div>
              <div className="ml-4">
                <p className="text-sm font-medium text-gray-500">Активные заявки</p>
                <p className="text-2xl font-semibold text-gray-900">{mockData.stats.activeRequests}</p>
              </div>
            </div>
          </div>

          <div className="bg-white rounded-lg shadow p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <DollarSign className="h-8 w-8 text-purple-600" />
              </div>
              <div className="ml-4">
                <p className="text-sm font-medium text-gray-500">Выручка за месяц</p>
                <p className="text-2xl font-semibold text-gray-900">
                  {formatCurrency(mockData.stats.monthlyRevenue)}
                </p>
              </div>
            </div>
          </div>
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
          {/* Recent Service Requests */}
          <div className="bg-white rounded-lg shadow">
            <div className="px-6 py-4 border-b border-gray-200">
              <h2 className="text-lg font-semibold text-gray-900">Последние заявки</h2>
            </div>
            <div className="p-6">
              <div className="space-y-4">
                {mockData.recentRequests.map((request) => (
                  <div key={request.id} className="border border-gray-200 rounded-lg p-4">
                    <div className="flex justify-between items-start mb-2">
                      <h3 className="text-lg font-medium text-gray-900">{request.title}</h3>
                      <span className={`px-2 py-1 rounded-full text-xs font-medium ${
                        request.status === 'completed' ? 'bg-green-100 text-green-800' :
                        request.status === 'in_progress' ? 'bg-orange-100 text-orange-800' :
                        'bg-yellow-100 text-yellow-800'
                      }`}>
                        {request.status === 'completed' ? 'Завершено' :
                         request.status === 'in_progress' ? 'В работе' : 'Ожидает'}
                      </span>
                    </div>
                    <p className="text-sm text-gray-500 mb-2">
                      {request.customerName} • {request.carInfo}
                    </p>
                    <p className="text-sm text-gray-500 mb-2">
                      Создано: {formatDate(request.createdAt)}
                    </p>
                    <div className="flex justify-between items-center">
                      <p className="text-sm font-medium text-gray-900">
                        {formatCurrency(request.estimatedCost)}
                      </p>
                      <span className={`px-2 py-1 rounded-full text-xs font-medium ${
                        request.priority === 'high' ? 'bg-red-100 text-red-800' :
                        request.priority === 'medium' ? 'bg-yellow-100 text-yellow-800' :
                        'bg-green-100 text-green-800'
                      }`}>
                        {request.priority === 'high' ? 'Высокий' :
                         request.priority === 'medium' ? 'Средний' : 'Низкий'}
                      </span>
                    </div>
                  </div>
                ))}
              </div>
              <div className="mt-4">
                <button className="w-full bg-green-600 text-white py-2 rounded-md font-medium hover:bg-green-700">
                  Просмотреть все заявки
                </button>
              </div>
            </div>
          </div>

          {/* Upcoming Appointments */}
          <div className="bg-white rounded-lg shadow">
            <div className="px-6 py-4 border-b border-gray-200">
              <h2 className="text-lg font-semibold text-gray-900">Ближайшие записи</h2>
            </div>
            <div className="p-6">
              {mockData.upcomingAppointments.length > 0 ? (
                <div className="space-y-4">
                  {mockData.upcomingAppointments.map((appointment) => (
                    <div key={appointment.id} className="flex items-center justify-between p-4 border border-gray-200 rounded-lg">
                      <div className="flex items-center">
                        <Clock className="h-5 w-5 text-green-600 mr-3" />
                        <div>
                          <h3 className="font-medium text-gray-900">{appointment.service}</h3>
                          <p className="text-sm text-gray-500">
                            {appointment.customerName} • {appointment.car}
                          </p>
                          <p className="text-sm text-gray-500">
                            {formatDate(appointment.date)} в {appointment.time}
                          </p>
                        </div>
                      </div>
                      <button className="text-green-600 hover:text-green-800 text-sm font-medium">
                        Изменить
                      </button>
                    </div>
                  ))}
                </div>
              ) : (
                <div className="text-center py-8">
                  <Clock className="h-12 w-12 text-gray-400 mx-auto mb-4" />
                  <p className="text-gray-500">Нет запланированных записей</p>
                </div>
              )}
              <div className="mt-4">
                <button className="w-full border-2 border-dashed border-gray-300 rounded-lg py-4 text-gray-500 hover:border-gray-400 hover:text-gray-700">
                  + Добавить запись
                </button>
              </div>
            </div>
          </div>
        </div>

        {/* Revenue Chart */}
        <div className="mt-8 bg-white rounded-lg shadow">
          <div className="px-6 py-4 border-b border-gray-200">
            <h2 className="text-lg font-semibold text-gray-900">Динамика выручки</h2>
          </div>
          <div className="p-6">
            <div className="grid grid-cols-6 gap-4">
              {mockData.revenueChart.map((item, index) => (
                <div key={index} className="text-center">
                  <div className="bg-green-100 rounded-lg p-4 mb-2">
                    <div className="text-lg font-semibold text-green-600">
                      {formatCurrency(item.revenue)}
                    </div>
                  </div>
                  <div className="text-sm text-gray-600">{item.month}</div>
                </div>
              ))}
            </div>
          </div>
        </div>

        {/* Quick Actions */}
        <div className="mt-8 grid grid-cols-1 md:grid-cols-3 gap-6">
          <button className="bg-white rounded-lg shadow p-6 text-left hover:shadow-lg transition-shadow">
            <div className="flex items-center">
              <Users className="h-8 w-8 text-blue-600 mr-4" />
              <div>
                <h3 className="text-lg font-semibold text-gray-900">Управление клиентами</h3>
                <p className="text-sm text-gray-600">Добавить нового клиента</p>
              </div>
            </div>
          </button>

          <button className="bg-white rounded-lg shadow p-6 text-left hover:shadow-lg transition-shadow">
            <div className="flex items-center">
              <Wrench className="h-8 w-8 text-green-600 mr-4" />
              <div>
                <h3 className="text-lg font-semibold text-gray-900">Новая заявка</h3>
                <p className="text-sm text-gray-600">Создать заявку на обслуживание</p>
              </div>
            </div>
          </button>

          <button className="bg-white rounded-lg shadow p-6 text-left hover:shadow-lg transition-shadow">
            <div className="flex items-center">
              <TrendingUp className="h-8 w-8 text-purple-600 mr-4" />
              <div>
                <h3 className="text-lg font-semibold text-gray-900">Отчеты</h3>
                <p className="text-sm text-gray-600">Просмотреть аналитику</p>
              </div>
            </div>
          </button>
        </div>
      </div>
    </div>
  );
} 