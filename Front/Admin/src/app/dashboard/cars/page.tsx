"use client";

import { useState } from "react";
import { DashboardLayout } from "@/components/layout/dashboard-layout";
import { useCars, useDeleteCar, useUpdateCar } from "@/hooks/use-api";
import { Button } from "@/components/ui/button";
import { Car } from "@/types";
import {
  PlusIcon,
  MagnifyingGlassIcon,
  FunnelIcon,
  EyeIcon,
  PencilIcon,
  TrashIcon,
  WrenchScrewdriverIcon,
} from "@heroicons/react/24/outline";

export default function CarsPage() {
  const [page, setPage] = useState(1);
  const [searchTerm, setSearchTerm] = useState("");
  const [selectedMake, setSelectedMake] = useState("all");

  const { data: carsData, isLoading, error } = useCars(page, 10);
  const deleteCarMutation = useDeleteCar();
  const updateCarMutation = useUpdateCar();

  const cars = carsData?.data || [];
  const totalPages = carsData?.totalPages || 1;

  const handleDeleteCar = async (carId: string) => {
    if (window.confirm("Вы уверены, что хотите удалить этот автомобиль?")) {
      try {
        await deleteCarMutation.mutateAsync(carId);
      } catch (error) {
        console.error("Error deleting car:", error);
      }
    }
  };

  const handleUpdateCar = async (carId: string, data: Partial<Car>) => {
    try {
      await updateCarMutation.mutateAsync({ id: carId, data });
    } catch (error) {
      console.error("Error updating car:", error);
    }
  };

  const getTransmissionText = (transmission: string) => {
    switch (transmission) {
      case "manual":
        return "Механика";
      case "automatic":
        return "Автомат";
      case "cvt":
        return "Вариатор";
      case "robot":
        return "Робот";
      default:
        return transmission;
    }
  };

  const getEngineTypeText = (engineType: string) => {
    switch (engineType) {
      case "gasoline":
        return "Бензин";
      case "diesel":
        return "Дизель";
      case "hybrid":
        return "Гибрид";
      case "electric":
        return "Электро";
      default:
        return engineType;
    }
  };

  const filteredCars = cars.filter((car: Car) => {
    const matchesSearch = car.make.toLowerCase().includes(searchTerm.toLowerCase()) ||
                         car.model.toLowerCase().includes(searchTerm.toLowerCase()) ||
                         car.carPlate.toLowerCase().includes(searchTerm.toLowerCase()) ||
                         car.vin.toLowerCase().includes(searchTerm.toLowerCase()) ||
                         car.ownerName.toLowerCase().includes(searchTerm.toLowerCase());
    const matchesMake = selectedMake === "all" || car.make.toLowerCase() === selectedMake.toLowerCase();
    
    return matchesSearch && matchesMake;
  });

  const uniqueMakes = Array.from(new Set(cars.map(car => car.make))).sort();

  if (isLoading) {
    return (
      <DashboardLayout>
        <div className="flex items-center justify-center h-64">
          <div className="animate-spin rounded-full h-32 w-32 border-b-2 border-blue-600"></div>
        </div>
      </DashboardLayout>
    );
  }

  if (error) {
    return (
      <DashboardLayout>
        <div className="text-center py-12">
          <p className="text-red-600">Ошибка загрузки автомобилей</p>
        </div>
      </DashboardLayout>
    );
  }

  return (
    <DashboardLayout>
      <div className="space-y-6">
        {/* Header */}
        <div className="flex justify-between items-center">
          <div>
            <h1 className="text-2xl font-bold text-gray-900">Автомобили</h1>
            <p className="text-gray-600">
              Управление автомобилями клиентов
            </p>
          </div>
          <Button className="flex items-center gap-2">
            <PlusIcon className="h-4 w-4" />
            Добавить автомобиль
          </Button>
        </div>

        {/* Filters */}
        <div className="bg-white rounded-lg shadow p-6">
          <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
            {/* Search */}
            <div className="relative">
              <MagnifyingGlassIcon className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-gray-400" />
              <input
                type="text"
                placeholder="Поиск по марке, модели, номеру или VIN..."
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                className="pl-10 pr-4 py-2 w-full border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 focus:border-transparent"
              />
            </div>

            {/* Make Filter */}
            <select
              value={selectedMake}
              onChange={(e) => setSelectedMake(e.target.value)}
              className="px-4 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 focus:border-transparent"
            >
              <option value="all">Все марки</option>
              {uniqueMakes.map((make) => (
                <option key={make} value={make}>{make}</option>
              ))}
            </select>

            {/* Clear Filters */}
            <Button
              onClick={() => {
                setSearchTerm("");
                setSelectedMake("all");
              }}
              className="flex items-center gap-2"
            >
              <FunnelIcon className="h-4 w-4" />
              Сбросить
            </Button>
          </div>
        </div>

        {/* Cars Table */}
        <div className="bg-white rounded-lg shadow overflow-hidden">
          <div className="overflow-x-auto">
            <table className="min-w-full divide-y divide-gray-200">
              <thead className="bg-gray-50">
                <tr>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Автомобиль
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Номер
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    VIN
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Двигатель
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Владелец
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Дата регистрации
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Действия
                  </th>
                </tr>
              </thead>
              <tbody className="bg-white divide-y divide-gray-200">
                {filteredCars.map((car: Car) => (
                  <tr key={car.id} className="hover:bg-gray-50">
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="flex items-center">
                        <div className="flex-shrink-0 h-10 w-10">
                          <div className="h-10 w-10 rounded-full bg-gray-300 flex items-center justify-center">
                            <WrenchScrewdriverIcon className="h-5 w-5 text-gray-600" />
                          </div>
                        </div>
                        <div className="ml-4">
                          <div className="text-sm font-medium text-gray-900">
                            {car.make} {car.model}
                          </div>
                          <div className="text-sm text-gray-500">
                            {car.year} • {car.color}
                          </div>
                        </div>
                      </div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">
                      {car.carPlate}
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500 font-mono">
                      {car.vin}
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="text-sm text-gray-900">
                        {getEngineTypeText(car.engineType)}
                      </div>
                      <div className="text-sm text-gray-500">
                        {car.engineVolume}л • {getTransmissionText(car.transmission)}
                      </div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="text-sm font-medium text-gray-900">
                        {car.ownerName}
                      </div>
                      <div className="text-sm text-gray-500">
                        {car.ownerEmail}
                      </div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                      {new Date(car.createdAt).toLocaleDateString()}
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                      <div className="flex items-center space-x-2">
                        <Button
                          className="flex items-center gap-1"
                        >
                          <EyeIcon className="h-3 w-3" />
                          Просмотр
                        </Button>
                        <Button
                          className="flex items-center gap-1"
                        >
                          <PencilIcon className="h-3 w-3" />
                          Редактировать
                        </Button>
                        <Button
                          
                          onClick={() => handleDeleteCar(car.id)}
                          className="flex items-center gap-1"
                        >
                          <TrashIcon className="h-3 w-3" />
                          Удалить
                        </Button>
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
                <Button
                
                  onClick={() => setPage(page - 1)}
                  disabled={page === 1}
                >
                  Назад
                </Button>
                <Button
                 
                  onClick={() => setPage(page + 1)}
                  disabled={page === totalPages}
                >
                  Вперед
                </Button>
              </div>
              <div className="hidden sm:flex-1 sm:flex sm:items-center sm:justify-between">
                <div>
                  <p className="text-sm text-gray-700">
                    Показано <span className="font-medium">{(page - 1) * 10 + 1}</span> до{" "}
                    <span className="font-medium">
                      {Math.min(page * 10, carsData?.totalItems || 0)}
                    </span>{" "}
                    из <span className="font-medium">{carsData?.totalItems || 0}</span> результатов
                  </p>
                </div>
                <div>
                  <nav className="relative z-0 inline-flex rounded-md shadow-sm -space-x-px">
                    <Button
                      onClick={() => setPage(page - 1)}
                      disabled={page === 1}
                      className="rounded-l-md"
                    >
                      Назад
                    </Button>
                    {Array.from({ length: totalPages }, (_, i) => i + 1).map((pageNum) => (
                      <Button
                        key={pageNum}
                      
                        onClick={() => setPage(pageNum)}
                        className="rounded-none"
                      >
                        {pageNum}
                      </Button>
                    ))}
                    <Button
                      onClick={() => setPage(page + 1)}
                      disabled={page === totalPages}
                      className="rounded-r-md"
                    >
                      Вперед
                    </Button>
                  </nav>
                </div>
              </div>
            </div>
          )}
        </div>

        {/* Summary Stats */}
        <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
          <div className="bg-white p-4 rounded-lg shadow">
            <div className="text-sm font-medium text-gray-500">Всего автомобилей</div>
            <div className="text-2xl font-bold text-gray-900">{cars.length}</div>
          </div>
          <div className="bg-white p-4 rounded-lg shadow">
            <div className="text-sm font-medium text-gray-500">Уникальных марок</div>
            <div className="text-2xl font-bold text-blue-600">
              {uniqueMakes.length}
            </div>
          </div>
          <div className="bg-white p-4 rounded-lg shadow">
            <div className="text-sm font-medium text-gray-500">Бензиновые</div>
            <div className="text-2xl font-bold text-green-600">
              {cars.filter(c => c.engineType === "gasoline").length}
            </div>
          </div>
          <div className="bg-white p-4 rounded-lg shadow">
            <div className="text-sm font-medium text-gray-500">Дизельные</div>
            <div className="text-2xl font-bold text-orange-600">
              {cars.filter(c => c.engineType === "diesel").length}
            </div>
          </div>
        </div>
      </div>
    </DashboardLayout>
  );
} 