"use client";

import { DashboardLayout } from "@/components/layout/dashboard-layout";
import { useAuth } from "@/providers/auth-provider";
import { useDashboardStats, useRecentOrders } from "@/hooks/use-api";
import {
  UsersIcon,
  WrenchScrewdriverIcon,
  CurrencyDollarIcon,
  ChartBarIcon,
  ClockIcon,
} from "@heroicons/react/24/outline";

export default function DashboardPage() {
  const { user } = useAuth();
  
  // Используем мок данные вместо API
  const stats = {
    totalCustomers: 1234,
    totalCars: 2567,
    totalRepairOrders: 456,
    monthlyRevenue: 2400000,
    averageRating: 4.8,
    averageWaitTime: 2.3,
    pendingOrders: 45,
    inProgressOrders: 23,
    completedOrders: 388,
  };

  const recentOrders = [
    {
      id: "1",
      customerName: "Иван Петров",
      carInfo: "BMW X5 2020",
      serviceType: "Замена масла",
      status: "in_progress",
      createdAt: "2024-01-15",
    },
    {
      id: "2",
      customerName: "Мария Сидорова",
      carInfo: "Audi A4 2019",
      serviceType: "Ремонт тормозов",
      status: "completed",
      createdAt: "2024-01-14",
    },
    {
      id: "3",
      customerName: "Алексей Козлов",
      carInfo: "Mercedes C200 2021",
      serviceType: "Диагностика",
      status: "pending",
      createdAt: "2024-01-13",
    },
  ];

  const getGreeting = () => {
    const hour = new Date().getHours();
    if (hour < 12) return "Доброе утро";
    if (hour < 18) return "Добрый день";
    return "Добрый вечер";
  };

  const getStatusColor = (status: string) => {
    switch (status) {
      case "completed":
        return "bg-green-100 text-green-800";
      case "in_progress":
        return "bg-blue-100 text-blue-800";
      case "pending":
        return "bg-yellow-100 text-yellow-800";
      case "cancelled":
        return "bg-red-100 text-red-800";
      default:
        return "bg-gray-100 text-gray-800";
    }
  };

  const getStatusText = (status: string) => {
    switch (status) {
      case "completed":
        return "Завершен";
      case "in_progress":
        return "В работе";
      case "pending":
        return "Ожидает";
      case "cancelled":
        return "Отменен";
      default:
        return status;
    }
  };

  const statsCards = [
    { 
      name: "Всего клиентов", 
      value: stats.totalCustomers.toLocaleString(), 
      icon: UsersIcon, 
      change: "+12%", 
      changeType: "positive" 
    },
    { 
      name: "Автомобилей", 
      value: stats.totalCars.toLocaleString(), 
      icon: WrenchScrewdriverIcon, 
      change: "+8%", 
      changeType: "positive" 
    },
    { 
      name: "Заказов на ремонт", 
      value: stats.totalRepairOrders.toLocaleString(), 
              icon: WrenchScrewdriverIcon, 
      change: "+15%", 
      changeType: "positive" 
    },
    { 
      name: "Доход за месяц", 
      value: `₽${(stats.monthlyRevenue / 1000000).toFixed(1)}M`, 
      icon: CurrencyDollarIcon, 
      change: "+23%", 
      changeType: "positive" 
    },
    { 
      name: "Средний рейтинг", 
      value: stats.averageRating.toFixed(1), 
      icon: ChartBarIcon, 
      change: "+0.2", 
      changeType: "positive" 
    },
    { 
      name: "Время ожидания", 
      value: `${stats.averageWaitTime}ч`, 
      icon: ClockIcon, 
      change: "-0.5ч", 
      changeType: "negative" 
    },
  ];

  return (
    <DashboardLayout>
      <div className="space-y-6">
        {/* Header */}
        <div>
          <h1 className="text-2xl font-bold text-gray-900">
            {getGreeting()}, {user?.name}!
          </h1>
          <p className="text-gray-600">
            Добро пожаловать в панель управления ARM
          </p>
        </div>

        {/* Stats Grid */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {statsCards.map((stat) => (
            <div key={stat.name} className="bg-white rounded-lg shadow p-6">
              <div className="flex items-center">
                <div className="flex-shrink-0">
                  <stat.icon className="h-8 w-8 text-gray-400" />
                </div>
                <div className="ml-5 w-0 flex-1">
                  <dl>
                    <dt className="text-sm font-medium text-gray-500 truncate">
                      {stat.name}
                    </dt>
                    <dd className="flex items-baseline">
                      <div className="text-2xl font-semibold text-gray-900">
                        {stat.value}
                      </div>
                      <div className={`ml-2 flex items-baseline text-sm font-semibold ${
                        stat.changeType === "positive" ? "text-green-600" : "text-red-600"
                      }`}>
                        {stat.change}
                      </div>
                    </dd>
                  </dl>
                </div>
              </div>
            </div>
          ))}
        </div>

        {/* Recent Orders */}
        <div className="bg-white rounded-lg shadow">
          <div className="px-6 py-4 border-b border-gray-200">
            <h3 className="text-lg font-medium text-gray-900">
              Последние заказы
            </h3>
          </div>
          <div className="overflow-x-auto">
            <table className="min-w-full divide-y divide-gray-200">
              <thead className="bg-gray-50">
                <tr>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Клиент
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Автомобиль
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Услуга
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Статус
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Дата
                  </th>
                </tr>
              </thead>
              <tbody className="bg-white divide-y divide-gray-200">
                {recentOrders.map((order) => (
                  <tr key={order.id} className="hover:bg-gray-50">
                    <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">
                      {order.customerName}
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                      {order.carInfo}
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                      {order.serviceType}
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <span className={`inline-flex px-2 py-1 text-xs font-semibold rounded-full ${getStatusColor(order.status)}`}>
                        {getStatusText(order.status)}
                      </span>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                      {order.createdAt}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>

        {/* Additional Stats */}
        <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
          <div className="bg-white p-4 rounded-lg shadow">
            <div className="text-sm font-medium text-gray-500">Ожидают</div>
            <div className="text-2xl font-bold text-yellow-600">
              {stats.pendingOrders}
            </div>
          </div>
          <div className="bg-white p-4 rounded-lg shadow">
            <div className="text-sm font-medium text-gray-500">В работе</div>
            <div className="text-2xl font-bold text-blue-600">
              {stats.inProgressOrders}
            </div>
          </div>
          <div className="bg-white p-4 rounded-lg shadow">
            <div className="text-sm font-medium text-gray-500">Завершены</div>
            <div className="text-2xl font-bold text-green-600">
              {stats.completedOrders}
            </div>
          </div>
        </div>
      </div>
    </DashboardLayout>
  );
} 