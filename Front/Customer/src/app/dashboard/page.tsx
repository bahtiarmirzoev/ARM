'use client';

import { useAuth } from '@/providers/auth-provider';
import { useRouter } from 'next/navigation';
import { useEffect } from 'react';
import { Car, Wrench, Clock, CheckCircle, AlertCircle, DollarSign, Calendar } from 'lucide-react';
import { formatCurrency, formatDate } from '@/lib/utils';

// Mock data for customer dashboard
const mockData = {
  user: {
    firstName: 'Иван',
    lastName: 'Иванов',
    email: 'ivan@example.com',
    phone: '+7 (999) 123-45-67',
  },
  cars: [
    {
      id: '1',
      brand: 'Toyota',
      model: 'Camry',
      year: 2020,
      licensePlate: 'А123БВ77',
      mileage: 45000,
    },
    {
      id: '2',
      brand: 'Honda',
      model: 'Civic',
      year: 2018,
      licensePlate: 'В456ГД77',
      mileage: 78000,
    },
  ],
  serviceRequests: [
    {
      id: '1',
      title: 'Замена масла и фильтров',
      status: 'completed',
      priority: 'medium',
      estimatedCost: 5000,
      actualCost: 4800,
      carId: '1',
      createdAt: '2024-01-15',
      completedAt: '2024-01-16',
    },
    {
      id: '2',
      title: 'Диагностика подвески',
      status: 'in_progress',
      priority: 'high',
      estimatedCost: 3000,
      carId: '2',
      createdAt: '2024-01-20',
    },
    {
      id: '3',
      title: 'Замена тормозных колодок',
      status: 'pending',
      priority: 'medium',
      estimatedCost: 8000,
      carId: '1',
      createdAt: '2024-01-25',
    },
  ],
  upcomingAppointments: [
    {
      id: '1',
      service: 'Техническое обслуживание',
      date: '2024-02-01',
      time: '10:00',
      car: 'Toyota Camry',
    },
  ],
  totalSpent: 4800,
  totalServices: 3,
  activeRequests: 2,
};

export default function CustomerDashboard() {
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
        <div className="animate-spin rounded-full h-32 w-32 border-b-2 border-blue-600"></div>
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
                <h1 className="text-2xl font-bold text-blue-600">ARM</h1>
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
            Управляйте своими автомобилями и заявками на обслуживание
          </p>
        </div>

        {/* Stats Cards */}
        <div className="grid grid-cols-1 md:grid-cols-4 gap-6 mb-8">
          <div className="bg-white rounded-lg shadow p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <Car className="h-8 w-8 text-blue-600" />
              </div>
              <div className="ml-4">
                <p className="text-sm font-medium text-gray-500">Автомобили</p>
                <p className="text-2xl font-semibold text-gray-900">{mockData.cars.length}</p>
              </div>
            </div>
          </div>

          <div className="bg-white rounded-lg shadow p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <Wrench className="h-8 w-8 text-green-600" />
              </div>
              <div className="ml-4">
                <p className="text-sm font-medium text-gray-500">Всего услуг</p>
                <p className="text-2xl font-semibold text-gray-900">{mockData.totalServices}</p>
              </div>
            </div>
          </div>

          <div className="bg-white rounded-lg shadow p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <Clock className="h-8 w-8 text-orange-600" />
              </div>
              <div className="ml-4">
                <p className="text-sm font-medium text-gray-500">Активные заявки</p>
                <p className="text-2xl font-semibold text-gray-900">{mockData.activeRequests}</p>
              </div>
            </div>
          </div>

          <div className="bg-white rounded-lg shadow p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <DollarSign className="h-8 w-8 text-green-600" />
              </div>
              <div className="ml-4">
                <p className="text-sm font-medium text-gray-500">Потрачено</p>
                <p className="text-2xl font-semibold text-gray-900">
                  {formatCurrency(mockData.totalSpent)}
                </p>
              </div>
            </div>
          </div>
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
          {/* My Cars */}
          <div className="bg-white rounded-lg shadow">
            <div className="px-6 py-4 border-b border-gray-200">
              <h2 className="text-lg font-semibold text-gray-900">Мои автомобили</h2>
            </div>
            <div className="p-6">
              <div className="space-y-4">
                {mockData.cars.map((car) => (
                  <div key={car.id} className="border border-gray-200 rounded-lg p-4">
                    <div className="flex justify-between items-start">
                      <div>
                        <h3 className="text-lg font-medium text-gray-900">
                          {car.brand} {car.model}
                        </h3>
                        <p className="text-sm text-gray-500">
                          {car.year} • {car.licensePlate}
                        </p>
                        <p className="text-sm text-gray-500">
                          Пробег: {car.mileage.toLocaleString()} км
                        </p>
                      </div>
                      <button className="bg-blue-600 text-white px-4 py-2 rounded-md text-sm font-medium hover:bg-blue-700">
                        Записаться
                      </button>
                    </div>
                  </div>
                ))}
              </div>
              <div className="mt-4">
                <button className="w-full border-2 border-dashed border-gray-300 rounded-lg py-4 text-gray-500 hover:border-gray-400 hover:text-gray-700">
                  + Добавить автомобиль
                </button>
              </div>
            </div>
          </div>

          {/* Recent Service Requests */}
          <div className="bg-white rounded-lg shadow">
            <div className="px-6 py-4 border-b border-gray-200">
              <h2 className="text-lg font-semibold text-gray-900">Последние заявки</h2>
            </div>
            <div className="p-6">
              <div className="space-y-4">
                {mockData.serviceRequests.slice(0, 3).map((request) => (
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
                      Создано: {formatDate(request.createdAt)}
                    </p>
                    <p className="text-sm font-medium text-gray-900">
                      Стоимость: {formatCurrency(request.estimatedCost)}
                    </p>
                  </div>
                ))}
              </div>
              <div className="mt-4">
                <button className="w-full bg-blue-600 text-white py-2 rounded-md font-medium hover:bg-blue-700">
                  Создать новую заявку
                </button>
              </div>
            </div>
          </div>
        </div>

        {/* Upcoming Appointments */}
        <div className="mt-8 bg-white rounded-lg shadow">
          <div className="px-6 py-4 border-b border-gray-200">
            <h2 className="text-lg font-semibold text-gray-900">Ближайшие записи</h2>
          </div>
          <div className="p-6">
            {mockData.upcomingAppointments.length > 0 ? (
              <div className="space-y-4">
                {mockData.upcomingAppointments.map((appointment) => (
                  <div key={appointment.id} className="flex items-center justify-between p-4 border border-gray-200 rounded-lg">
                    <div className="flex items-center">
                      <Calendar className="h-5 w-5 text-blue-600 mr-3" />
                      <div>
                        <h3 className="font-medium text-gray-900">{appointment.service}</h3>
                        <p className="text-sm text-gray-500">
                          {appointment.car} • {formatDate(appointment.date)} в {appointment.time}
                        </p>
                      </div>
                    </div>
                    <button className="text-blue-600 hover:text-blue-800 text-sm font-medium">
                      Изменить
                    </button>
                  </div>
                ))}
              </div>
            ) : (
              <div className="text-center py-8">
                <Calendar className="h-12 w-12 text-gray-400 mx-auto mb-4" />
                <p className="text-gray-500">Нет запланированных записей</p>
                <button className="mt-2 text-blue-600 hover:text-blue-800 font-medium">
                  Записаться на сервис
                </button>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
} 