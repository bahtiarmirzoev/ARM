"use client";

import { useState } from "react";
import { DashboardLayout } from "@/components/layout/dashboard-layout";
import { Button } from "@/components/ui/button";
import { Brand } from "@/types";
import {
  PlusIcon,
  MagnifyingGlassIcon,
  FunnelIcon,
  EyeIcon,
  PencilIcon,
  TrashIcon,
  BuildingOfficeIcon,
  StarIcon,
} from "@heroicons/react/24/outline";
import toast from "react-hot-toast";

export default function BrandsPage() {
  const [page, setPage] = useState(1);
  const [searchTerm, setSearchTerm] = useState("");
  const [selectedStatus, setSelectedStatus] = useState("all");

  // Мок данные автосервисов
  const mockBrands: Brand[] = [
    {
      id: "1",
      name: "Автосервис Центральный",
      fullName: "ООО Автосервис Центральный",
      description: "Крупный автосервис в центре города с полным спектром услуг",
      phoneNumber: "+7 (495) 123-45-67",
      address: "ул. Тверская, д. 1, Москва",
      rating: 4.8,
      email: "info@autoservice-central.ru",
      totalReviews: 156,
      maxCarsPerDay: 20,
      hasParking: true,
      hasWaitingRoom: true,
      isOpen: true,
      createdAt: "2024-01-01",
    },
    {
      id: "2",
      name: "Автосервис Северный",
      fullName: "ИП Иванов Автосервис Северный",
      description: "Небольшой автосервис в северной части города",
      phoneNumber: "+7 (495) 234-56-78",
      address: "ул. Ленинградская, д. 15, Москва",
      rating: 4.2,
      email: "north@autoservice.ru",
      totalReviews: 89,
      maxCarsPerDay: 8,
      hasParking: false,
      hasWaitingRoom: true,
      isOpen: true,
      createdAt: "2024-01-02",
    },
    {
      id: "3",
      name: "Автосервис Южный",
      fullName: "ООО Автосервис Южный",
      description: "Современный автосервис с новейшим оборудованием",
      phoneNumber: "+7 (495) 345-67-89",
      address: "ул. Южная, д. 25, Москва",
      rating: 4.9,
      email: "south@autoservice.ru",
      totalReviews: 234,
      maxCarsPerDay: 25,
      hasParking: true,
      hasWaitingRoom: true,
      isOpen: true,
      createdAt: "2024-01-03",
    },
    {
      id: "4",
      name: "Автосервис Западный",
      fullName: "ИП Петров Автосервис Западный",
      description: "Специализируется на ремонте немецких автомобилей",
      phoneNumber: "+7 (495) 456-78-90",
      address: "ул. Западная, д. 10, Москва",
      rating: 4.5,
      email: "west@autoservice.ru",
      totalReviews: 67,
      maxCarsPerDay: 12,
      hasParking: true,
      hasWaitingRoom: false,
      isOpen: false,
      createdAt: "2024-01-04",
    },
  ];

  const brands = mockBrands;
  const totalPages = 1;

  const handleDeleteBrand = async (brandId: string) => {
    if (window.confirm("Вы уверены, что хотите удалить этот автосервис?")) {
      toast.success("Автосервис успешно удален");
    }
  };

  const handleToggleStatus = async (brandId: string, currentStatus: boolean) => {
    toast.success(`Автосервис ${currentStatus ? "закрыт" : "открыт"}`);
  };

  const getStatusColor = (isOpen: boolean) => {
    return isOpen
      ? "bg-green-100 text-green-800"
      : "bg-red-100 text-red-800";
  };

  const getStatusText = (isOpen: boolean) => {
    return isOpen ? "Открыт" : "Закрыт";
  };

  const renderStars = (rating: number) => {
    return Array.from({ length: 5 }, (_, i) => (
      <StarIcon
        key={i}
        className={`h-4 w-4 ${
          i < rating ? "text-yellow-400 fill-current" : "text-gray-300"
        }`}
      />
    ));
  };

  const filteredBrands = brands.filter((brand: Brand) => {
    const matchesSearch = brand.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
                         brand.fullName.toLowerCase().includes(searchTerm.toLowerCase()) ||
                         brand.address.toLowerCase().includes(searchTerm.toLowerCase());
    const matchesStatus = selectedStatus === "all" || 
                         (selectedStatus === "open" && brand.isOpen) ||
                         (selectedStatus === "closed" && !brand.isOpen);
    
    return matchesSearch && matchesStatus;
  });

  return (
    <DashboardLayout>
      <div className="space-y-6">
        {/* Header */}
        <div className="flex justify-between items-center">
          <div>
            <h1 className="text-2xl font-bold text-gray-900">Автосервисы</h1>
            <p className="text-gray-600">
              Управление автосервисами в системе
            </p>
          </div>
          <Button className="flex items-center gap-2">
            <PlusIcon className="h-4 w-4" />
            Добавить автосервис
          </Button>
        </div>

        {/* Stats */}
        <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
          <div className="bg-white rounded-lg shadow p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <BuildingOfficeIcon className="h-8 w-8 text-blue-600" />
              </div>
              <div className="ml-5 w-0 flex-1">
                <dl>
                  <dt className="text-sm font-medium text-gray-500 truncate">
                    Всего автосервисов
                  </dt>
                  <dd className="text-2xl font-semibold text-gray-900">
                    {brands.length}
                  </dd>
                </dl>
              </div>
            </div>
          </div>
          <div className="bg-white rounded-lg shadow p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <BuildingOfficeIcon className="h-8 w-8 text-green-600" />
              </div>
              <div className="ml-5 w-0 flex-1">
                <dl>
                  <dt className="text-sm font-medium text-gray-500 truncate">
                    Открытых
                  </dt>
                  <dd className="text-2xl font-semibold text-gray-900">
                    {brands.filter(b => b.isOpen).length}
                  </dd>
                </dl>
              </div>
            </div>
          </div>
          <div className="bg-white rounded-lg shadow p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <StarIcon className="h-8 w-8 text-yellow-400" />
              </div>
              <div className="ml-5 w-0 flex-1">
                <dl>
                  <dt className="text-sm font-medium text-gray-500 truncate">
                    Средний рейтинг
                  </dt>
                  <dd className="text-2xl font-semibold text-gray-900">
                    {(brands.reduce((sum, b) => sum + b.rating, 0) / brands.length).toFixed(1)}
                  </dd>
                </dl>
              </div>
            </div>
          </div>
          <div className="bg-white rounded-lg shadow p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <BuildingOfficeIcon className="h-8 w-8 text-purple-600" />
              </div>
              <div className="ml-5 w-0 flex-1">
                <dl>
                  <dt className="text-sm font-medium text-gray-500 truncate">
                    Всего отзывов
                  </dt>
                  <dd className="text-2xl font-semibold text-gray-900">
                    {brands.reduce((sum, b) => sum + b.totalReviews, 0)}
                  </dd>
                </dl>
              </div>
            </div>
          </div>
        </div>

        {/* Filters */}
        <div className="bg-white rounded-lg shadow p-6">
          <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
            {/* Search */}
            <div className="relative">
              <MagnifyingGlassIcon className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-gray-400" />
              <input
                type="text"
                placeholder="Поиск по названию или адресу..."
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                className="pl-10 pr-4 py-2 w-full border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 focus:border-transparent"
              />
            </div>

            {/* Status Filter */}
            <select
              value={selectedStatus}
              onChange={(e) => setSelectedStatus(e.target.value)}
              className="px-4 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 focus:border-transparent"
            >
              <option value="all">Все статусы</option>
              <option value="open">Открытые</option>
              <option value="closed">Закрытые</option>
            </select>

            {/* Clear Filters */}
            <Button
              onClick={() => {
                setSearchTerm("");
                setSelectedStatus("all");
              }}
              className="flex items-center gap-2"
            >
              <FunnelIcon className="h-4 w-4" />
              Сбросить
            </Button>
          </div>
        </div>

        {/* Brands Table */}
        <div className="bg-white rounded-lg shadow overflow-hidden">
          <div className="overflow-x-auto">
            <table className="min-w-full divide-y divide-gray-200">
              <thead className="bg-gray-50">
                <tr>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Автосервис
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Контакты
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Рейтинг
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Мощность
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Статус
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Действия
                  </th>
                </tr>
              </thead>
              <tbody className="bg-white divide-y divide-gray-200">
                {filteredBrands.map((brand: Brand) => (
                  <tr key={brand.id} className="hover:bg-gray-50">
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="flex items-center">
                        <div className="flex-shrink-0 h-10 w-10">
                          <div className="h-10 w-10 rounded-full bg-gray-300 flex items-center justify-center">
                            <BuildingOfficeIcon className="h-5 w-5 text-gray-600" />
                          </div>
                        </div>
                        <div className="ml-4">
                          <div className="text-sm font-medium text-gray-900">
                            {brand.name}
                          </div>
                          <div className="text-sm text-gray-500">
                            {brand.fullName}
                          </div>
                          <div className="text-sm text-gray-500">
                            {brand.address}
                          </div>
                        </div>
                      </div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="text-sm text-gray-900">
                        {brand.phoneNumber}
                      </div>
                      <div className="text-sm text-gray-500">
                        {brand.email}
                      </div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="flex items-center">
                        <div className="flex">
                          {renderStars(brand.rating)}
                        </div>
                        <span className="ml-2 text-sm text-gray-900">
                          {brand.rating} ({brand.totalReviews})
                        </span>
                      </div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="text-sm text-gray-900">
                        {brand.maxCarsPerDay} машин/день
                      </div>
                      <div className="text-xs text-gray-500">
                        {brand.hasParking ? "Парковка" : "Без парковки"} • {brand.hasWaitingRoom ? "Зал ожидания" : "Без зала ожидания"}
                      </div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <span className={`inline-flex px-2 py-1 text-xs font-semibold rounded-full ${getStatusColor(brand.isOpen)}`}>
                        {getStatusText(brand.isOpen)}
                      </span>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                      <div className="flex space-x-2">
                        <button className="text-blue-600 hover:text-blue-900">
                          <EyeIcon className="h-4 w-4" />
                        </button>
                        <button className="text-indigo-600 hover:text-indigo-900">
                          <PencilIcon className="h-4 w-4" />
                        </button>
                        <button 
                          onClick={() => handleToggleStatus(brand.id, brand.isOpen)}
                          className="text-yellow-600 hover:text-yellow-900"
                        >
                          {brand.isOpen ? "Закрыть" : "Открыть"}
                        </button>
                        <button 
                          onClick={() => handleDeleteBrand(brand.id)}
                          className="text-red-600 hover:text-red-900"
                        >
                          <TrashIcon className="h-4 w-4" />
                        </button>
                      </div>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>

          {/* Pagination */}
          {totalPages > 1 && (
            <div className="bg-white px-4 py-3 flex items-center justify-between border-t border-gray-200 sm:px-6">
              <div className="flex-1 flex justify-between sm:hidden">
                <button
                  onClick={() => setPage(Math.max(1, page - 1))}
                  disabled={page === 1}
                  className="relative inline-flex items-center px-4 py-2 border border-gray-300 text-sm font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50 disabled:opacity-50"
                >
                  Назад
                </button>
                <button
                  onClick={() => setPage(Math.min(totalPages, page + 1))}
                  disabled={page === totalPages}
                  className="ml-3 relative inline-flex items-center px-4 py-2 border border-gray-300 text-sm font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50 disabled:opacity-50"
                >
                  Вперед
                </button>
              </div>
              <div className="hidden sm:flex-1 sm:flex sm:items-center sm:justify-between">
                <div>
                  <p className="text-sm text-gray-700">
                    Показано <span className="font-medium">{((page - 1) * 10) + 1}</span> до{" "}
                    <span className="font-medium">{Math.min(page * 10, filteredBrands.length)}</span> из{" "}
                    <span className="font-medium">{filteredBrands.length}</span> результатов
                  </p>
                </div>
                <div>
                  <nav className="relative z-0 inline-flex rounded-md shadow-sm -space-x-px">
                    <button
                      onClick={() => setPage(Math.max(1, page - 1))}
                      disabled={page === 1}
                      className="relative inline-flex items-center px-2 py-2 rounded-l-md border border-gray-300 bg-white text-sm font-medium text-gray-500 hover:bg-gray-50 disabled:opacity-50"
                    >
                      Назад
                    </button>
                    <button
                      onClick={() => setPage(Math.min(totalPages, page + 1))}
                      disabled={page === totalPages}
                      className="relative inline-flex items-center px-2 py-2 rounded-r-md border border-gray-300 bg-white text-sm font-medium text-gray-500 hover:bg-gray-50 disabled:opacity-50"
                    >
                      Вперед
                    </button>
                  </nav>
                </div>
              </div>
            </div>
          )}
        </div>
      </div>
    </DashboardLayout>
  );
} 