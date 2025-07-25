"use client";

import { useState } from "react";
import { DashboardLayout } from "@/components/layout/dashboard-layout";
import { useCustomers, useDeleteCustomer, useUpdateCustomer } from "@/hooks/use-api";
import { Button } from "@/components/ui/button";
import { Customer } from "@/types";
import {
  PlusIcon,
  MagnifyingGlassIcon,
  FunnelIcon,
  EyeIcon,
  PencilIcon,
  TrashIcon,
} from "@heroicons/react/24/outline";

export default function CustomersPage() {
  const [page, setPage] = useState(1);
  const [searchTerm, setSearchTerm] = useState("");
  const [selectedStatus, setSelectedStatus] = useState("all");

  const { data: customersData, isLoading, error } = useCustomers(page, 10);
  const deleteCustomerMutation = useDeleteCustomer();
  const updateCustomerMutation = useUpdateCustomer();

  const customers = customersData?.data || [];
  const totalPages = customersData?.totalPages || 1;

  const handleDeleteCustomer = async (customerId: string) => {
    if (window.confirm("Вы уверены, что хотите удалить этого клиента?")) {
      try {
        await deleteCustomerMutation.mutateAsync(customerId);
      } catch (error) {
        console.error("Error deleting customer:", error);
      }
    }
  };

  const handleToggleStatus = async (customerId: string, currentStatus: boolean) => {
    try {
      await updateCustomerMutation.mutateAsync({
        id: customerId,
        data: { isActive: !currentStatus }
      });
    } catch (error) {
      console.error("Error updating customer status:", error);
    }
  };

  const getStatusColor = (isActive: boolean) => {
    return isActive
      ? "bg-green-100 text-green-800"
      : "bg-red-100 text-red-800";
  };

  const getStatusText = (isActive: boolean) => {
    return isActive ? "Активен" : "Неактивен";
  };

  const getEmailVerifiedColor = (emailVerified: boolean) => {
    return emailVerified
      ? "bg-blue-100 text-blue-800"
      : "bg-yellow-100 text-yellow-800";
  };

  const getEmailVerifiedText = (emailVerified: boolean) => {
    return emailVerified ? "Подтвержден" : "Не подтвержден";
  };

  const filteredCustomers = customers.filter((customer: Customer) => {
    const matchesSearch = customer.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
                         customer.surname.toLowerCase().includes(searchTerm.toLowerCase()) ||
                         customer.email.toLowerCase().includes(searchTerm.toLowerCase()) ||
                         customer.phoneNumber.includes(searchTerm);
    const matchesStatus = selectedStatus === "all" || 
                         (selectedStatus === "active" && customer.isActive) ||
                         (selectedStatus === "inactive" && !customer.isActive);
    
    return matchesSearch && matchesStatus;
  });

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
          <p className="text-red-600">Ошибка загрузки клиентов</p>
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
            <h1 className="text-2xl font-bold text-gray-900">Клиенты</h1>
            <p className="text-gray-600">
              Управление клиентами системы
            </p>
          </div>
          <Button className="flex items-center gap-2">
            <PlusIcon className="h-4 w-4" />
            Добавить клиента
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
                placeholder="Поиск по имени, email или телефону..."
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
              <option value="active">Активные</option>
              <option value="inactive">Неактивные</option>
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

        {/* Customers Table */}
        <div className="bg-white rounded-lg shadow overflow-hidden">
          <div className="overflow-x-auto">
            <table className="min-w-full divide-y divide-gray-200">
              <thead className="bg-gray-50">
                <tr>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Клиент
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Контакты
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Статус
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Email
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
                {filteredCustomers.map((customer: Customer) => (
                  <tr key={customer.id} className="hover:bg-gray-50">
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="flex items-center">
                        <div className="flex-shrink-0 h-10 w-10">
                          <div className="h-10 w-10 rounded-full bg-gray-300 flex items-center justify-center">
                            <span className="text-sm font-medium text-gray-700">
                              {customer.name.charAt(0).toUpperCase()}
                            </span>
                          </div>
                        </div>
                        <div className="ml-4">
                          <div className="text-sm font-medium text-gray-900">
                            {customer.name} {customer.surname}
                          </div>
                        </div>
                      </div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                      {customer.phoneNumber}
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <span className={`inline-flex px-2 py-1 text-xs font-semibold rounded-full ${getStatusColor(customer.isActive)}`}>
                        {getStatusText(customer.isActive)}
                      </span>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="flex flex-col gap-1">
                        <span className="text-sm text-gray-900">{customer.email}</span>
                        <span className={`inline-flex px-2 py-1 text-xs font-semibold rounded-full ${getEmailVerifiedColor(customer.emailVerified)}`}>
                          {getEmailVerifiedText(customer.emailVerified)}
                        </span>
                      </div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                      {new Date(customer.createdAt).toLocaleDateString()}
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                      <div className="flex items-center space-x-2">
                        <Button
                          onClick={() => handleToggleStatus(customer.id, customer.isActive)}
                          className="flex items-center gap-1"
                        >
                          <PencilIcon className="h-3 w-3" />
                          {customer.isActive ? "Деактивировать" : "Активировать"}
                        </Button>
                        <Button
                          onClick={() => handleDeleteCustomer(customer.id)}
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
                      {Math.min(page * 10, customersData?.totalItems || 0)}
                    </span>{" "}
                    из <span className="font-medium">{customersData?.totalItems || 0}</span> результатов
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
            <div className="text-sm font-medium text-gray-500">Всего клиентов</div>
            <div className="text-2xl font-bold text-gray-900">{customers.length}</div>
          </div>
          <div className="bg-white p-4 rounded-lg shadow">
            <div className="text-sm font-medium text-gray-500">Активные</div>
            <div className="text-2xl font-bold text-green-600">
              {customers.filter(c => c.isActive).length}
            </div>
          </div>
          <div className="bg-white p-4 rounded-lg shadow">
            <div className="text-sm font-medium text-gray-500">Неактивные</div>
            <div className="text-2xl font-bold text-red-600">
              {customers.filter(c => !c.isActive).length}
            </div>
          </div>
          <div className="bg-white p-4 rounded-lg shadow">
            <div className="text-sm font-medium text-gray-500">Email подтверждены</div>
            <div className="text-2xl font-bold text-blue-600">
              {customers.filter(c => c.emailVerified).length}
            </div>
          </div>
        </div>
      </div>
    </DashboardLayout>
  );
} 