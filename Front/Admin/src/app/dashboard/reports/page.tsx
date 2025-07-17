"use client";

import { useState } from "react";
import { DashboardLayout } from "@/components/layout/dashboard-layout";
import { Button } from "@/components/ui/button";
import {
  ChartBarIcon,
  CalendarIcon,
  CurrencyDollarIcon,
  UsersIcon,
  WrenchScrewdriverIcon,
  DocumentTextIcon,
} from "@heroicons/react/24/outline";

export default function ReportsPage() {
  const [selectedPeriod, setSelectedPeriod] = useState("month");
  const [selectedReport, setSelectedReport] = useState("revenue");

  // Мок данные для отчетов
  const mockData = {
    revenue: {
      title: "Доходы",
      total: "₽2,400,000",
      change: "+23%",
      changeType: "positive",
      data: [
        { month: "Янв", value: 1800000 },
        { month: "Фев", value: 2100000 },
        { month: "Мар", value: 1950000 },
        { month: "Апр", value: 2200000 },
        { month: "Май", value: 2400000 },
        { month: "Июн", value: 2600000 },
      ]
    },
    orders: {
      title: "Заказы",
      total: "456",
      change: "+15%",
      changeType: "positive",
      data: [
        { month: "Янв", value: 320 },
        { month: "Фев", value: 380 },
        { month: "Мар", value: 350 },
        { month: "Апр", value: 420 },
        { month: "Май", value: 456 },
        { month: "Июн", value: 480 },
      ]
    },
    customers: {
      title: "Клиенты",
      total: "1,234",
      change: "+12%",
      changeType: "positive",
      data: [
        { month: "Янв", value: 980 },
        { month: "Фев", value: 1050 },
        { month: "Мар", value: 1120 },
        { month: "Апр", value: 1180 },
        { month: "Май", value: 1234 },
        { month: "Июн", value: 1280 },
      ]
    },
    services: {
      title: "Популярные услуги",
      data: [
        { name: "Замена масла", count: 156, revenue: 390000 },
        { name: "Диагностика", count: 89, revenue: 133500 },
        { name: "Замена тормозных колодок", count: 67, revenue: 536000 },
        { name: "Замена ремня ГРМ", count: 34, revenue: 510000 },
        { name: "Покраска", count: 23, revenue: 276000 },
      ]
    }
  };

  const renderBarChart = (data: any[]) => {
    const maxValue = Math.max(...data.map(d => d.value));
    
    return (
      <div className="space-y-3">
        {data.map((item, index) => (
          <div key={index} className="flex items-center space-x-3">
            <div className="w-12 text-sm text-gray-600">{item.month}</div>
            <div className="flex-1">
              <div className="bg-gray-200 rounded-full h-4">
                <div 
                  className="bg-blue-600 h-4 rounded-full transition-all duration-300"
                  style={{ width: `${(item.value / maxValue) * 100}%` }}
                />
              </div>
            </div>
            <div className="w-16 text-sm text-gray-900 text-right">
              {item.value.toLocaleString()}
            </div>
          </div>
        ))}
      </div>
    );
  };

  const renderServiceTable = (services: any[]) => {
    return (
      <div className="overflow-x-auto">
        <table className="min-w-full divide-y divide-gray-200">
          <thead className="bg-gray-50">
            <tr>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Услуга
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Количество
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Доход
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Доля
              </th>
            </tr>
          </thead>
          <tbody className="bg-white divide-y divide-gray-200">
            {services.map((service, index) => {
              const totalRevenue = services.reduce((sum, s) => sum + s.revenue, 0);
              const percentage = ((service.revenue / totalRevenue) * 100).toFixed(1);
              
              return (
                <tr key={index} className="hover:bg-gray-50">
                  <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">
                    {service.name}
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                    {service.count}
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                    ₽{service.revenue.toLocaleString()}
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap">
                    <div className="flex items-center">
                      <div className="flex-1 bg-gray-200 rounded-full h-2 mr-2">
                        <div 
                          className="bg-blue-600 h-2 rounded-full"
                          style={{ width: `${percentage}%` }}
                        />
                      </div>
                      <span className="text-sm text-gray-600">{percentage}%</span>
                    </div>
                  </td>
                </tr>
              );
            })}
          </tbody>
        </table>
      </div>
    );
  };

  return (
    <DashboardLayout>
      <div className="space-y-6">
        {/* Header */}
        <div className="flex justify-between items-center">
          <div>
            <h1 className="text-2xl font-bold text-gray-900">Отчеты</h1>
            <p className="text-gray-600">
              Аналитика и отчеты по работе автосервиса
            </p>
          </div>
          <div className="flex space-x-2">
            <select
              value={selectedPeriod}
              onChange={(e) => setSelectedPeriod(e.target.value)}
              className="px-4 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 focus:border-transparent"
            >
              <option value="week">Неделя</option>
              <option value="month">Месяц</option>
              <option value="quarter">Квартал</option>
              <option value="year">Год</option>
            </select>
            <Button className="flex items-center gap-2">
              <DocumentTextIcon className="h-4 w-4" />
              Экспорт
            </Button>
          </div>
        </div>

        {/* Report Type Selector */}
        <div className="bg-white rounded-lg shadow p-6">
          <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
            <button
              onClick={() => setSelectedReport("revenue")}
              className={`p-4 rounded-lg border-2 transition-colors ${
                selectedReport === "revenue" 
                  ? "border-blue-500 bg-blue-50" 
                  : "border-gray-200 hover:border-gray-300"
              }`}
            >
              <div className="flex items-center">
                <CurrencyDollarIcon className="h-8 w-8 text-blue-600" />
                <div className="ml-3">
                  <div className="text-sm font-medium text-gray-900">Доходы</div>
                  <div className="text-xs text-gray-500">Финансовая аналитика</div>
                </div>
              </div>
            </button>
            
            <button
              onClick={() => setSelectedReport("orders")}
              className={`p-4 rounded-lg border-2 transition-colors ${
                selectedReport === "orders" 
                  ? "border-blue-500 bg-blue-50" 
                  : "border-gray-200 hover:border-gray-300"
              }`}
            >
              <div className="flex items-center">
                <WrenchScrewdriverIcon className="h-8 w-8 text-green-600" />
                <div className="ml-3">
                  <div className="text-sm font-medium text-gray-900">Заказы</div>
                  <div className="text-xs text-gray-500">Статистика заказов</div>
                </div>
              </div>
            </button>
            
            <button
              onClick={() => setSelectedReport("customers")}
              className={`p-4 rounded-lg border-2 transition-colors ${
                selectedReport === "customers" 
                  ? "border-blue-500 bg-blue-50" 
                  : "border-gray-200 hover:border-gray-300"
              }`}
            >
              <div className="flex items-center">
                <UsersIcon className="h-8 w-8 text-purple-600" />
                <div className="ml-3">
                  <div className="text-sm font-medium text-gray-900">Клиенты</div>
                  <div className="text-xs text-gray-500">Анализ клиентов</div>
                </div>
              </div>
            </button>
            
            <button
              onClick={() => setSelectedReport("services")}
              className={`p-4 rounded-lg border-2 transition-colors ${
                selectedReport === "services" 
                  ? "border-blue-500 bg-blue-50" 
                  : "border-gray-200 hover:border-gray-300"
              }`}
            >
              <div className="flex items-center">
                <ChartBarIcon className="h-8 w-8 text-orange-600" />
                <div className="ml-3">
                  <div className="text-sm font-medium text-gray-900">Услуги</div>
                  <div className="text-xs text-gray-500">Популярные услуги</div>
                </div>
              </div>
            </button>
          </div>
        </div>

        {/* Report Content */}
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
          {/* Main Chart */}
          <div className="lg:col-span-2 bg-white rounded-lg shadow p-6">
            <div className="flex items-center justify-between mb-6">
              <div>
                <h3 className="text-lg font-medium text-gray-900">
                  {mockData[selectedReport as keyof typeof mockData].title}
                </h3>
                <p className="text-sm text-gray-500">
                  За последние 6 месяцев
                </p>
              </div>
                           <div className="text-right">
               {selectedReport !== "services" && (
                 <>
                   <div className="text-2xl font-bold text-gray-900">
                     {(mockData[selectedReport as 'revenue' | 'orders' | 'customers'] as any).total}
                   </div>
                   <div className={`text-sm ${
                     (mockData[selectedReport as 'revenue' | 'orders' | 'customers'] as any).changeType === "positive" 
                       ? "text-green-600" 
                       : "text-red-600"
                   }`}>
                     {(mockData[selectedReport as 'revenue' | 'orders' | 'customers'] as any).change}
                   </div>
                 </>
               )}
               {selectedReport === "services" && (
                 <div className="text-2xl font-bold text-gray-900">
                   {mockData.services.data.length} услуг
                 </div>
               )}
             </div>
            </div>
            
            {selectedReport !== "services" && (
              <div className="h-64">
                {renderBarChart(mockData[selectedReport as keyof typeof mockData].data)}
              </div>
            )}
            
            {selectedReport === "services" && (
              <div>
                {renderServiceTable(mockData.services.data)}
              </div>
            )}
          </div>

          {/* Sidebar Stats */}
          <div className="space-y-6">
            {/* Quick Stats */}
            <div className="bg-white rounded-lg shadow p-6">
              <h3 className="text-lg font-medium text-gray-900 mb-4">Быстрая статистика</h3>
              <div className="space-y-4">
                <div className="flex items-center justify-between">
                  <div className="flex items-center">
                    <CurrencyDollarIcon className="h-5 w-5 text-green-600" />
                    <span className="ml-2 text-sm text-gray-600">Средний чек</span>
                  </div>
                  <span className="text-sm font-medium text-gray-900">₽5,263</span>
                </div>
                <div className="flex items-center justify-between">
                  <div className="flex items-center">
                    <UsersIcon className="h-5 w-5 text-blue-600" />
                    <span className="ml-2 text-sm text-gray-600">Новых клиентов</span>
                  </div>
                  <span className="text-sm font-medium text-gray-900">46</span>
                </div>
                <div className="flex items-center justify-between">
                  <div className="flex items-center">
                    <WrenchScrewdriverIcon className="h-5 w-5 text-purple-600" />
                    <span className="ml-2 text-sm text-gray-600">Заказов в работе</span>
                  </div>
                  <span className="text-sm font-medium text-gray-900">23</span>
                </div>
                <div className="flex items-center justify-between">
                  <div className="flex items-center">
                    <CalendarIcon className="h-5 w-5 text-orange-600" />
                    <span className="ml-2 text-sm text-gray-600">Среднее время</span>
                  </div>
                  <span className="text-sm font-medium text-gray-900">2.3ч</span>
                </div>
              </div>
            </div>

            {/* Recent Activity */}
            <div className="bg-white rounded-lg shadow p-6">
              <h3 className="text-lg font-medium text-gray-900 mb-4">Последняя активность</h3>
              <div className="space-y-3">
                <div className="flex items-start space-x-3">
                  <div className="flex-shrink-0">
                    <div className="h-2 w-2 bg-green-400 rounded-full mt-2"></div>
                  </div>
                  <div className="flex-1 min-w-0">
                    <p className="text-sm text-gray-900">Новый заказ #1234</p>
                    <p className="text-xs text-gray-500">2 минуты назад</p>
                  </div>
                </div>
                <div className="flex items-start space-x-3">
                  <div className="flex-shrink-0">
                    <div className="h-2 w-2 bg-blue-400 rounded-full mt-2"></div>
                  </div>
                  <div className="flex-1 min-w-0">
                    <p className="text-sm text-gray-900">Заказ #1230 завершен</p>
                    <p className="text-xs text-gray-500">15 минут назад</p>
                  </div>
                </div>
                <div className="flex items-start space-x-3">
                  <div className="flex-shrink-0">
                    <div className="h-2 w-2 bg-purple-400 rounded-full mt-2"></div>
                  </div>
                  <div className="flex-1 min-w-0">
                    <p className="text-sm text-gray-900">Новый клиент зарегистрирован</p>
                    <p className="text-xs text-gray-500">1 час назад</p>
                  </div>
                </div>
                <div className="flex items-start space-x-3">
                  <div className="flex-shrink-0">
                    <div className="h-2 w-2 bg-orange-400 rounded-full mt-2"></div>
                  </div>
                  <div className="flex-1 min-w-0">
                    <p className="text-sm text-gray-900">Обновлен прайс-лист</p>
                    <p className="text-xs text-gray-500">2 часа назад</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </DashboardLayout>
  );
} 